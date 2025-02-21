﻿using AHSW.EventLog.Extensions;
using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Models.Enums;
using Bookstore.Sample.Configurations;
using Bookstore.Sample.Interfaces;
using Bookstore.Sample.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Sample;

internal static class Program
{
    public static async Task Main()
    {
        const bool recreateDatabase = true;
        
        var services = GetServices(Host.Create(recreateDatabase).Services);

        const int userId = 3;
        const int bookCount = 2;
        
        await CreateBooksOneByOneInSingleEventScopeWithoutEntityLog(userId, bookCount, services);
        await CreateBooksAndAddToShelfOneByOneInDedicatedEventScope(userId, bookCount, services);
        await CreateBooksAndAddToShelfOneByOneInSingleEventScope(userId, bookCount, services);
        await CreateBookAndThenUpdateBookInDedicatedEventScope(userId, services);
        await CreateBooksAndThrowUnhandledException(userId, bookCount, services);
        await CreateBooksAndThrowHandledException(userId, services);
        
        Console.WriteLine("Completed...");
    }

    private static ResolvedServices GetServices(IServiceProvider serviceProvider) =>
        new()
        {
            EventLog = serviceProvider
                .GetRequiredService<IEventLogService<EventType, EntityType, PropertyType>>(),
            BookRepository = serviceProvider
                .GetRequiredService<IBookRepository>(),
            ShelfRepository = serviceProvider
                .GetRequiredService<IShelfRepository>()
        };
    
    // Register only event level record for all entities
    // --------
    // Results:
    // --------
    // Event level records: 1 (1 shelf and 2 books creation is recorded as 1 event)
    // Entity level records: 0
    // Property level records: 0
    private static  async Task CreateBooksOneByOneInSingleEventScopeWithoutEntityLog(
        int userId, int bookCount, ResolvedServices services) =>
            await services.EventLog.CreateEventScopeAndRun(
                EventType.AddBooksOnShelf,
                async eventLogScope =>
                {
                    eventLogScope.EventLogEntry.CreatedBy = userId;
                    eventLogScope.EventLogEntry.Details = $"Adding a shelf and {bookCount} books";
                        
                    var shelf = new ShelfEntity()
                    {
                        Height = (decimal)Random.Shared.Next(1, 100) / 100
                    };
                
                    await eventLogScope.SaveAndLogEntitiesAsync(
                        () => services.ShelfRepository.AddOrUpdateAsync(shelf));

                    for (var index = 0; index < bookCount; index++)
                    {
                        var book = CreateBookEntity();
                        book.Shelf = shelf;
                            
                        await eventLogScope.SaveAndLogEntitiesAsync(
                            () => services.BookRepository.AddOrUpdateAsync(book));
                    }
                });
    
    // Register event, entity, and property levels records as one event for all entities
    // --------
    // Results:
    // --------
    // Event level records: 1 (1 shelf and 2 books creation is recorded as 1 event)
    // Entity level records: 3 (1 shelf and 2 books creation)
    // Property level records: StringPropertyLog, Int32PropertyLog, etc. have change records
    private static  async Task CreateBooksAndAddToShelfOneByOneInSingleEventScope(
        int userId, int bookCount, ResolvedServices services) =>
            await services.EventLog.CreateEventScopeAndRun(
                EventType.AddBooksOnShelf,
                async eventLogScope =>
                {
                    eventLogScope.EventLogEntry.CreatedBy = userId;
                    eventLogScope.EventLogEntry.Details = $"Adding a shelf and {bookCount} books";
                    
                    var shelf = new ShelfEntity()
                    {
                        Height = (decimal)Random.Shared.Next(1, 100) / 100
                    };
            
                    await eventLogScope.SaveAndLogEntitiesAsync(
                        () => services.ShelfRepository.AddOrUpdateAsync(shelf),
                        options => options
                            .AddEntityLogging(
                                services.ShelfRepository.GetOriginalPropertyValue,
                                new[] { shelf }, ObservableProperties.GetForShelfEntity));

                    for (var index = 0; index < bookCount; index++)
                    {
                        var book = CreateBookEntity();
                        book.Shelf = shelf;
                        
                        await eventLogScope.SaveAndLogEntitiesAsync(
                            () => services.BookRepository.AddOrUpdateAsync(book),
                            options => options
                                .AddEntityLogging(
                                    services.BookRepository.GetOriginalPropertyValue,
                                    new[] { book }, ObservableProperties.GetForBookEntity));
                    }
                });
    
    // Register event, entity, and property levels records in dedicated event scope for each entity
    // --------
    // Results:
    // --------
    // Event level records: 3 (1 for shelf and 2 for books creation)
    // Entity level records: 3 (1 shelf and 2 books creation)
    // Property level records: StringPropertyLog, Int32PropertyLog, etc. have change records
    private static  async Task CreateBooksAndAddToShelfOneByOneInDedicatedEventScope(
        int userId, int bookCount, ResolvedServices services)
    {
        var shelf = new ShelfEntity()
        {
            Height = (decimal)Random.Shared.Next(1, 100) / 100
        };
        
        await services.EventLog.CreateEventScopeAndRun(
            EventType.AddShelf,
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "Adding a shelf";
                
                await eventLogScope.SaveAndLogEntitiesAsync(
                    () => services.ShelfRepository.AddOrUpdateAsync(shelf),
                    options => options
                        .AddEntityLogging(
                            services.ShelfRepository.GetOriginalPropertyValue,
                            new[] { shelf }, ObservableProperties.GetForShelfEntity));
            });
        
        for (var index = 0; index < bookCount; index++)
        {
            var book = CreateBookEntity();
            book.Shelf = shelf;

            await services.EventLog.CreateEventScopeAndRun(
                EventType.AddBooksOnShelf,
                async eventLogScope =>
                {
                    eventLogScope.EventLogEntry.CreatedBy = userId;
                    eventLogScope.EventLogEntry.Details = "Adding a book";
                
                    await eventLogScope.SaveAndLogEntitiesAsync(
                        () => services.BookRepository.AddOrUpdateAsync(book),
                        options => options
                            .AddEntityLogging(
                                services.BookRepository.GetOriginalPropertyValue,
                                new[] { book }, ObservableProperties.GetForBookEntity));
                });
        }
    }

    // Register event, entity, and property levels records in dedicated event scope for each entity
    // 1st event - create a book
    // 2nd event - change the book title and update
    // 3rd event - update the book without changing any observable book property
    // --------
    // Results:
    // --------
    // Event level records: 3 (1 for creation and 2 for updating)
    // Entity level records: 2 (1 for creation and 1 for updating. Update record is created only for event where at least one observable property is changed)
    // Property level records: StringPropertyLog, Int32PropertyLog, etc. have change records
    private static  async Task CreateBookAndThenUpdateBookInDedicatedEventScope(
        int userId, ResolvedServices services)
    {
        var book = CreateBookEntity();

        await services.EventLog.CreateEventScopeAndRun(
            EventType.AddBooksOnShelf,
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "Adding a book";
            
                await eventLogScope.SaveAndLogEntitiesAsync(
                    () => services.BookRepository.AddOrUpdateAsync(book),
                    options => options
                        .AddEntityLogging(
                            services.BookRepository.GetOriginalPropertyValue,
                            new[] { book }, ObservableProperties.GetForBookEntity));
            });

        book.Title += " - Revision_1";
        
        await services.EventLog.CreateEventScopeAndRun(
            EventType.UpdateBooksOnShelf,
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "Observable property is changed";
            
                await eventLogScope.SaveAndLogEntitiesAsync(
                    () => services.BookRepository.AddOrUpdateAsync(book),
                    options => options
                        .AddEntityLogging(
                            services.BookRepository.GetOriginalPropertyValue,
                            new[] { book }, ObservableProperties.GetForBookEntity));
            });
        
        await services.EventLog.CreateEventScopeAndRun(
            EventType.UpdateBooksOnShelf,
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "No one observable property is not changed";
            
                await eventLogScope.SaveAndLogEntitiesAsync(
                    () => services.BookRepository.AddOrUpdateAsync(book),
                    options => options
                        .AddEntityLogging(
                            services.BookRepository.GetOriginalPropertyValue,
                            new[] { book }, ObservableProperties.GetForBookEntity));
            });
    }
    
    // Register event, entity, and property levels records for books and throw unhandled exception
    // --------
    // Results:
    // --------
    // Event level records: 1 (1 event with UnhandledException status)
    // Entity level records: 0
    // Property level records: 0
    private static  async Task CreateBooksAndThrowUnhandledException(
        int userId, int bookCount, ResolvedServices services) =>
            await services.EventLog.CreateEventScopeAndRun(
                EventType.AddBooksOnShelf,
                async eventLogScope =>
                {
                    eventLogScope.EventLogEntry.CreatedBy = userId;
                    eventLogScope.EventLogEntry.Details = $"Adding {bookCount} books";
                
                    await eventLogScope.SaveAndLogEntitiesAsync(
                        () => throw new Exception("An exception occurred..."),
                        options => options
                            .AddEntityLogging(
                                services.BookRepository.GetOriginalPropertyValue,
                                new[] { CreateBookEntity() }, ObservableProperties.GetForBookEntity));
                });
    
    // Register event, entity, and property levels records for books and throw handled exception
    // --------
    // Results:
    // --------
    // Event level records: 1 (1 event with HandledException status)
    // Entity level records: 1 (For successful created entity)
    // Property level records: StringPropertyLog, Int32PropertyLog, etc. have change records
    private static  async Task CreateBooksAndThrowHandledException(
        int userId, ResolvedServices services)
    {
        await services.EventLog.CreateEventScopeAndRun(
            EventType.AddBooksOnShelf,
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "Adding 2 books";
            
                var book1 = CreateBookEntity();

                await eventLogScope.SaveAndLogEntitiesAsync(
                    () => services.BookRepository.AddOrUpdateAsync(book1),
                    options => options
                        .AddEntityLogging(
                            services.BookRepository.GetOriginalPropertyValue,
                            new[] { book1 }, ObservableProperties.GetForBookEntity));

                var book2 = CreateBookEntity();

                try
                {
                    await eventLogScope.SaveAndLogEntitiesAsync(
                        () => throw new Exception("An exception occurred..."),
                        options => options
                            .AddEntityLogging(
                                services.BookRepository.GetOriginalPropertyValue,
                                new[] { book2 }, ObservableProperties.GetForBookEntity));
                }
                catch (Exception exception)
                {
                    eventLogScope.EventLogEntry.SetFailedStatusAndAddFailureDetails(
                        EventStatus.HandledException,
                        $"Database error when saving a book with title {book2.Title}",
                        exception.ToString());
                }
            });
    }
    
    private static BookEntity CreateBookEntity() => 
        new()
        {
            Title = $"EventLog Manual - {Random.Shared.Next(1, 100)} Edition",
            Published = DateTime.UtcNow,
            IsAvailable = true,
            LikeCount = Random.Shared.Next(1, 100)
        };
}

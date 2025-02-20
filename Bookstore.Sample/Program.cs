using AHWS.EventLog.Interfaces;
using AHWS.EventLog.Models;
using Bookstore.Sample.Configurations;
using EventLog.Models.Entities;
using EventLog.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Host = Bookstore.Sample.Configurations.Host;

namespace Bookstore.Sample;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var services = GetServices(Host.Create(args).Services);

        await TestMultipleLoggingIntoOneEventLogScopeAsync(services);
        await TestOnceLoggingIntoOneEventLogScopeAsync(services);
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
    
    private static  async Task TestMultipleLoggingIntoOneEventLogScopeAsync(ResolvedServices services) =>
        await services.EventLog.CreateEventScopeAndRun(EventType.RunTestMethod,
            eventLogScope => TestEventLogActionAsync(eventLogScope, services));

    private static async Task TestEventLogActionAsync(
        EventLogScope<EventType, EntityType, PropertyType> eventLogScope,
        ResolvedServices services)
    {
        const int initiatorId = 9;
        
        eventLogScope.EventLogEntry.CreatedBy = initiatorId;
        eventLogScope.EventLogEntry.Details = "MultipleLogging => OneEventLogScope";
        eventLogScope.EventLogEntry.FailureDetails = "-- empty --";
        
        Console.WriteLine("Test EventLogClient!");

        var applicationEntity = new BookEntity()
        {
            Title = "EventLog Manual",
            Published = DateTime.Now,
            IsAvailable = true,
            LikeCount = 25
        };
        
        var applicationOtherEntity = new ShelfEntity()
        {
            Height = (decimal)23.24
        };
        
        await eventLogScope.SaveAndLogEntitiesAsync(
            () => Task.WhenAll(
                services.BookRepository.AddOrUpdateAsync(applicationEntity),
                services.ShelfRepository.AddOrUpdateAsync(applicationOtherEntity)),
            options => options
                .AddEntityLogging(
                    services.BookRepository.GetOriginalPropertyValue,
                    new[] { applicationEntity },
                    ObservableProperties.GetForBookEntity)
                .AddEntityLogging(
                    services.ShelfRepository.GetOriginalPropertyValue,
                    new[] { applicationOtherEntity },
                    ObservableProperties.GetForApplicationShelfEntity));
    }
    
    private static async Task TestOnceLoggingIntoOneEventLogScopeAsync(ResolvedServices services)
    {
        var applicationEntity = new BookEntity();
        
        await services.EventLog.CreateEventScopeAndRun(EventType.RunTestMethod,
            eventLogScope =>
            {
                const int initiatorId = 9;
                
                eventLogScope.EventLogEntry.CreatedBy = initiatorId;
                eventLogScope.EventLogEntry.Details = "OnceLogging => OneEventLogScope";
                eventLogScope.EventLogEntry.FailureDetails = "-- empty --";
                
                return eventLogScope
                    .SaveAndLogEntitiesAsync(
                        () => services.BookRepository.AddOrUpdateAsync(applicationEntity),
                        options => options
                            .AddEntityLogging(
                                services.BookRepository.GetOriginalPropertyValue,
                                new[] { applicationEntity },
                                ObservableProperties.GetForBookEntity));
            });
    }
}

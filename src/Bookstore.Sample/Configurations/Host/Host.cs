using AHSW.EventLog.Extensions;
using AHSW.EventLog.Models.Configurations;
using Bookstore.Sample.DatabaseContext;
using Bookstore.Sample.Repository;
using Bookstore.Sample.Interfaces;
using Bookstore.Sample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bookstore.Sample.Configurations;

internal static class Host
{
    public static IHost Create(bool recreateDatabase)
    {
        var host = CreateHostBuilder().Build();
        
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
        
        ApplyPendingMigrations(context, recreateDatabase);
        
        EventLogServiceConfiguration<EventType, EntityType, PropertyType>.Configure<BookstoreDbContext>(
            configurationBuilder => configurationBuilder
                .UseCustomTypeDescriptions(context,
                    options => options
                        .AddEventTypeDescription(EventType.AddBooksOnShelf,
                            "Add books on shelf")
                        .AddEventTypeDescription(EventType.UpdateBooksOnShelf,
                            "Update books on shelf"))
                .RegisterEntity<BookEntity>(EntityType.Book,
                    options => options
                        .RegisterProperty(PropertyType.BookTitle,
                            x => x.Title, nameof(BookEntity.Title))
                        .RegisterProperty(PropertyType.BookPublished,
                            x => x.Published, nameof(BookEntity.Published))
                        .RegisterProperty(PropertyType.BookIsAvailable,
                            x => x.IsAvailable, nameof(BookEntity.IsAvailable))
                        .RegisterProperty(PropertyType.BookLikeCount,
                            x => x.LikeCount, nameof(BookEntity.LikeCount)))
                .RegisterEntity<ShelfEntity>(EntityType.Shelf,
                    options => options
                        .RegisterProperty(PropertyType.ShelfHeight,
                            x => x.Height, nameof(ShelfEntity.Height))));
        
        return host;
    }

    private static IHostBuilder CreateHostBuilder()
    {
        var hostBuilder = new HostBuilder();

        hostBuilder.ConfigureServices((_, services) =>
        {
            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlite("Data Source=Bookstore.db");
            });

            services.AddEventLog<BookstoreDbContext, EventType, EntityType, PropertyType>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();
        });
        
        return hostBuilder;
    }
    
    private static void ApplyPendingMigrations(BookstoreDbContext context, bool recreateDatabase)
    {
        if (recreateDatabase)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("The Bookstore database is deleted.");
        }
        
        var pendingMigrationNames = context.Database.GetPendingMigrations().ToList();
        
        if (!pendingMigrationNames.Any())
        {
            Console.WriteLine("The Bookstore database is up to date.");
            return;
        }
        
        Console.WriteLine("Pending migrations: {0}", string.Join(", ", pendingMigrationNames));
        
        context.Database.Migrate();

        Console.WriteLine("The Bookstore database was successfully updated.");
    }
}
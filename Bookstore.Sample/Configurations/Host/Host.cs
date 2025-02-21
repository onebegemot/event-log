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
    public static IHost Create(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        
        ApplyPendingMigrations(host.Services);
        
        EventLogServiceConfiguration<EventType, EntityType, PropertyType>.Configure<BookstoreDbContext>(
            configurationBuilder => configurationBuilder
                .UseCustomTypeDescriptions(
                    host.Services.GetRequiredService<BookstoreDbContext>(),
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

    private static IHostBuilder CreateHostBuilder(string[] args)
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
    
    private static void ApplyPendingMigrations(IServiceProvider servicesProvider)
    {
        var context = servicesProvider.GetRequiredService<BookstoreDbContext>();
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
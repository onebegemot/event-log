using AHWS.EventLog.Extensions;
using AHWS.EventLog.Models.Configurations;
using AHWS.EventLog.Models.Enums;
using Bookstore.Sample.Configuration.DatabaseContext;
using Bookstore.Sample.Configuration.Repository;
using EventLog.Models.Entities;
using EventLog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bookstore.Sample.Configurations;

internal class Host
{
    public static IHost Create(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        
        // Client non-required method
        ApplyApplicationPendingMigrations(host.Services);
        
        // todo: For documentation examples
        EventLogServiceConfiguration<EventType, EntityType, PropertyType>.Configure<BookstoreDbContext>(
            configurationBuilder => configurationBuilder
                .UseCustomTypeDescriptions(
                    host.Services.GetRequiredService<BookstoreDbContext>(),
                    options => options
                        .AddEventTypeDescription(EventType.RunTestMethod,
                            "Update Application Entity Text")
                        .AddEventTypeDescription(EventType.ApplicationShutdown,
                            "Remove Application Entity Text")
                        .AddEventStatusDescription(EventStatus.Successful,
                            "Successfully completed"))
                .RegisterEntity<BookEntity>(
                    EntityType.ApplicationEntity,
                    options => options
                        .RegisterProperty(PropertyType.BookTitle,
                            x => x.Title, nameof(BookEntity.Title))
                        .RegisterProperty(PropertyType.BookPublished,
                            x => x.Published, nameof(BookEntity.Published))
                        .RegisterProperty(PropertyType.BookIsAvailable,
                            x => x.IsAvailable, nameof(BookEntity.IsAvailable))
                        .RegisterProperty(PropertyType.BookLikeCount,
                            x => x.LikeCount, nameof(BookEntity.LikeCount)))
                .RegisterEntity<ShelfEntity>(
                    EntityType.ApplicationOtherEntity,
                    options => options
                        .RegisterProperty(PropertyType.ShelfHeight,
                            x => x.Height, nameof(ShelfEntity.Height))));
        
        return host;
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = new HostBuilder();
        
        // hostBuilder.ConfigureHostConfiguration(config =>
        // {
        //     config.AddEnvironmentVariables("DOTNET_");
        // });
        
        hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            // var env = hostingContext.HostingEnvironment;
            //
            // config
            //     .AddJsonFile("Configurations/configurations.json", false, false)
            //     .AddJsonFile($"Configurations/configurations.{env.EnvironmentName}.json", true, false);
            //
            // config.AddEnvironmentVariables();

            if (args != null)
            {
                config.AddCommandLine(args);
            }
        });

        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddDbContext<BookstoreDbContext>(options =>
            {
                SqliteDbContextOptionsBuilderExtensions.UseSqlite(options, "Data Source=Bookstore.db");
            });
            
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();

            services.AddEventLog<BookstoreDbContext, EventType, EntityType, PropertyType>();
        });
        
        return hostBuilder;
    }
    
    private static void ApplyApplicationPendingMigrations(IServiceProvider servicesProvider)
    {
        using var scope = servicesProvider.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
        var pendingMigrationNames = context.Database.GetPendingMigrations().ToList();
        
        if (!pendingMigrationNames.Any())
        {
            Console.WriteLine("The ApplicationDbContext database is up to date");
            return;
        }
        
        Console.WriteLine("Pending migrations: {0}", string.Join(", ", pendingMigrationNames));
        
        context.Database.Migrate();

        Console.WriteLine("The ApplicationDbContext database was successfully updated");
    }
}
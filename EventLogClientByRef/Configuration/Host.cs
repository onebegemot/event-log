using EventLog.DatabaseContext;
using EventLog.Extensions;
using EventLog.Interfaces;
using EventLog.Models.Enums;
using EventLog.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventLog.Configuration;

internal class Host
{
    public static IHost Create(string[] args) =>
        CreateHostBuilder(args).Build();

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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                SqliteDbContextOptionsBuilderExtensions.UseSqlite(options, "Data Source=Application.db");
            });
            
            services.AddScoped<IApplicationEntityRepository, ApplicationEntityRepository>();
            services.AddScoped<IApplicationOtherEntityRepository, ApplicationOtherEntityRepository>();

            services.AddEventLog<ApplicationDbContext, EventType, EntityType, PropertyType>();
        });
        
        return hostBuilder;
    }
}
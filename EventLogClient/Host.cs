using EventLog.DatabaseContext;
using EventLog.Extensions;
using EventLog.Interfaces;
using EventLog.Repository;
using EventLog.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventLog;

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
                options.UseSqlite("Data Source=Application.db");
            });
            
            services.AddScoped<ITestDataRepository, TestDataRepository>();
            
            services.ConfigureEventLog<ApplicationDbContext>();
        });
        
        return hostBuilder;
    }
}
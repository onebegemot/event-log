using EventLog.DatabaseContext;
using EventLog.Enums;
using EventLog.Interfaces;
using EventLog.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventLog;

public class Program
{
    public static async Task Main(string[] args)
    {
        await TestEventLog(args);
    }

    private static async Task TestEventLog(string[] args)
    {
        const int initiatorId = 9;

        var host = GetHost(args);
        
        var eventLogService = host.Services.GetRequiredService<IEventLogService<EventType>>();
        var testDataRepository = host.Services.GetRequiredService<ITestDataRepository>();

        var testEventLog = new TestEventLog();
        await testEventLog.TestAsync(eventLogService, testDataRepository, initiatorId);
    }
    
    #region Service methods

    private static IHost GetHost(string[] args)
    {
        var host = Host.Create(args);
        
        // todo: For manual
        EventLogServiceConfigurator.UseCustomTypeDescriptions<ApplicationDbContext, EventType>(
            configurationBuilder =>
                {
                    configurationBuilder
                        .SetDatabaseContext(host.Services.GetRequiredService<ApplicationDbContext>())
                        .AddEventTypeDescription(EventType.UpdateApplicationEntity, "Update Application Entity Text")
                        .AddEventTypeDescription(EventType.RemoveApplicationEntity, "Remove Application Entity Text");
                });

        // Client non-required method
        ApplyApplicationPendingMigrations(host.Services);

        return host;
    }
    
    private static void ApplyApplicationPendingMigrations(IServiceProvider servicesProvider)
    {
        using var scope = servicesProvider.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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

    #endregion
}

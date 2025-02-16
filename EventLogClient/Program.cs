using EventLog.DatabaseContext;
using EventLog.Enums;
using EventLog.Interfaces;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;
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

        ApplyApplicationPendingMigrations(host.Services);
        FillEventLogHelperTablesMigrations(host);

        return host;
    }
    
    // todo: must be included into NuGet
    private static void FillEventLogHelperTablesMigrations(IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        
        context.Set<EventTypeDescription>().RemoveRange(context.Set<EventTypeDescription>().ToList());
        context.Set<EventTypeDescription>().AddRange(EventLogHelper.GetEventTypeDescriptionEntities<EventType>(GetEnumId, GetDescription));
        
        context.Set<EventStatusDescription>().RemoveRange(context.Set<EventStatusDescription>().ToList());
        context.Set<EventStatusDescription>().AddRange(EventLogHelper.GetEventStatusDescriptionEntities());
        
        context.SaveChanges();
    }

    private static int GetEnumId(EventType eventType) => (int)eventType;
    
    private static string GetDescription(EventType eventType) =>
        eventType switch
        {
            EventType.UpdateApplicationEntity => "UpdateApplicationEntity",
            _ => "UNKNOWN EVENT TYPE"
        };
    
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

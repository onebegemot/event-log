using EventLog.DbContext;
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
        var eventLogService = host.Services.GetRequiredService<IEventLogService>();
        var testDataRepository = host.Services.GetRequiredService<ITestDataRepository>();

        var testEventLog = new TestEventLog();
        await testEventLog.TestAsync(eventLogService, testDataRepository, initiatorId);
    }
    
    #region Service methods

    private static IHost GetHost(string[] args)
    {
        var host = Host.Create(args);

        ApplyEventLogPendingMigrations(host.Services);
        FillEventLogHelperTablesMigrations(host);
        ApplyApplicationPendingMigrations(host.Services);

        return host;
    }
    
    // todo: must be included into NuGet
    private static void FillEventLogHelperTablesMigrations(IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<EventLogDbContext>();
        
        context.Set<EventTypeDescription>().RemoveRange(context.Set<EventTypeDescription>().ToList());
        context.Set<EventTypeDescription>().AddRange(EventLogHelper.GetEventTypeDescriptionEntities());
        
        context.Set<EventStatusDescription>().RemoveRange(context.Set<EventStatusDescription>().ToList());
        context.Set<EventStatusDescription>().AddRange(EventLogHelper.GetEventStatusDescriptionEntities());
        
        context.SaveChanges();
    }
    
    private static void ApplyEventLogPendingMigrations(IServiceProvider servicesProvider)
    {
        using var scope = servicesProvider.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<EventLogDbContext>();
        var pendingMigrationNames = context.Database.GetPendingMigrations().ToList();
        
        if (!pendingMigrationNames.Any())
        {
            Console.WriteLine("The EventLogDbContext database is up to date");
            return;
        }
        
        Console.WriteLine("Pending migrations: {0}", string.Join(", ", pendingMigrationNames));
        
        context.Database.Migrate();

        Console.WriteLine("The EventLogDbContext database was successfully updated");
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

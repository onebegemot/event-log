using EventLog.Interfaces;
using EventLog.Repository;
using EventLog.Service;
using Microsoft.Extensions.DependencyInjection;

namespace EventLog.Extensions;

public static class ConfigureExtensions
{
    public static IServiceCollection ConfigureEventLog(this IServiceCollection services)
    {
        services.AddScoped<IEventLogService, EventLogService>();
        services.AddScoped<IEventLogEntryRepository, EventLogEntryRepository>();
        
        return services;
    }
}
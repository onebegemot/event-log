using EventLog.Interfaces;
using EventLog.Repository;
using EventLog.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventLog.Extensions;

public static class ConfigureExtensions
{
    public static IServiceCollection AddEventLog<TDbContext, TEventType>(
        this IServiceCollection services)
            where TDbContext : DbContext
            where TEventType : struct, Enum
    {
        services.AddScoped<IEventLogService<TEventType>, EventLogService<TEventType>>();
        services.AddScoped<IEventLogEntryRepository<TEventType>, EventLogEntryRepository<TDbContext, TEventType>>();
        
        return services;
    }
}
using EventLog.Interfaces;
using EventLog.Repository;
using EventLog.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventLog.Extensions;

public static class ConfigureExtensions
{
    public static IServiceCollection AddEventLog<TDbContext, TEventType, TEntityType>(this IServiceCollection services)
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
    {
        services.AddScoped<IEventLogService<TEventType, TEntityType>, EventLogService<TEventType, TEntityType>>();
        services.AddScoped<IEventLogEntryRepository<TEventType, TEntityType>, EventLogEntryRepository<TDbContext, TEventType, TEntityType>>();
        
        return services;
    }
}
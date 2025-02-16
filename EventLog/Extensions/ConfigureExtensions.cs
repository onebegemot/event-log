using EventLog.Interfaces;
using EventLog.Repository;
using EventLog.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventLog.Extensions;

public static class ConfigureExtensions
{
    public static IServiceCollection ConfigureEventLog<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddScoped<IEventLogService, EventLogService>();
        services.AddScoped<IEventLogEntryRepository, EventLogEntryRepository<TDbContext>>();

        return services;
    }
}
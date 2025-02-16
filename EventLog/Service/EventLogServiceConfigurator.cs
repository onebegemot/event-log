using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Service;

public static class EventLogServiceConfigurator<TEventType, TEntityType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
{
    private static IReadOnlyDictionary<Type, TEntityType> _entityTypes;
    
    public static void Configure<TDbContext>(Action<EventLogConfiguration<TDbContext, TEventType, TEntityType>> configurationBuilder = null)
        where TDbContext : DbContext
    {
        var configuration = CreateDefaultConfiguration<TDbContext>();
        configurationBuilder?.Invoke(configuration);
        
        _entityTypes = configuration.EntityTypes;
        
        TryFillCustomDescriptionTables(configuration);
    }
    
    private static EventLogConfiguration<TDbContext, TEventType, TEntityType> CreateDefaultConfiguration<TDbContext>()
        where TDbContext : DbContext
    {
        var configuration = new EventLogConfiguration<TDbContext, TEventType, TEntityType>();

        ((IEventLogConfigurator<TEventType>)configuration)
            .AddEventStatusDescription(EventStatus.Successful, "Successful")
            .AddEventStatusDescription(EventStatus.HandledException, "Handled exception")
            .AddEventStatusDescription(EventStatus.UnhandledException, "Unhandled exception");

        return configuration;
    }
    
    public static TEntityType GetEntityType<TEntity>(EntityLogInfo<TEntity> logInfo)
        where TEntity : IPkEntity
    {
        if (_entityTypes.TryGetValue(logInfo.GetType(), out var entityType))
            return entityType;
        
        throw new NotImplementedException(
                $"The type {nameof(EntityLogInfo<TEntity>)} cannot be parsed into {nameof(TEntityType)}");
    }
    
    private static void TryFillCustomDescriptionTables<TDbContext>(EventLogConfiguration<TDbContext, TEventType, TEntityType> configuration)
        where TDbContext : DbContext
    {
        var context = configuration.DatabaseContext;

        if (context == null)
            return;

        var eventTypeDescriptions = GetCustomEnumDescriptions<TEventType, EventTypeDescription>(configuration.EventTypeDescription);
        context.Set<EventTypeDescription>().ExecuteDelete();
        context.Set<EventTypeDescription>().AddRange(eventTypeDescriptions);

        var eventStatusDescriptions = GetCustomEnumDescriptions<EventStatus, EventStatusDescription>(configuration.EventStatusDescription);
        context.Set<EventStatusDescription>().ExecuteDelete();
        context.Set<EventStatusDescription>().AddRange(eventStatusDescriptions);
        
        context.SaveChanges();
    }
    
    private static IReadOnlyCollection<TDescriptiveEntity> GetCustomEnumDescriptions<TEnum, TDescriptiveEntity>(
        IReadOnlyDictionary<TEnum, string> enumDescriptions)
            where TDescriptiveEntity : BaseDescriptiveEntity, new()
            where TEnum : struct, Enum
    {
        var enumValues = Enum.GetValues<TEnum>();
        var entities = new List<TDescriptiveEntity>(enumValues.Length);
        
        entities.AddRange(enumValues.Select(value =>
            new TDescriptiveEntity()
            {
                EnumId = Convert.ToInt32(value),
                Description = enumDescriptions?.TryGetValue(value, out var description) ?? false
                    ? description
                    : Enum.GetName(value)
            }));

        return entities;
    }
}
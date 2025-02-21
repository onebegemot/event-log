using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Entities;
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Models.Configurations;

public static class EventLogServiceConfiguration<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    private static IReadOnlyDictionary<Type, TEntityType> _entityTypes;
    private static IReadOnlyDictionary<TPropertyType, IPropertyInfo> _registeredProperties;
    
    public static void Configure<TDbContext>(
        Action<EventLogConfiguration<TDbContext, TEventType, TEntityType, TPropertyType>> configurationBuilder = null)
            where TDbContext : DbContext
    {
        var configuration = new EventLogConfiguration<TDbContext, TEventType, TEntityType, TPropertyType>();
        configurationBuilder?.Invoke(configuration);
        
        _entityTypes = configuration.EntityTypes;
        _registeredProperties = configuration.Properties;
        
        TryFillCustomDescriptionTables(configuration);
    }
    
    public static TEntityType GetEntityType<TEntity>(EntityLogInfo<TEntity, TPropertyType> logInfo)
        where TEntity : IPkEntity
    {
        if (_entityTypes.TryGetValue(logInfo.GetType(), out var entityType))
            return entityType;
        
        throw new NotImplementedException($"The type {nameof(EntityLogInfo<TEntity, TPropertyType>)} cannot be parsed into {nameof(TEntityType)}");
    }
    
    public static PropertyValues GetPropertyInfo<TEntity>(TEntity entity,
        TPropertyType propertyType, Func<TEntity, string, object> getOriginalPropertyValue)
            where TEntity : IPkEntity
    {
        if (_registeredProperties.TryGetValue(propertyType, out var propertyInfo))
        {
            // think about improve here
            if (propertyInfo is PropertyInfo<TEntity> targetPropertyInfo)
            {
                return new PropertyValues(
                    getOriginalPropertyValue(entity, targetPropertyInfo.Name),
                    targetPropertyInfo.Getter(entity));
            }
        }
                
        throw new Exception($"Not found a registered property for the {nameof(TPropertyType)}.{propertyType}");
    }
    
    private static void TryFillCustomDescriptionTables<TDbContext>(
        EventLogConfiguration<TDbContext, TEventType, TEntityType, TPropertyType> configuration)
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
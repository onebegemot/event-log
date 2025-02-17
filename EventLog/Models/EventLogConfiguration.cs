using EventLog.Interfaces;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Models;

public class EventLogConfiguration<TDbContext, TEventType, TEntityType, TPropertyType> :
    IEventLogConfigurator<TEventType>,
    IEntityConfigurator<TEntityType, TPropertyType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescription = new();
    private readonly Dictionary<EventStatus, string> _eventStatusDescription = new();
    private readonly Dictionary<Type, TEntityType> _entityTypes = new();
    private readonly Dictionary<TPropertyType, IPropertyInfo> _properties = new();
    
    private TDbContext _databaseContext;
    
    public IReadOnlyDictionary<TEventType, string> EventTypeDescription => _eventTypeDescription;
    
    public IReadOnlyDictionary<EventStatus, string> EventStatusDescription => _eventStatusDescription;
    
    public IReadOnlyDictionary<Type, TEntityType> EntityTypes => _entityTypes;
    
    public IReadOnlyDictionary<TPropertyType, IPropertyInfo> Properties => _properties;
    
    public TDbContext DatabaseContext => _databaseContext;
    
    public IEventLogConfigurator<TEventType> SetDatabaseContext(TDbContext context)
    {
        _databaseContext = context;
        return this;
    }
    
    IEventLogConfigurator<TEventType> IEventLogConfigurator<TEventType>.AddEventTypeDescription(
        TEventType eventType, string description)
    {
        _eventTypeDescription[eventType] = description;
        return this;
    }
    
    IEventLogConfigurator<TEventType> IEventLogConfigurator<TEventType>.AddEventStatusDescription(
        EventStatus eventStatus, string description)
    {
        _eventStatusDescription[eventStatus] = description;
        return this;
    }
    
    public IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(TEntityType entityType,
        Action<IPropertyConfigurator<TEntity, TPropertyType>> propertyConfigurationBuilder)
            where TEntity : IPkEntity
    {
        _entityTypes[typeof(EntityLogInfo<TEntity, TPropertyType>)] = entityType;

        var propertyConfigurator = new PropertyConfiguration<TEntity, TPropertyType>();
        propertyConfigurationBuilder(propertyConfigurator);

        foreach (var property in propertyConfigurator.Properties)
            _properties[property.Key] = property.Value;
        
        return this;
    }
}
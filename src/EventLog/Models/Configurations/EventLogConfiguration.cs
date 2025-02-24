using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Interfaces.Entities;
using AHSW.EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Models.Configurations;

public class EventLogConfiguration<TDbContext, TEventType, TEntityType, TPropertyType> :
    IEntityConfigurator<TEntityType, TPropertyType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescriptions = new();
    private readonly Dictionary<TEntityType, string> _entityTypeDescriptions = new();
    private readonly Dictionary<TPropertyType, string> _propertyTypeDescriptions = new();
    private readonly Dictionary<EventStatus, string> _eventStatusDescriptions = new();
    private readonly Dictionary<Type, TEntityType> _entityTypes = new();
    private readonly Dictionary<TPropertyType, IPropertyInfo> _properties = new();
    
    private TDbContext _databaseContext;
    
    public IReadOnlyDictionary<TEventType, string> EventTypeDescriptions => _eventTypeDescriptions;
    
    public IReadOnlyDictionary<TEntityType, string> EntityTypeDescriptions => _entityTypeDescriptions;
    
    public IReadOnlyDictionary<TPropertyType, string> PropertyTypeDescriptions => _propertyTypeDescriptions;
    
    public IReadOnlyDictionary<EventStatus, string> EventStatusDescriptions => _eventStatusDescriptions;
    
    public IReadOnlyDictionary<Type, TEntityType> EntityTypes => _entityTypes;
    
    public IReadOnlyDictionary<TPropertyType, IPropertyInfo> Properties => _properties;
    
    public TDbContext DatabaseContext => _databaseContext;
    
    public IEntityConfigurator<TEntityType, TPropertyType> UseCustomTypeDescriptions(TDbContext context,
        Action<ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType>> optionsBuilder)
    {
        _databaseContext = context;

        var configurator = CreateDefaultTypeDescriptionsConfiguration();
        optionsBuilder(configurator);
        
        foreach (var pair in configurator.EventTypeDescriptions)
            _eventTypeDescriptions[pair.Key] = pair.Value;
        
        foreach (var pair in configurator.EntityTypeDescriptions)
            _entityTypeDescriptions[pair.Key] = pair.Value;
        
        foreach (var pair in configurator.PropertyTypeDescriptions)
            _propertyTypeDescriptions[pair.Key] = pair.Value;
        
        foreach (var pair in configurator.EventStatusDescriptions)
            _eventStatusDescriptions[pair.Key] = pair.Value;
        
        return this;
    }
    
    public IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(
        TEntityType entityType, Action<IPropertyConfigurator<TEntity, TPropertyType>> optionsBuilder)
            where TEntity : IPkEntity
    {
        _entityTypes[typeof(EntityLogInfo<TEntity, TPropertyType>)] = entityType;

        var configurator = new PropertyConfiguration<TEntity, TPropertyType>();
        optionsBuilder(configurator);

        foreach (var property in configurator.Properties)
            _properties[property.Key] = property.Value;
        
        return this;
    }
    
    private TypeDescriptionsConfiguration<TEventType, TEntityType, TPropertyType> CreateDefaultTypeDescriptionsConfiguration()
    {
        var configurator = new TypeDescriptionsConfiguration<TEventType, TEntityType, TPropertyType>();
        
        configurator
            .AddEventStatusDescription(EventStatus.Successful, "Successful")
            .AddEventStatusDescription(EventStatus.HandledException, "Handled exception")
            .AddEventStatusDescription(EventStatus.UnhandledException, "Unhandled exception");

        return configurator;
    }
}
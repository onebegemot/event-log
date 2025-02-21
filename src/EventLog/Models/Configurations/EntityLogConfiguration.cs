using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Interfaces.Entities;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Entities.Abstract;
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Models.Configurations;

public class EntityLogConfiguration<TEventType, TEntityType, TPropertyType> :
    IEntityLogConfigurator<TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly List<LogEntityUnit<TEventType, TEntityType, TPropertyType>> _logEntityUnits = new();

    public IReadOnlyCollection<LogEntityUnit<TEventType, TEntityType, TPropertyType>> LogEntityUnits => _logEntityUnits;
    
    public IEntityLogConfigurator<TEventType, TEntityType, TPropertyType> AddEntityLogging<TEntity>(
        Func<TEntity, string, object> getOriginalPropertyValue, IEnumerable<TEntity> entities,
        params TPropertyType[] propertyTypes)
            where TEntity : IPkEntity
    {
        ArgumentNullException.ThrowIfNull(getOriginalPropertyValue);
        ArgumentNullException.ThrowIfNull(entities);
        ArgumentNullException.ThrowIfNull(propertyTypes);
        
        var logEntityUnits = GetLogEntities(getOriginalPropertyValue,
            new EntityLogInfo<TEntity, TPropertyType>(entities, propertyTypes));
        
        _logEntityUnits.AddRange(logEntityUnits);
        
        return this;
    }
    
    private static IEnumerable<LogEntityUnit<TEventType, TEntityType, TPropertyType>> GetLogEntities<TEntity>(
        Func<TEntity, string, object> getOriginalPropertyValue, EntityLogInfo<TEntity, TPropertyType> logInfo)
            where TEntity : IPkEntity
    {
        var entityType = EventLogServiceConfiguration<TEventType, TEntityType, TPropertyType>.GetEntityType(logInfo);

        return logInfo.Entities
            .Select(entity =>
            {
                var entityLogEntry = new EntityLogEntry<TEventType, TEntityType, TPropertyType>()
                {
                    ActionType = entity.Id == 0
                        ? ActionType.Create
                        : ActionType.Update,
                    EntityType = entityType
                };

                if (logInfo.Properties != null)
                {
                    foreach (var property in logInfo.Properties)
                        AddPropertyLogEntries(entity, property, getOriginalPropertyValue, entityLogEntry);
                }

                return new LogEntityUnit<TEventType, TEntityType, TPropertyType>(entity, entityLogEntry);
            })
            .ToList();
    }
    
    private static void AddPropertyLogEntries<TEntity>(TEntity entity, TPropertyType property,
        Func<TEntity, string, object> getOriginalPropertyValue, EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry)
            where TEntity : IPkEntity
    {
        var propertyValues = EventLogServiceConfiguration<TEventType, TEntityType, TPropertyType>.GetPropertyInfo(
            entity, property, getOriginalPropertyValue);

        var indicativeProperty = propertyValues.New ?? propertyValues.Original;
        
        if (indicativeProperty == null)
            return;
        
        switch (indicativeProperty)
        {
            case bool:
                TryCreateAndAddPropertyLogEntry<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>, bool>(
                    property, (bool)propertyValues.Original, (bool)propertyValues.New, entityLogEntry,
                    entityLogEntry.BoolPropertyLogEntries != null,
                    x => entityLogEntry.BoolPropertyLogEntries = x,
                    x => entityLogEntry.BoolPropertyLogEntries.Add(x));
                
                break;
            
            case string:
                TryCreateAndAddPropertyLogEntry<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>, string>(
                    property, (string)propertyValues.Original, (string)propertyValues.New, entityLogEntry,
                    entityLogEntry.StringPropertyLogEntries != null,
                    x => entityLogEntry.StringPropertyLogEntries = x,
                    x => entityLogEntry.StringPropertyLogEntries.Add(x));
                
                break;
            
            case int:
                TryCreateAndAddPropertyLogEntry<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>, int>(
                    property, (int)propertyValues.Original, (int)propertyValues.New, entityLogEntry,
                    entityLogEntry.Int32PropertyLogEntries != null,
                    x => entityLogEntry.Int32PropertyLogEntries = x,
                    x => entityLogEntry.Int32PropertyLogEntries.Add(x));
                
                break;
            
            case decimal:
                TryCreateAndAddPropertyLogEntry<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>, decimal>(
                    property, (decimal)propertyValues.Original, (decimal)propertyValues.New, entityLogEntry,
                    entityLogEntry.DecimalPropertyLogEntries != null,
                    x => entityLogEntry.DecimalPropertyLogEntries = x,
                    x => entityLogEntry.DecimalPropertyLogEntries.Add(x));
                
                break;
            
            default:
                throw new NotImplementedException(nameof(property));
        }
    }
    
    private static void TryCreateAndAddPropertyLogEntry<TPropertyLogEntry, TLogValue>(
        TPropertyType propertyType, TLogValue originalValue, TLogValue newValue,
        EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry, bool isLogInitialized,
        Action<ICollection<TPropertyLogEntry>> collectionSetter,Action<TPropertyLogEntry> addItem)
        where TPropertyLogEntry : PropertyLogEntry<TLogValue, TEventType, TEntityType, TPropertyType>, new()
    {
        if (!IsNewEntity() && newValue != null && newValue.Equals(originalValue))
            return;
        
        var propertyLogEntry = new TPropertyLogEntry
        {
            PropertyType = propertyType,
            Value = newValue,
            EntityLogEntry = entityLogEntry
        };
        
        if (!isLogInitialized)
            collectionSetter(new List<TPropertyLogEntry>());

        addItem(propertyLogEntry);

        return;

        bool IsNewEntity() => entityLogEntry.ActionType == ActionType.Create;
    }
}
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
        Func<TPropertyType[]> getObservableProperties)
            where TEntity : IPkEntity
    {
        ArgumentNullException.ThrowIfNull(getOriginalPropertyValue);
        ArgumentNullException.ThrowIfNull(entities);
        ArgumentNullException.ThrowIfNull(getObservableProperties);
        
        var logEntityUnits = GetLogEntities(getOriginalPropertyValue,
            new EntityLogInfo<TEntity, TPropertyType>(entities, getObservableProperties()));
        
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

        if (indicativeProperty is Enum)
            indicativeProperty = NullableCast<int>(indicativeProperty);
        
        switch (indicativeProperty)
        {
            case bool:
                TryCreateAndAddPropertyLogEntry<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>, bool?>(
                    property, NullableCast<bool>(propertyValues.Original), NullableCast<bool>(propertyValues.New), entityLogEntry,
                    entityLogEntry.BoolPropertyLogEntries != null,
                    x => entityLogEntry.BoolPropertyLogEntries = x,
                    x => entityLogEntry.BoolPropertyLogEntries.Add(x));
                
                break;
            
            case DateTime:
                TryCreateAndAddPropertyLogEntry<DateTimePropertyLogEntry<TEventType, TEntityType, TPropertyType>, DateTime?>(
                    property, NullableCast<DateTime>(propertyValues.Original), NullableCast<DateTime>(propertyValues.New), entityLogEntry,
                    entityLogEntry.DateTimePropertyLogEntries != null,
                    x => entityLogEntry.DateTimePropertyLogEntries = x,
                    x => entityLogEntry.DateTimePropertyLogEntries.Add(x));
                
                break;
            
            case string:
                TryCreateAndAddPropertyLogEntry<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>, string>(
                    property, (string)propertyValues.Original, (string)propertyValues.New, entityLogEntry,
                    entityLogEntry.StringPropertyLogEntries != null,
                    x => entityLogEntry.StringPropertyLogEntries = x,
                    x => entityLogEntry.StringPropertyLogEntries.Add(x));
                
                break;
            
            case int:
                TryCreateAndAddPropertyLogEntry<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>, int?>(
                    property, NullableCast<int>(propertyValues.Original), NullableCast<int>(propertyValues.New), entityLogEntry,
                    entityLogEntry.Int32PropertyLogEntries != null,
                    x => entityLogEntry.Int32PropertyLogEntries = x,
                    x => entityLogEntry.Int32PropertyLogEntries.Add(x));
                
                break;
            
            case double:
                TryCreateAndAddPropertyLogEntry<DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType>, double?>(
                    property, NullableCast<double>(propertyValues.Original), NullableCast<double>(propertyValues.New), entityLogEntry,
                    entityLogEntry.DoublePropertyLogEntries != null,
                    x => entityLogEntry.DoublePropertyLogEntries = x,
                    x => entityLogEntry.DoublePropertyLogEntries.Add(x));
                
                break;
            
            case decimal:
                TryCreateAndAddPropertyLogEntry<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>, decimal?>(
                    property, (decimal?)propertyValues.Original, (decimal?)propertyValues.New, entityLogEntry,
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
    
    private static TValue? NullableCast<TValue>(object value)
        where TValue : struct =>
            value != null ? (TValue)value : null;
}
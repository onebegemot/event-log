using AHSW.EventLog.Models.Entities.Abstract;
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Models.Entities;

public class EntityLogEntry<TEventType, TEntityType, TPropertyType> : PkEntity
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    public ActionType ActionType { get; set; }
    
    public TEntityType EntityType { get; set; }

    public int EntityId { get; set; }
    
    public int EventLogEntryId { get; set; }
    
    public EventLogEntry<TEventType, TEntityType, TPropertyType> EventLogEntry { get; set; }
    
    public ICollection<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>> BoolPropertyLogEntries { get; set; }
    
    public ICollection<DateTimePropertyLogEntry<TEventType, TEntityType, TPropertyType>> DateTimePropertyLogEntries { get; set; }
    
    public ICollection<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>> StringPropertyLogEntries { get; set; }
    
    public ICollection<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>> Int32PropertyLogEntries { get; set; }
    
    public ICollection<DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType>> DoublePropertyLogEntries { get; set; }
    
    public ICollection<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>> DecimalPropertyLogEntries { get; set; }

    public bool HasPropertyLogEntries =>
        (BoolPropertyLogEntries != null && BoolPropertyLogEntries.Any()) ||
        (DateTimePropertyLogEntries != null && DateTimePropertyLogEntries.Any()) ||
        (StringPropertyLogEntries != null && StringPropertyLogEntries.Any()) ||
        (Int32PropertyLogEntries != null && Int32PropertyLogEntries.Any()) ||
        (DoublePropertyLogEntries != null && DoublePropertyLogEntries.Any()) ||
        (DecimalPropertyLogEntries != null && DecimalPropertyLogEntries.Any());
}
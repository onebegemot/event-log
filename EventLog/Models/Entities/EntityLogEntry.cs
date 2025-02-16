using EventLog.Entities.Abstracts;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;

namespace EventLog.Models.Entities;

public class EntityLogEntry<TEventType, TEntityType> : PkEntity
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
{
    public ActionType ActionType { get; set; }
    
    public TEntityType EntityType { get; set; }

    public int EntityId { get; set; }
    
    public int EventLogEntryId { get; set; }
    
    public EventLogEntry<TEventType, TEntityType> EventLogEntry { get; set; }
    
    public ICollection<BoolPropertyLogEntry<TEventType, TEntityType>> BoolPropertyLogEntries { get; set; }
    
    public ICollection<StringPropertyLogEntry<TEventType, TEntityType>> StringPropertyLogEntries { get; set; }
    
    public ICollection<Int32PropertyLogEntry<TEventType, TEntityType>> Int32PropertyLogEntries { get; set; }
    
    public ICollection<DecimalPropertyLogEntry<TEventType, TEntityType>> DecimalPropertyLogEntries { get; set; }

    public bool HasPropertyLogEntries =>
        (BoolPropertyLogEntries != null && BoolPropertyLogEntries.Any()) ||
        (StringPropertyLogEntries != null && StringPropertyLogEntries.Any()) ||
        (Int32PropertyLogEntries != null && Int32PropertyLogEntries.Any()) ||
        (DecimalPropertyLogEntries != null && DecimalPropertyLogEntries.Any());
}
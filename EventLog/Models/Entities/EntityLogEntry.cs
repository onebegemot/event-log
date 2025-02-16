using EventLog.Entities.Abstracts;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;

namespace EventLog.Models.Entities;

public class EntityLogEntry<TEventType> : PkEntity
    where TEventType : struct, Enum
{
    public ActionType ActionType { get; set; }
    
    public EntityType EntityType { get; set; }

    public int EntityId { get; set; }
    
    public int EventLogEntryId { get; set; }
    
    public EventLogEntry<TEventType> EventLogEntry { get; set; }
    
    public ICollection<BoolPropertyLogEntry<TEventType>> BoolPropertyLogEntries { get; set; }
    
    public ICollection<StringPropertyLogEntry<TEventType>> StringPropertyLogEntries { get; set; }
    
    public ICollection<Int32PropertyLogEntry<TEventType>> Int32PropertyLogEntries { get; set; }
    
    public ICollection<DecimalPropertyLogEntry<TEventType>> DecimalPropertyLogEntries { get; set; }

    public bool HasPropertyLogEntries =>
        (BoolPropertyLogEntries != null && BoolPropertyLogEntries.Any()) ||
        (StringPropertyLogEntries != null && StringPropertyLogEntries.Any()) ||
        (Int32PropertyLogEntries != null && Int32PropertyLogEntries.Any()) ||
        (DecimalPropertyLogEntries != null && DecimalPropertyLogEntries.Any());
}
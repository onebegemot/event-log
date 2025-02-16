using EventLog.Entities.Abstracts;
using EventLog.Models.Enums;

namespace EventLog.Models.Entities.Abstract;

public abstract class PropertyLogEntry<T, TEventType, TEntityType> : PkEntity
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
{
    public PropertyType PropertyType { get; set; }
    
    public T Value { get; set; }
    
    public int EntityLogEntryId { get; set; }

    public EntityLogEntry<TEventType, TEntityType> EntityLogEntry { get; set; }
}
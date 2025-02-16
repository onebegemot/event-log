using EventLog.Entities.Abstracts;
using EventLog.Models.Enums;

namespace EventLog.Models.Entities.Abstract;

public abstract class PropertyLogEntry<T> : PkEntity
{
    public PropertyType PropertyType { get; set; }
    
    public T Value { get; set; }
    
    public int EntityLogEntryId { get; set; }

    public EntityLogEntry EntityLogEntry { get; set; }
}
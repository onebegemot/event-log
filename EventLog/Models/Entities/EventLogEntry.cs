using System.ComponentModel.DataAnnotations.Schema;
using EventLog.Models.Entities.Abstract;
using EventLog.Models.Enums;

namespace EventLog.Models.Entities;

public class EventLogEntry<TEventType, TEntityType, TPropertyType> : ReadOnlyEntity
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    public TEventType EventType { get; set; }
    
    public ICollection<EntityLogEntry<TEventType, TEntityType, TPropertyType>> EntityLogEntries { get; set; }

    // public User User { get; set; }
    
    public DateTime CompletedAt { get; set; }
    
    public string Details { get; set; }
    
    public EventStatus? Status { get; set; }
    
    public string FailureDetails { get; set; }
    
    /// <summary>
    /// Contains True if an exception thrown explicitly after logging the event in order to return an appropriate response to a client
    /// In this case IEventLogService does not save the additional unhandled exception into the model
    /// </summary>
    [NotMapped]
    public bool ExplicitlyThrownException { get; set; }
}
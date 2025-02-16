using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class Int32PropertyLogEntry<TEventType> : PropertyLogEntry<int, TEventType>
    where TEventType : struct, Enum
{
}
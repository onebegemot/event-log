using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class BoolPropertyLogEntry<TEventType> : PropertyLogEntry<bool, TEventType>
    where TEventType : struct, Enum
{
}
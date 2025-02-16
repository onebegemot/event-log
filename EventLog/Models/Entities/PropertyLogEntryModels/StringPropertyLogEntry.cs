using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class StringPropertyLogEntry<TEventType> : PropertyLogEntry<string, TEventType>
    where TEventType : struct, Enum
{
}
using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class StringPropertyLogEntry<TEventType, TEntityType> :
    PropertyLogEntry<string, TEventType, TEntityType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
}
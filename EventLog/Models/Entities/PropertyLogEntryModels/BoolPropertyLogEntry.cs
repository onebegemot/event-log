using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class BoolPropertyLogEntry<TEventType, TEntityType> :
    PropertyLogEntry<bool, TEventType, TEntityType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
}
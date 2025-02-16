using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class Int32PropertyLogEntry<TEventType, TEntityType> :
    PropertyLogEntry<int, TEventType, TEntityType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
}
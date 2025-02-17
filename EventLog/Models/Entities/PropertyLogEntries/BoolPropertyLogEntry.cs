using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntries;

public class BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType> :
    PropertyLogEntry<bool, TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
}
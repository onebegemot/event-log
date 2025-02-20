using AHWS.EventLog.Models.Entities.Abstract;

namespace AHWS.EventLog.Models.Entities.PropertyLogEntries;

public class StringPropertyLogEntry<TEventType, TEntityType, TPropertyType> :
    PropertyLogEntry<string, TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
}
using AHWS.EventLog.Models.Entities.Abstract;

namespace AHWS.EventLog.Models.Entities.PropertyLogEntries;

public class DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType> :
    PropertyLogEntry<decimal, TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
}
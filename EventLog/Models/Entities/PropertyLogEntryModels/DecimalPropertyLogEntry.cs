using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class DecimalPropertyLogEntry<TEventType, TEntityType> :
    PropertyLogEntry<decimal, TEventType, TEntityType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
}
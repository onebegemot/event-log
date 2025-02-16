using EventLog.Models.Entities.Abstract;

namespace EventLog.Models.Entities.PropertyLogEntryModels;

public class DecimalPropertyLogEntry<TEventType> : PropertyLogEntry<decimal, TEventType>
    where TEventType : struct, Enum
{
}
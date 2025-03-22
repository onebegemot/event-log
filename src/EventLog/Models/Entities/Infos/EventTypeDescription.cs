using AHSW.EventLog.Models.Entities.Abstract;

namespace AHSW.EventLog.Models.Entities;

public class EventTypeDescription<TEventType> :
    BaseDescriptiveEntity<TEventType>
        where TEventType : struct, Enum
{
}
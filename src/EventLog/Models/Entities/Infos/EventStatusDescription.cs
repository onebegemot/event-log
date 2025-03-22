using AHSW.EventLog.Models.Entities.Abstract;

namespace AHSW.EventLog.Models.Entities;

public class EventStatusDescription<TEventStatus> :
    BaseDescriptiveEntity<TEventStatus>
        where TEventStatus : struct, Enum
{
}
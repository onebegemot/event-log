using AHSW.EventLog.Interfaces.Entities;
using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Models;

public class LogEntityUnit<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    private readonly IPkEntity _entity;
    private readonly EntityLogEntry<TEventType, TEntityType, TPropertyType> _entityLogEntry;

    public LogEntityUnit(IPkEntity entity, EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry)
    {
        _entity = entity;
        _entityLogEntry = entityLogEntry;
    }

    public EntityLogEntry<TEventType, TEntityType, TPropertyType> GetEntityLogEntry()
    {
        _entityLogEntry.EntityId = _entity.Id;
        return _entityLogEntry;
    }
}
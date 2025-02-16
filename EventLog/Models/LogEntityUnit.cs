using EventLog.Interfaces;
using EventLog.Models.Entities;

namespace EventLog.Models;

public class LogEntityUnit<TEventType, TEntityType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
{
    private readonly IPkEntity _entity;
    private readonly EntityLogEntry<TEventType, TEntityType> _entityLogEntry;

    public LogEntityUnit(IPkEntity entity, EntityLogEntry<TEventType, TEntityType> entityLogEntry)
    {
        _entity = entity;
        _entityLogEntry = entityLogEntry;
    }

    public EntityLogEntry<TEventType, TEntityType> GetEntityLogEntry()
    {
        _entityLogEntry.EntityId = _entity.Id;
        return _entityLogEntry;
    }
}
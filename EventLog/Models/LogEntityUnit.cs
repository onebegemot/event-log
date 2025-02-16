using EventLog.Interfaces;
using EventLog.Models.Entities;

namespace EventLog.Models;

public class LogEntityUnit<TEventType>
    where TEventType : struct, Enum
{
    private readonly IPkEntity _entity;
    private readonly EntityLogEntry<TEventType> _entityLogEntry;

    public LogEntityUnit(IPkEntity entity, EntityLogEntry<TEventType> entityLogEntry)
    {
        _entity = entity;
        _entityLogEntry = entityLogEntry;
    }

    public EntityLogEntry<TEventType> GetEntityLogEntry()
    {
        _entityLogEntry.EntityId = _entity.Id;
        return _entityLogEntry;
    }
}
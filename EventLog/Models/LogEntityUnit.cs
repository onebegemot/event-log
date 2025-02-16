using EventLog.Interfaces;
using EventLog.Models.Entities;

namespace EventLog.Models;

public class LogEntityUnit
{
    private readonly IPkEntity _entity;
    private readonly EntityLogEntry _entityLogEntry;

    public LogEntityUnit(IPkEntity entity, EntityLogEntry entityLogEntry)
    {
        _entity = entity;
        _entityLogEntry = entityLogEntry;
    }

    public EntityLogEntry GetEntityLogEntry()
    {
        _entityLogEntry.EntityId = _entity.Id;
        return _entityLogEntry;
    }
}
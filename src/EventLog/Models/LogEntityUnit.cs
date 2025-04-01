using AHSW.EventLog.Models.Configurations;
using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Models;

public class LogEntityUnit<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    private readonly object _entity;
    private readonly EntityLogEntry<TEventType, TEntityType, TPropertyType> _entityLogEntry;

    public LogEntityUnit(object entity, EntityLogEntry<TEventType, TEntityType, TPropertyType> entityLogEntry)
    {
        _entity = entity;
        _entityLogEntry = entityLogEntry;
    }

    public EntityLogEntry<TEventType, TEntityType, TPropertyType> GetEntityLogEntry()
    {
        _entityLogEntry.EntityId = EventLogServiceConfiguration<TEventType, TEntityType, TPropertyType>.GetEntityId(_entity);
        return _entityLogEntry;
    }
}
using AHSW.EventLog.Interfaces.Entities;
using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Interfaces;

public interface IEventLogRepository
{
    Task AddOrUpdateEventLogAsync<TEventType, TEntityType, TPropertyType>(
        EventLogEntry<TEventType, TEntityType, TPropertyType> entity,
        CancellationToken cancellationToken = default)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
            where TPropertyType : struct, Enum;

    object GetOriginalPropertyValue<TEntity>(TEntity entity, string propertyName)
        where TEntity : IPkEntity;
}
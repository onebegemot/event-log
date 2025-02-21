using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Interfaces;

public interface IEventLogEntryRepository<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    Task AddOrUpdateAsync(EventLogEntry<TEventType, TEntityType, TPropertyType> entity,
        CancellationToken cancellationToken = default);
}
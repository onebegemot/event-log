using EventLog.Models.Entities;

namespace EventLog.Interfaces;

public interface IEventLogEntryRepository<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    Task<EventLogEntry<TEventType, TEntityType, TPropertyType>> GetByIdAsync(
        int id, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateAsync(EventLogEntry<TEventType, TEntityType, TPropertyType> entity,
        int? initiatorId, CancellationToken cancellationToken = default);
}
using EventLog.Models.Entities;

namespace EventLog.Interfaces;

public interface IEventLogEntryRepository<TEventType, TEntityType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
{
    Task<EventLogEntry<TEventType, TEntityType>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateAsync(EventLogEntry<TEventType, TEntityType> entity, int? initiatorId, CancellationToken cancellationToken = default);
}
using EventLog.Models.Entities;

namespace EventLog.Interfaces;

public interface IEventLogEntryRepository<TEventType>
    where TEventType : struct, Enum
{
    Task<EventLogEntry<TEventType>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateAsync(EventLogEntry<TEventType> entity, int? initiatorId, CancellationToken cancellationToken = default);
}
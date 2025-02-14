using EventLog.Models.Entities;

namespace EventLog.Interfaces;

public interface IEventLogEntryRepository
{
    Task<EventLogEntry> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateAsync(EventLogEntry entity, int? initiatorId, CancellationToken cancellationToken = default);
}
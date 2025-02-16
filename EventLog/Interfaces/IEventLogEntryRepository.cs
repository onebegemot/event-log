using EventLog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Interfaces;

public interface IEventLogEntryRepository
{
    Task<EventLogEntry> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task AddOrUpdateAsync(EventLogEntry entity, int? initiatorId, CancellationToken cancellationToken = default);
}
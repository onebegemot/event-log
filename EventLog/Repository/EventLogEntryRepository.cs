using EventLog.DbContext;
using EventLog.Interfaces;
using EventLog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Repository;

public class EventLogEntryRepository : IEventLogEntryRepository
{
    private readonly EventLogDbContext _dbContext;

    public EventLogEntryRepository(EventLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EventLogEntry> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<EventLogEntry>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddOrUpdateAsync(EventLogEntry entity, int? initiatorId,
        CancellationToken cancellationToken = default)
    {
        if (IsNew())
        {
            entity.CreatedBy = initiatorId;
            await _dbContext.Set<EventLogEntry>().AddAsync(entity, cancellationToken);
        }
        
        entity.CompletedAt = DateTime.UtcNow;
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
}
using EventLog.Interfaces;
using EventLog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Repository;

public class EventLogEntryRepository<TDbContext, TEventType, TEntityType> :
    IEventLogEntryRepository<TEventType, TEntityType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
    private readonly TDbContext _dbContext;

    public EventLogEntryRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EventLogEntry<TEventType, TEntityType>> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<EventLogEntry<TEventType, TEntityType>>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddOrUpdateAsync(EventLogEntry<TEventType, TEntityType> entity, int? initiatorId,
        CancellationToken cancellationToken = default)
    {
        if (IsNew())
        {
            entity.CreatedBy = initiatorId;
            await _dbContext.Set<EventLogEntry<TEventType, TEntityType>>().AddAsync(entity, cancellationToken);
        }
        
        entity.CompletedAt = DateTime.UtcNow;
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
}
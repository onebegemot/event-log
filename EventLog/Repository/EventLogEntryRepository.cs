using EventLog.Interfaces;
using EventLog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Repository;

public class EventLogEntryRepository<TDbContext, TEventType, TEntityType, TPropertyType> :
    IEventLogEntryRepository<TEventType, TEntityType, TPropertyType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly TDbContext _dbContext;

    public EventLogEntryRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EventLogEntry<TEventType, TEntityType, TPropertyType>> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<EventLogEntry<TEventType, TEntityType, TPropertyType>>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddOrUpdateAsync(EventLogEntry<TEventType, TEntityType, TPropertyType> entity, int? initiatorId,
        CancellationToken cancellationToken = default)
    {
        if (IsNew())
        {
            entity.CreatedBy = initiatorId;
            await _dbContext.Set<EventLogEntry<TEventType, TEntityType, TPropertyType>>().AddAsync(entity, cancellationToken);
        }
        
        entity.CompletedAt = DateTime.UtcNow;
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
}
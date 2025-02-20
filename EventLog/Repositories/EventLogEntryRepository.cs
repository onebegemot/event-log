using AHWS.EventLog.Interfaces;
using AHWS.EventLog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AHWS.EventLog.Repositories;

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
    
    public async Task AddOrUpdateAsync(EventLogEntry<TEventType, TEntityType, TPropertyType> entity,
        CancellationToken cancellationToken = default)
    {
        if (IsNew())
        {
            await _dbContext
                .Set<EventLogEntry<TEventType, TEntityType, TPropertyType>>()
                .AddAsync(entity, cancellationToken);
        }
        
        entity.CompletedAt = DateTime.UtcNow;
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
}
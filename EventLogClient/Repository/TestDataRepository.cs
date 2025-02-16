using EventLog.DatabaseContext;
using EventLog.Interfaces;

namespace EventLog.Repository;

public class TestDataRepository : ITestDataRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestDataRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddOrUpdateAsync(ApplicationEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (IsNew())
            await _dbContext.Set<ApplicationEntity>().AddAsync(entity, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        bool IsNew() => entity.Id == 0;
    }
    
    public object GetOriginalPropertyValue(ApplicationEntity entity, string propertyName) =>
        _dbContext.Entry(entity).Property(propertyName).OriginalValue;
}
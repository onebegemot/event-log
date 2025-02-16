using EventLog.DbContext;

namespace EventLog.Interfaces;

public interface ITestDataRepository
{
    Task AddOrUpdateAsync(ApplicationEntity entity,
        CancellationToken cancellationToken = default);
    
    object GetOriginalPropertyValue(ApplicationEntity entity, string propertyName);
}
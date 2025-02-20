using AHWS.EventLog.Interfaces.Entities;

namespace EventLog.Repository.Interfaces;

public interface IBaseRepository<in TEntity>
    where TEntity : class, IPkEntity
{
    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    object GetOriginalPropertyValue(TEntity entity, string propertyName);
}
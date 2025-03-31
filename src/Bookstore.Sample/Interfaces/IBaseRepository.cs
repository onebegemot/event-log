using AHSW.EventLog.Interfaces.Entities;

namespace Bookstore.Sample.Interfaces;

internal interface IBaseRepository<in TEntity>
    where TEntity : class, IPkEntity
{
    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
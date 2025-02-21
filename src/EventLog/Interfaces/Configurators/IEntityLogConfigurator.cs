using AHSW.EventLog.Interfaces.Entities;

namespace AHSW.EventLog.Interfaces.Configurators;

public interface IEntityLogConfigurator<TEventType, TEntityType, in TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityLogConfigurator<TEventType, TEntityType, TPropertyType> AddEntityLogging<TEntity>(
        Func<TEntity, string, object> getOriginalPropertyValue, IEnumerable<TEntity> entities,
        params TPropertyType[] propertyTypes)
            where TEntity : IPkEntity;
}
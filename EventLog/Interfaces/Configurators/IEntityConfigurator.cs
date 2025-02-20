using AHWS.EventLog.Interfaces.Entities;

namespace AHWS.EventLog.Interfaces.Configurators;

public interface IEntityConfigurator<in TEntityType, in TPropertyType>
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(
        TEntityType entityType, Action<IPropertyConfigurator<TEntity, TPropertyType>> optionsBuilder)
            where TEntity : IPkEntity;
}
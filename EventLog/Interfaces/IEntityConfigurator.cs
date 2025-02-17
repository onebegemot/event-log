namespace EventLog.Interfaces;

public interface IEntityConfigurator<in TEntityType, TPropertyType>
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(TEntityType entityType,
        Action<IPropertyConfigurator<TEntity, TPropertyType>> propertyConfigurationBuilder)
        where TEntity : IPkEntity;
}
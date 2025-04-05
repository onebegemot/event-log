using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Interfaces.Configurators;

public interface IEventLogConfigurator<in TEventType, in TEntityType, in TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    IEntityConfigurator<TEntityType, TPropertyType> UseCustomTypeDescriptions(DbContext context,
        Action<ITypeDescriptionsConfigurator<TEventType, TEntityType, TPropertyType>> optionsBuilder);

    IEntityConfigurator<TEntityType, TPropertyType> RegisterEntity<TEntity>(
        TEntityType entityType, Func<object, int> getId,
        Action<IPropertyConfigurator<TEntity, TPropertyType>> optionsBuilder)
        where TEntity : class;
}
using AHWS.EventLog.Interfaces.Entities;

namespace AHWS.EventLog.Interfaces.Configurators;

public interface IPropertyConfigurator<out TEntity, in TPropertyType>
    where TEntity : IPkEntity
    where TPropertyType : struct, Enum
{
    IPropertyConfigurator<TEntity, TPropertyType> RegisterProperty(TPropertyType property,
        Func<TEntity, object> propertyGetter, string propertyName);
}
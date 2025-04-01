namespace AHSW.EventLog.Interfaces.Configurators;

public interface IPropertyConfigurator<out TEntity, in TPropertyType>
    where TEntity : class
    where TPropertyType : struct, Enum
{
    IPropertyConfigurator<TEntity, TPropertyType> RegisterProperty(TPropertyType property,
        Func<TEntity, object> propertyGetter, string propertyName);
}
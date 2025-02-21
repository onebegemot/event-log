using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Interfaces.Configurators;
using AHSW.EventLog.Interfaces.Entities;

namespace AHSW.EventLog.Models.Configurations;

public class PropertyConfiguration<TEntity, TPropertyType> :
    IPropertyConfigurator<TEntity, TPropertyType>
        where TEntity : IPkEntity
        where TPropertyType : struct, Enum
{
    private readonly Dictionary<TPropertyType, IPropertyInfo> _properties = new();

    public IReadOnlyDictionary<TPropertyType, IPropertyInfo> Properties => _properties;
    
    public IPropertyConfigurator<TEntity, TPropertyType> RegisterProperty(TPropertyType property,
        Func<TEntity, object> propertyGetter, string propertyName)
    {
        _properties[property] = new PropertyInfo<TEntity>(propertyGetter, propertyName);
        return this;
    }
}
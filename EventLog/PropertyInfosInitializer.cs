using EventLog.DatabaseContext;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Enums;

namespace EventLog;

internal static class PropertyInfosInitializer
{
    // Register property infos
    public static PropertyValues GetPropertyInfo<TEntity>(TEntity entity, PropertyType propertyType,
        Func<TEntity, string, object> getOriginalPropertyValue)
            where TEntity : IPkEntity =>
                entity switch
                {
                    ApplicationEntity debtor => ApplicationTestDataEntity.TryGetValue(propertyType, out var propertyInfo)
                        ? new PropertyValues(
                            getOriginalPropertyValue(entity, propertyInfo.Name),
                            propertyInfo.Getter(debtor))
                        : throw new Exception($"Not found a property in the {nameof(PropertyInfosInitializer)} dictionary for the {nameof(PropertyType)}.{propertyType}"),
                    
                    _ => throw new NotImplementedException($"Entity type {nameof(TEntity)} was not recognized")
                };

    
    
    // Register client entity property and name getters
    public static IReadOnlyDictionary<IPkEntity, IDictionary<PropertyType, PropertyInfo<IPkEntity>>> RegisteredPropertyGetters { get; }

    
    private static IDictionary<PropertyType, PropertyInfo<ApplicationEntity>> ApplicationTestDataEntity =
        new Dictionary<PropertyType, PropertyInfo<ApplicationEntity>>()
        {
            { 
                PropertyType.ApplicationEntityTestDate,
                new PropertyInfo<ApplicationEntity>(x => x.TestDate, nameof(ApplicationEntity.TestDate))
            },
            { 
                PropertyType.ApplicationEntityTestString,
                new PropertyInfo<ApplicationEntity>(x => x.TestString, nameof(ApplicationEntity.TestString))
            },
            { 
                PropertyType.ApplicationEntityTestBool,
                new PropertyInfo<ApplicationEntity>(x => x.TestBool, nameof(ApplicationEntity.TestBool))
            },
            { 
                PropertyType.ApplicationEntityTestInt32,
                new PropertyInfo<ApplicationEntity>(x => x.TestInt32, nameof(ApplicationEntity.TestInt32))
            },
        };
}
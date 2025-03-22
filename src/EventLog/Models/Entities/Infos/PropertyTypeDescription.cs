using AHSW.EventLog.Models.Entities.Abstract;

namespace AHSW.EventLog.Models.Entities;

public class PropertyTypeDescription<TPropertyType> :
    BaseDescriptiveEntity<TPropertyType>
        where TPropertyType : struct, Enum
{
}
using AHSW.EventLog.Models.Entities.Abstract;

namespace AHSW.EventLog.Models.Entities;

public class EntityTypeDescription<TEntityType> :
    BaseDescriptiveEntity<TEntityType>
        where TEntityType : struct, Enum
{
}
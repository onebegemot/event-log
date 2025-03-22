using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EntityTypeDescriptionConfiguration<TEntityType> :
    BaseConfiguration,
    IEntityTypeConfiguration<EntityTypeDescription<TEntityType>>
        where TEntityType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EntityTypeDescription<TEntityType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EntityTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        MapEnumTypeToaColumnType(builder.Property(x => x.EnumId));
    }
}
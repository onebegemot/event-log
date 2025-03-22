using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class PropertyTypeDescriptionConfiguration<TPropertyType> :
    BaseConfiguration,
    IEntityTypeConfiguration<PropertyTypeDescription<TPropertyType>>
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<PropertyTypeDescription<TPropertyType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.PropertyTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        MapEnumTypeToaColumnType(builder.Property(x => x.EnumId));
    }
}
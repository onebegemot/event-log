using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class PropertyTypeDescriptionConfiguration : IEntityTypeConfiguration<PropertyTypeDescription>
{
    public void Configure(EntityTypeBuilder<PropertyTypeDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.PropertyTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
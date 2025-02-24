using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventTypeDescriptionConfiguration : IEntityTypeConfiguration<EventTypeDescription>
{
    public void Configure(EntityTypeBuilder<EventTypeDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EventTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}

public class EntityTypeDescriptionConfiguration : IEntityTypeConfiguration<EntityTypeDescription>
{
    public void Configure(EntityTypeBuilder<EntityTypeDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EntityTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}

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
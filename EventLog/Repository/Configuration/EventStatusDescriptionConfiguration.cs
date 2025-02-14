using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class EventStatusDescriptionConfiguration : IEntityTypeConfiguration<EventStatusDescription>
{
    public void Configure(EntityTypeBuilder<EventStatusDescription> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EventStatusDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
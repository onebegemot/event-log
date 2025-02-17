using EventLog.Models.Entities.PropertyLogEntries;
using EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repositories.Configuration;

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
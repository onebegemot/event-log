using AHWS.EventLog.Models.Entities.PropertyLogEntries;
using AHWS.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHWS.EventLog.Repositories.Configuration;

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
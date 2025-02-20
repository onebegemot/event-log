using AHWS.EventLog.Models.Entities.PropertyLogEntries;
using AHWS.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHWS.EventLog.Repositories.Configuration;

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
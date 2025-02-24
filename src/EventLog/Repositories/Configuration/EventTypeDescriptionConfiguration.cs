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
using EventLog.Models.Entities;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class EventLogEntryConfiguration : IEntityTypeConfiguration<EventLogEntry>
{
    public void Configure(EntityTypeBuilder<EventLogEntry> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EventLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
            
        builder
            .HasMany(x => x.EntityLogEntries)
            .WithOne(x => x.EventLogEntry)
            .HasForeignKey(x => x.EventLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
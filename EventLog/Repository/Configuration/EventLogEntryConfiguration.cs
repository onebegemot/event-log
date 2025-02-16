using EventLog.Models.Entities;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class EventLogEntryConfiguration<TEventType> :
    IEntityTypeConfiguration<EventLogEntry<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EventLogEntry<TEventType>> builder)
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
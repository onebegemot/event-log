using EventLog.Models.Entities;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class EventLogEntryConfiguration<TEventType, TEntityType> :
    IEntityTypeConfiguration<EventLogEntry<TEventType, TEntityType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EventLogEntry<TEventType, TEntityType>> builder)
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
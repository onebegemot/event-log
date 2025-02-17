using EventLog.Models.Entities;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class EventLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    IEntityTypeConfiguration<EventLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EventLogEntry<TEventType, TEntityType, TPropertyType>> builder)
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
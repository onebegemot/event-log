using EventLog.Models.Entities;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class EntityLogEntryConfiguration<TEventType> :
    IEntityTypeConfiguration<EntityLogEntry<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EntityLogEntry<TEventType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EntityLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        builder
            .HasMany(x => x.BoolPropertyLogEntries)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.StringPropertyLogEntries)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.Int32PropertyLogEntries)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasMany(x => x.DecimalPropertyLogEntries)
            .WithOne(x => x.EntityLogEntry)
            .HasForeignKey(x => x.EntityLogEntryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
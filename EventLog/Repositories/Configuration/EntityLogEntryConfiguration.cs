using EventLog.Models.Entities;
using EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repositories.Configuration;

public class EntityLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    IEntityTypeConfiguration<EntityLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EntityLogEntry<TEventType, TEntityType, TPropertyType>> builder)
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
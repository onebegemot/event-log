using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class BoolPropertyLogEntryConfiguration<TEventType, TEntityType> :
    IEntityTypeConfiguration<BoolPropertyLogEntry<TEventType, TEntityType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
    public void Configure(EntityTypeBuilder<BoolPropertyLogEntry<TEventType, TEntityType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.BoolPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
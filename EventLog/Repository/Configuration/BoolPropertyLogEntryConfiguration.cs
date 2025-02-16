using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class BoolPropertyLogEntryConfiguration<TEventType> :
    IEntityTypeConfiguration<BoolPropertyLogEntry<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<BoolPropertyLogEntry<TEventType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.BoolPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class Int32PropertyLogEntryConfiguration<TEventType> :
    IEntityTypeConfiguration<Int32PropertyLogEntry<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<Int32PropertyLogEntry<TEventType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.Int32PropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
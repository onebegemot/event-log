using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class StringPropertyLogEntryConfiguration<TEventType> :
    IEntityTypeConfiguration<StringPropertyLogEntry<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<StringPropertyLogEntry<TEventType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.StringPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class StringPropertyLogEntryConfiguration<TEventType, TEntityType> :
    IEntityTypeConfiguration<StringPropertyLogEntry<TEventType, TEntityType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
    public void Configure(EntityTypeBuilder<StringPropertyLogEntry<TEventType, TEntityType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.StringPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
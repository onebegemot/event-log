using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class Int32PropertyLogEntryConfiguration<TEventType,TEntityType> :
    IEntityTypeConfiguration<Int32PropertyLogEntry<TEventType, TEntityType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
    public void Configure(EntityTypeBuilder<Int32PropertyLogEntry<TEventType, TEntityType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.Int32PropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
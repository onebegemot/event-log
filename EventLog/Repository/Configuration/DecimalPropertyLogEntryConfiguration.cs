using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class DecimalPropertyLogEntryConfiguration<TEventType> :
    IEntityTypeConfiguration<DecimalPropertyLogEntry<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<DecimalPropertyLogEntry<TEventType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.DecimalPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
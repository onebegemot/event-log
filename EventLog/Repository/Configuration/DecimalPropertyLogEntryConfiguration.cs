using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class DecimalPropertyLogEntryConfiguration<TEventType, TEntityType> :
    IEntityTypeConfiguration<DecimalPropertyLogEntry<TEventType, TEntityType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
{
    public void Configure(EntityTypeBuilder<DecimalPropertyLogEntry<TEventType, TEntityType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.DecimalPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class DoublePropertyLogEntryConfiguration<TEventType, TEntityType, TPropertyType> :
    IEntityTypeConfiguration<DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<DoublePropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.DoublePropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
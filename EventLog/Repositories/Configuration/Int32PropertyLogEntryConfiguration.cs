using AHWS.EventLog.Models.Entities.PropertyLogEntries;
using AHWS.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHWS.EventLog.Repositories.Configuration;

public class Int32PropertyLogEntryConfiguration<TEventType,TEntityType, TPropertyType> :
    IEntityTypeConfiguration<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public void Configure(EntityTypeBuilder<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.Int32PropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Repositories.Configuration.Abstract;
using AHSW.EventLog.Repositories.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHSW.EventLog.Repositories.Configuration;

public class EventTypeDescriptionConfiguration<TEventType> :
    BaseConfiguration,
    IEntityTypeConfiguration<EventTypeDescription<TEventType>>
        where TEventType : struct, Enum
{
    public void Configure(EntityTypeBuilder<EventTypeDescription<TEventType>> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.EventTypeDescriptionsTableName,
                EventLogPersistenceConstants.EventLogSchema);
        
        MapEnumTypeToaColumnType(builder.Property(x => x.EnumId));
    }
}
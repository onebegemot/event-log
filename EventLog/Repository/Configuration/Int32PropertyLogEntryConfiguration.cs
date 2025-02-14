using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class Int32PropertyLogEntryConfiguration : IEntityTypeConfiguration<Int32PropertyLogEntry>
{
    public void Configure(EntityTypeBuilder<Int32PropertyLogEntry> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.Int32PropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
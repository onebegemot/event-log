using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class StringPropertyLogEntryConfiguration : IEntityTypeConfiguration<StringPropertyLogEntry>
{
    public void Configure(EntityTypeBuilder<StringPropertyLogEntry> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.StringPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventLog.Repository.Configuration;

public class DecimalPropertyLogEntryConfiguration : IEntityTypeConfiguration<DecimalPropertyLogEntry>
{
    public void Configure(EntityTypeBuilder<DecimalPropertyLogEntry> builder)
    {
        builder
            .ToTable(
                EventLogPersistenceConstants.DecimalPropertyLogTableName,
                EventLogPersistenceConstants.EventLogSchema);
    }
}
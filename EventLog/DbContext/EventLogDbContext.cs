using EventLog.Models.Entities;
using EventLog.Models.Entities.PropertyLogEntryModels;
using Microsoft.EntityFrameworkCore;

namespace EventLog.DbContext;

public class EventLogDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public EventLogDbContext(DbContextOptions<EventLogDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<EventLogEntry> EventLog { get; set; }
        
    public DbSet<EntityLogEntry> EntityLog { get; set; }
        
    public DbSet<BoolPropertyLogEntry> BoolPropertyLog { get; set; }
        
    public DbSet<StringPropertyLogEntry> StringPropertyLog { get; set; }
        
    public DbSet<Int32PropertyLogEntry> Int32PropertyLog { get; set; }
        
    public DbSet<DecimalPropertyLogEntry> DecimalPropertyLog { get; set; }

    public DbSet<EventTypeDescription> EventTypeDescriptions { get; set; }
        
    public DbSet<EventStatusDescription> EventStatusDescriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventLogDbContext).Assembly);
    }
}
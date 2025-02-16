using EventLog.Models.Entities;
using EventLog.Models.Entities.PropertyLogEntryModels;
using Microsoft.EntityFrameworkCore;

namespace EventLog.DatabaseContext;

public class EventLogDbContext<TDbContext, TEventType> : DbContext
    where TDbContext : DbContext
    where TEventType : struct, Enum
{
    public EventLogDbContext(DbContextOptions<TDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<EventLogEntry<TEventType>> EventLog { get; set; }
        
    public DbSet<EntityLogEntry<TEventType>> EntityLog { get; set; }
        
    public DbSet<BoolPropertyLogEntry<TEventType>> BoolPropertyLog { get; set; }
        
    public DbSet<StringPropertyLogEntry<TEventType>> StringPropertyLog { get; set; }
        
    public DbSet<Int32PropertyLogEntry<TEventType>> Int32PropertyLog { get; set; }
        
    public DbSet<DecimalPropertyLogEntry<TEventType>> DecimalPropertyLog { get; set; }

    public DbSet<EventTypeDescription> EventTypeDescriptions { get; set; }
        
    public DbSet<EventStatusDescription> EventStatusDescriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventLogDbContext<TDbContext, TEventType>).Assembly);
    }
}
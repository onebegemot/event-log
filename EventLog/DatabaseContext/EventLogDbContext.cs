using EventLog.Models.Entities;
using EventLog.Models.Entities.PropertyLogEntryModels;
using Microsoft.EntityFrameworkCore;

namespace EventLog.DatabaseContext;

public class EventLogDbContext<TDbContext, TEventType, TEntityType> : DbContext
    where TDbContext : DbContext
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
{
    public EventLogDbContext(DbContextOptions<TDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<EventLogEntry<TEventType, TEntityType>> EventLog { get; set; }
        
    public DbSet<EntityLogEntry<TEventType, TEntityType>> EntityLog { get; set; }
        
    public DbSet<BoolPropertyLogEntry<TEventType, TEntityType>> BoolPropertyLog { get; set; }
        
    public DbSet<StringPropertyLogEntry<TEventType, TEntityType>> StringPropertyLog { get; set; }
        
    public DbSet<Int32PropertyLogEntry<TEventType, TEntityType>> Int32PropertyLog { get; set; }
        
    public DbSet<DecimalPropertyLogEntry<TEventType, TEntityType>> DecimalPropertyLog { get; set; }

    public DbSet<EventTypeDescription> EventTypeDescriptions { get; set; }
        
    public DbSet<EventStatusDescription> EventStatusDescriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventLogDbContext<TDbContext, TEventType, TEntityType>).Assembly);
    }
}
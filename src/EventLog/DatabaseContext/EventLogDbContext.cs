﻿using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.DatabaseContext;

public class EventLogDbContext<TDbContext, TEventType, TEntityType, TPropertyType> :
    DbContext,
    IEventLogDbContext<TEventType, TEntityType, TPropertyType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    public EventLogDbContext(DbContextOptions<TDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<EventLogEntry<TEventType, TEntityType, TPropertyType>> EventLog { get; set; }
        
    public DbSet<EntityLogEntry<TEventType, TEntityType, TPropertyType>> EntityLog { get; set; }
        
    public DbSet<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>> BoolPropertyLog { get; set; }
        
    public DbSet<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>> StringPropertyLog { get; set; }
        
    public DbSet<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>> Int32PropertyLog { get; set; }
        
    public DbSet<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>> DecimalPropertyLog { get; set; }

    public DbSet<EventTypeDescription> EventTypeDescriptions { get; set; }
    
    public DbSet<EventStatusDescription> EventStatusDescriptions { get; set; }
    
    public DbSet<EntityTypeDescription> EntityTypeDescriptions { get; set; }
    
    public DbSet<PropertyTypeDescription> PropertyTypeDescriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventLogDbContext<TDbContext, TEventType, TEntityType, TPropertyType>).Assembly);
    }
}
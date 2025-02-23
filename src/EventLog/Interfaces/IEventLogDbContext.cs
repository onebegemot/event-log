using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Entities.PropertyLogEntries;
using Microsoft.EntityFrameworkCore;

namespace AHSW.EventLog.Interfaces;

public interface IEventLogDbContext<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    DbSet<EventLogEntry<TEventType, TEntityType, TPropertyType>> EventLog { get; set; }
    
    DbSet<EntityLogEntry<TEventType, TEntityType, TPropertyType>> EntityLog { get; set; }
    
    DbSet<BoolPropertyLogEntry<TEventType, TEntityType, TPropertyType>> BoolPropertyLog { get; set; }
    
    DbSet<StringPropertyLogEntry<TEventType, TEntityType, TPropertyType>> StringPropertyLog { get; set; }
    
    DbSet<Int32PropertyLogEntry<TEventType, TEntityType, TPropertyType>> Int32PropertyLog { get; set; }
    
    DbSet<DecimalPropertyLogEntry<TEventType, TEntityType, TPropertyType>> DecimalPropertyLog { get; set; }
}
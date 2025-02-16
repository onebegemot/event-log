using EventLog.Interfaces;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Models;

public class EventLogConfiguration<TDbContext, TEventType> :
    IEventLogConfigurator<TEventType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescription = new();
    private readonly Dictionary<EventStatus, string> _eventStatusDescription = new();
    
    private TDbContext _databaseContext;
    
    public IReadOnlyDictionary<TEventType, string> EventTypeDescription => _eventTypeDescription;
    
    public IReadOnlyDictionary<EventStatus, string> EventStatusDescription => _eventStatusDescription;

    public TDbContext DatabaseContext => _databaseContext;
    
    public IEventLogConfigurator<TEventType> SetDatabaseContext(TDbContext context)
    {
        _databaseContext = context;
        return this;
    }
    
    IEventLogConfigurator<TEventType> IEventLogConfigurator<TEventType>.AddEventTypeDescription(
        TEventType eventType, string description)
    {
        _eventTypeDescription[eventType] = description;
        return this;
    }
    
    IEventLogConfigurator<TEventType> IEventLogConfigurator<TEventType>.AddEventStatusDescription(
        EventStatus eventStatus, string description)
    {
        _eventStatusDescription[eventStatus] = description;
        return this;
    }
}
using Microsoft.EntityFrameworkCore;

namespace EventLog.Models;

public interface IEventTypeConfigurator<in TEventType>
    where TEventType : struct, Enum
{
    IEventTypeConfigurator<TEventType> AddEventTypeDescription(
        TEventType eventType, string description);

}

public class EventLogConfiguration<TDbContext, TEventType> :
    IEventTypeConfigurator<TEventType>
        where TDbContext : DbContext
        where TEventType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescription = new();
    
    private TDbContext _databaseContext;
    
    public IReadOnlyDictionary<TEventType, string> EventTypeDescription => _eventTypeDescription;

    public TDbContext DatabaseContext => _databaseContext;
    
    public IEventTypeConfigurator<TEventType> SetDatabaseContext(TDbContext context)
    {
        _databaseContext = context;
        return this;
    }
    
    IEventTypeConfigurator<TEventType> IEventTypeConfigurator<TEventType>.AddEventTypeDescription(
        TEventType eventType, string description)
    {
        _eventTypeDescription.Add(eventType, description);
        return this;
    }
}
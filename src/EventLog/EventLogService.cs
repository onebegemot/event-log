using AHSW.EventLog.Extensions;
using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Models;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog;

public class EventLogService<TEventType, TEntityType, TPropertyType> :
    IEventLogService<TEventType, TEntityType, TPropertyType>
        where TEventType : struct, Enum
        where TEntityType : struct, Enum
        where TPropertyType : struct, Enum
{
    private readonly IEventLogEntryRepository<TEventType, TEntityType, TPropertyType> _eventLogEntryRepository;
    
    public EventLogService(IEventLogEntryRepository<TEventType, TEntityType, TPropertyType> eventLogEntryRepository)
    {
        _eventLogEntryRepository = eventLogEntryRepository;
    }
    
    public async Task CreateEventScopeAndRun(TEventType eventLogType,
        Func<EventLogScope<TEventType, TEntityType, TPropertyType>, Task> workUnitAction)
    {
        var eventLogEntry = new EventLogEntry<TEventType, TEntityType, TPropertyType>(eventLogType);;
        
        try
        {
            await workUnitAction(
                new EventLogScope<TEventType, TEntityType, TPropertyType>(
                    eventLogEntry, _eventLogEntryRepository));
            
            eventLogEntry.Status ??= EventStatus.Successful;
            
            await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry);
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(eventLogEntry, exception);
            throw;
        }
    }
    
    public async Task<TResult> CreateEventScopeAndRun<TResult>(TEventType eventLogType,
        Func<EventLogScope<TEventType, TEntityType, TPropertyType>, Task<TResult>> workUnitAction)
    {
        var eventLogEntry = new EventLogEntry<TEventType, TEntityType, TPropertyType>(eventLogType);;
        
        try
        {
            var result = await workUnitAction(
                new EventLogScope<TEventType, TEntityType, TPropertyType>(
                    eventLogEntry, _eventLogEntryRepository));
            
            eventLogEntry.Status ??= EventStatus.Successful;
            
            await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry);

            return result;
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(eventLogEntry, exception);
            throw;
        }
    }
    
    private async ValueTask ProcessUnhandledException(
        EventLogEntry<TEventType, TEntityType, TPropertyType> eventLogEntry,
        Exception exception)
    {
        if (!eventLogEntry.ExplicitlyThrownException)
        {
            eventLogEntry.SetFailedStatusAndAddFailureDetails(EventStatus.UnhandledException,
                "--- UNHANDLED EXCEPTION ---", exception.ToString());
        }

        await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry);
    }
}
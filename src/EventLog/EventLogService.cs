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
            
            eventLogEntry.Status = EventStatus.Successful;
            
            await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry);
        }
        catch (TaskCanceledException exception)
        {
            await ProcessUnhandledException(EventStatus.TaskCancelledException, eventLogEntry, exception);
            throw;
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(EventStatus.UnhandledException, eventLogEntry, exception);
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
            
            eventLogEntry.Status = EventStatus.Successful;
            
            await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry);

            return result;
        }
        catch (TaskCanceledException exception)
        {
            await ProcessUnhandledException(EventStatus.TaskCancelledException, eventLogEntry, exception);
            throw;
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(EventStatus.UnhandledException, eventLogEntry, exception);
            throw;
        }
    }
    
    private async ValueTask ProcessUnhandledException(EventStatus eventStatus,
        EventLogEntry<TEventType, TEntityType, TPropertyType> eventLogEntry,
        Exception exception)
    {
        if (!eventLogEntry.ExplicitlyThrownException)
        {
            var header = string.Empty;
            
            if (eventStatus == EventStatus.UnhandledException)
                header = "--- UNHANDLED EXCEPTION ---";
            
            if (eventStatus == EventStatus.UnhandledException)
                header = "--- TOKEN CANCELLATION EXCEPTION ---";
            
            eventLogEntry.SetFailedStatusAndAddFailureDetails(eventStatus, header, exception.ToString());
        }

        await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry);
    }
}
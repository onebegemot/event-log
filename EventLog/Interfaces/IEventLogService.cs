using EventLog.Models;
using EventLog.Models.Entities;

namespace EventLog.Interfaces;

public interface IEventLogService<TEventType>
    where TEventType : struct, Enum
{
    /// <summary>
    /// Create internally EventLogEntry object, wraps the work unit, executes it and
    /// logs a result to the EventLogEntry object
    /// </summary>
    Task CreateEventLogEntryAndProcessUnitOfWorkAsync(TEventType eventLogType,
        int? initiatorId, Func<EventLogEntry<TEventType>, Task> workUnitAction, string details = null);
    
    /// <summary>
    /// Create internally EventLogEntry object, wraps the work unit, executes it and
    /// logs a result to the EventLogEntry object
    /// </summary>
    Task<TResult> CreateEventLogEntryAndProcessUnitOfWorkAsync<TResult>(TEventType eventLogType,
        int? initiatorId, Func<EventLogEntry<TEventType>, Task<TResult>> workUnitAction, string details = null);
    
    /// <summary>
    /// Executes a repository action and records logs according to the passed configuration
    /// </summary>
    /// <param name="repositoryActionAsync">A repository action which will be executed and logged</param>
    /// <param name="eventLogEntry">It must be in the single DB context</param>
    /// <param name="getLogEntitiesActions">An object info model of logging entities and related properties</param>
    Task ExecuteActionAndAddRelatedLogAsync(
        Func<Task> repositoryActionAsync, EventLogEntry<TEventType> eventLogEntry,
        params Func<IEnumerable<LogEntityUnit<TEventType>>>[] getLogEntitiesActions);
}
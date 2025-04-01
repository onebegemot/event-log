using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Models.Configurations;
using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Models;

public class EventLogScope<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    private readonly IApplicationRepository _applicationRepository;
    
    public EventLogScope(
        EventLogEntry<TEventType,TEntityType,TPropertyType> eventLogEntry,
        IApplicationRepository applicationRepository)
    {
        ArgumentNullException.ThrowIfNull(eventLogEntry);

        EventLogEntry = eventLogEntry;
        _applicationRepository = applicationRepository;
    }
    
    public EventLogEntry<TEventType,TEntityType,TPropertyType> EventLogEntry { get; }

    /// <summary>
    /// Executes a repository action and records logs according to the passed configuration
    /// </summary>
    /// <param name="repositoryActionAsync">A repository action which will be executed and logged</param>
    /// <param name="optionsBuilder"></param>
    public async Task SaveAndLogEntitiesAsync(Func<Task> repositoryActionAsync,
        Action<EntityLogConfiguration<TEventType, TEntityType, TPropertyType>> optionsBuilder = null)
    {
        ArgumentNullException.ThrowIfNull(repositoryActionAsync);
        
        var entityLogConfiguration = new EntityLogConfiguration<TEventType, TEntityType, TPropertyType>(_applicationRepository);
        optionsBuilder?.Invoke(entityLogConfiguration);
        var logEntityUnits = entityLogConfiguration.LogEntityUnits;
        
        await repositoryActionAsync();

        if (logEntityUnits.Any())
        {
            EventLogEntry.EntityLog = EventLogEntry.EntityLog == null
                ? GetFilteredEntityLogEntries(logEntityUnits)
                    .ToList()
                : EventLogEntry.EntityLog
                    .Concat(GetFilteredEntityLogEntries(logEntityUnits))
                    .ToList();
        }
        
        await _applicationRepository.AddOrUpdateEventLogAsync(EventLogEntry);

        return;
        
        IEnumerable<EntityLogEntry<TEventType,TEntityType,TPropertyType>> GetFilteredEntityLogEntries(
            IEnumerable<LogEntityUnit<TEventType, TEntityType, TPropertyType>> values) =>
                values
                    .Select(x => x.GetEntityLogEntry())
                    .Where(EntityLogEntryFilter);
    }
    
    private static bool EntityLogEntryFilter(
        EntityLogEntry<TEventType, TEntityType, TPropertyType> entry) =>
            entry.HasPropertyLogEntries;
}
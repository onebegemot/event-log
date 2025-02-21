using AHSW.EventLog.Interfaces;
using AHSW.EventLog.Models.Configurations;
using AHSW.EventLog.Models.Entities;

namespace AHSW.EventLog.Models;

public class EventLogScope<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    private readonly IEventLogEntryRepository<TEventType, TEntityType, TPropertyType> _eventLogEntryRepository;
    
    public EventLogScope(
        EventLogEntry<TEventType,TEntityType,TPropertyType> eventLogEntry,
        IEventLogEntryRepository<TEventType, TEntityType, TPropertyType> eventLogEntryRepository)
    {
        ArgumentNullException.ThrowIfNull(eventLogEntry);

        EventLogEntry = eventLogEntry;
        _eventLogEntryRepository = eventLogEntryRepository;
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
        
        var entityLogConfiguration = new EntityLogConfiguration<TEventType, TEntityType, TPropertyType>();
        optionsBuilder?.Invoke(entityLogConfiguration);
        var logEntityUnits = entityLogConfiguration.LogEntityUnits;
        
        await repositoryActionAsync();

        if (logEntityUnits.Any())
        {
            EventLogEntry.EntityLogEntries = EventLogEntry.EntityLogEntries == null
                ? GetFilteredEntityLogEntries(logEntityUnits)
                    .ToList()
                : EventLogEntry.EntityLogEntries
                    .Concat(GetFilteredEntityLogEntries(logEntityUnits))
                    .ToList();
        }
        
        await _eventLogEntryRepository.AddOrUpdateAsync(EventLogEntry);

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
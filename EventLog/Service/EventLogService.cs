using EventLog.Extensions;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities;
using EventLog.Models.Entities.Abstract;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;
using EventType = EventLog.Models.Enums.EventType;
using PropertyType = EventLog.Models.Enums.PropertyType;

namespace EventLog.Service;

public class EventLogService : IEventLogService
{
    private readonly IEventLogEntryRepository _eventLogEntryRepository;
    
    public EventLogService(IEventLogEntryRepository eventLogEntryRepository)
    {
        _eventLogEntryRepository = eventLogEntryRepository;
    }
    
    public static EventLogEntry CreateEventLogEntry(EventType eventLogType,
        int? initiatorId, string details = null) =>
        new ()
        {
            EventType = eventLogType,
            CreatedBy = initiatorId,
            CreatedAt = DateTime.UtcNow,
            Details = details
        };
    
    public async Task CreateEventLogEntryAndProcessUnitOfWorkAsync(EventType eventLogType,
        int? initiatorId, Func<EventLogEntry, Task> workUnitAction, string details)
    {
        // Create initial EventLogEntry
        var eventLogEntry = CreateEventLogEntry(eventLogType, initiatorId, details);
        
        try
        {
            // Execute client action and log modified entities and their properties
            await workUnitAction(eventLogEntry);
            
            eventLogEntry.Status ??= EventStatus.Successful;
            
            await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry, eventLogEntry.CreatedBy);
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(eventLogEntry, exception);
            throw;
        }
    }
    
    public async Task<TResult> CreateEventLogEntryAndProcessUnitOfWorkAsync<TResult>(EventType eventLogType,
        int? initiatorId, Func<EventLogEntry, Task<TResult>> workUnitAction, string details)
    {
        var eventLogEntry = CreateEventLogEntry(eventLogType, initiatorId, details);
        
        try
        {
            var result = await workUnitAction(eventLogEntry);
            
            eventLogEntry.Status ??= EventStatus.Successful;
            
            await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry, eventLogEntry.CreatedBy);

            return result;
        }
        catch (Exception exception)
        {
            await ProcessUnhandledException(eventLogEntry, exception);
            throw;
        }
    }
    
    public static IEnumerable<LogEntityUnit> GetLogEntities<TEntity>(
        Func<TEntity, string, object> getOriginalPropertyValue,
        EntityLogInfo<TEntity> logInfo)
            where TEntity : IPkEntity
    {
        var entityType = PropertyInfosInitializer.GetEntityType(logInfo);

        return logInfo.Entities
            .Select(entity =>
            {
                var entityLogEntry = new EntityLogEntry()
                {
                    ActionType = entity.Id == 0
                        ? ActionType.Create
                        : ActionType.Update,
                    EntityType = entityType
                };

                foreach (var property in logInfo.Properties)
                    AddPropertyLogEntries(entity, property, getOriginalPropertyValue, entityLogEntry);

                return new LogEntityUnit(entity, entityLogEntry);
            })
            .ToList();
    }
    
    public async Task ExecuteActionAndAddRelatedLogAsync(
        Func<Task> repositoryActionAsync, EventLogEntry eventLogEntry,
        params Func<IEnumerable<LogEntityUnit>>[] getLogEntitiesActions)
    {
        ArgumentNullException.ThrowIfNull(repositoryActionAsync);
        ArgumentNullException.ThrowIfNull(eventLogEntry);
        
        var logEntityUnits = new List<LogEntityUnit>();
        
        foreach (var getLogEntitiesAction in getLogEntitiesActions)
            logEntityUnits.AddRange(getLogEntitiesAction());
        
        await repositoryActionAsync();

        eventLogEntry.EntityLogEntries = eventLogEntry.EntityLogEntries == null
            ? logEntityUnits
                .Select(x => x.GetEntityLogEntry())
                .Where(EntityLogEntryFilter)
                .ToList()
            : eventLogEntry.EntityLogEntries
                .Concat(
                    logEntityUnits
                        .Select(x => x.GetEntityLogEntry())
                        .Where(EntityLogEntryFilter))
                .ToList();
        
        await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry, eventLogEntry.CreatedBy);
    }

    private static bool EntityLogEntryFilter(EntityLogEntry entry) => entry.HasPropertyLogEntries;
    
    private static void AddPropertyLogEntries<TEntity>(TEntity entity, PropertyType property,
        Func<TEntity, string, object> getOriginalPropertyValue, EntityLogEntry entityLogEntry)
            where TEntity : IPkEntity
    {
        var propertyValues = PropertyInfosInitializer.GetPropertyInfo(
            entity, property, getOriginalPropertyValue);

        var indicativeProperty = propertyValues.New ?? propertyValues.Original;
        
        if (indicativeProperty == null)
            return;
        
        switch (indicativeProperty)
        {
            case bool:
                TryCreateAndAddPropertyLogEntry<BoolPropertyLogEntry, bool>(
                    property, (bool)propertyValues.Original, (bool)propertyValues.New, entityLogEntry,
                    entityLogEntry.BoolPropertyLogEntries != null,
                    x => entityLogEntry.BoolPropertyLogEntries = x,
                    x => entityLogEntry.BoolPropertyLogEntries.Add(x));
                
                break;
            
            case string:
                TryCreateAndAddPropertyLogEntry<StringPropertyLogEntry, string>(
                    property, (string)propertyValues.Original, (string)propertyValues.New, entityLogEntry,
                    entityLogEntry.StringPropertyLogEntries != null,
                    x => entityLogEntry.StringPropertyLogEntries = x,
                    x => entityLogEntry.StringPropertyLogEntries.Add(x));
                
                break;
            
            case int:
                TryCreateAndAddPropertyLogEntry<Int32PropertyLogEntry, int>(
                    property, (int)propertyValues.Original, (int)propertyValues.New, entityLogEntry,
                    entityLogEntry.Int32PropertyLogEntries != null,
                    x => entityLogEntry.Int32PropertyLogEntries = x,
                    x => entityLogEntry.Int32PropertyLogEntries.Add(x));
                
                break;
            
            case decimal:
                TryCreateAndAddPropertyLogEntry<DecimalPropertyLogEntry, decimal>(
                    property, (decimal)propertyValues.Original, (decimal)propertyValues.New, entityLogEntry,
                    entityLogEntry.DecimalPropertyLogEntries != null,
                    x => entityLogEntry.DecimalPropertyLogEntries = x,
                    x => entityLogEntry.DecimalPropertyLogEntries.Add(x));
                
                break;
            
            default:
                throw new NotImplementedException(nameof(property));
        }
    }

    private static void TryCreateAndAddPropertyLogEntry<TPropertyLogEntry, TLogValue>(
        PropertyType propertyType, TLogValue originalValue, TLogValue newValue, EntityLogEntry entityLogEntry,
        bool isLogInitialized, Action<ICollection<TPropertyLogEntry>> collectionSetter,Action<TPropertyLogEntry> addItem)
            where TPropertyLogEntry : PropertyLogEntry<TLogValue>, new()
    {
        if (!IsNewEntity() && newValue != null && newValue.Equals(originalValue))
            return;
        
        var propertyLogEntry =  new TPropertyLogEntry
        {
            PropertyType = propertyType,
            Value = newValue,
            EntityLogEntry = entityLogEntry
        };
        
        if (!isLogInitialized)
            collectionSetter(new List<TPropertyLogEntry>());

        addItem(propertyLogEntry);

        return;

        bool IsNewEntity() => entityLogEntry.ActionType == ActionType.Create;
    }
    
    private async ValueTask ProcessUnhandledException(EventLogEntry eventLogEntry, Exception exception)
    {
        if (!eventLogEntry.ExplicitlyThrownException)
        {
            eventLogEntry.SetFailedStatusAndAddFailureDetails(EventStatus.UnhandledException,
                "--- UNHANDLED EXCEPTION ---", exception.ToString());
        }

        await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry, eventLogEntry.CreatedBy);
    }
}
using EventLog.Extensions;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities;
using EventLog.Models.Entities.Abstract;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;

namespace EventLog.Service;

public class EventLogService<TEventType> :
    IEventLogService<TEventType>
        where TEventType : struct, Enum
{
    private readonly IEventLogEntryRepository<TEventType> _eventLogEntryRepository;
    
    public EventLogService(IEventLogEntryRepository<TEventType> eventLogEntryRepository)
    {
        _eventLogEntryRepository = eventLogEntryRepository;
    }
    
    public static EventLogEntry<TEventType> CreateEventLogEntry(TEventType eventLogType,
        int? initiatorId, string details = null) =>
        new ()
        {
            EventType = eventLogType,
            CreatedBy = initiatorId,
            CreatedAt = DateTime.UtcNow,
            Details = details
        };
    
    public async Task CreateEventLogEntryAndProcessUnitOfWorkAsync(TEventType eventLogType,
        int? initiatorId, Func<EventLogEntry<TEventType>, Task> workUnitAction, string details)
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
    
    public async Task<TResult> CreateEventLogEntryAndProcessUnitOfWorkAsync<TResult>(TEventType eventLogType,
        int? initiatorId, Func<EventLogEntry<TEventType>, Task<TResult>> workUnitAction, string details)
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
    
    public static IEnumerable<LogEntityUnit<TEventType>> GetLogEntities<TEntity>(
        Func<TEntity, string, object> getOriginalPropertyValue,
        EntityLogInfo<TEntity> logInfo)
            where TEntity : IPkEntity
    {
        var entityType = PropertyInfosInitializer.GetEntityType(logInfo);

        return logInfo.Entities
            .Select(entity =>
            {
                var entityLogEntry = new EntityLogEntry<TEventType>()
                {
                    ActionType = entity.Id == 0
                        ? ActionType.Create
                        : ActionType.Update,
                    EntityType = entityType
                };

                foreach (var property in logInfo.Properties)
                    AddPropertyLogEntries(entity, property, getOriginalPropertyValue, entityLogEntry);

                return new LogEntityUnit<TEventType>(entity, entityLogEntry);
            })
            .ToList();
    }
    
    public async Task ExecuteActionAndAddRelatedLogAsync(
        Func<Task> repositoryActionAsync, EventLogEntry<TEventType> eventLogEntry,
        params Func<IEnumerable<LogEntityUnit<TEventType>>>[] getLogEntitiesActions)
    {
        ArgumentNullException.ThrowIfNull(repositoryActionAsync);
        ArgumentNullException.ThrowIfNull(eventLogEntry);
        
        var logEntityUnits = new List<LogEntityUnit<TEventType>>();
        
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

    private static bool EntityLogEntryFilter(EntityLogEntry<TEventType> entry) =>
        entry.HasPropertyLogEntries;
    
    private static void AddPropertyLogEntries<TEntity>(TEntity entity, PropertyType property,
        Func<TEntity, string, object> getOriginalPropertyValue, EntityLogEntry<TEventType> entityLogEntry)
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
                TryCreateAndAddPropertyLogEntry<BoolPropertyLogEntry<TEventType>, bool>(
                    property, (bool)propertyValues.Original, (bool)propertyValues.New, entityLogEntry,
                    entityLogEntry.BoolPropertyLogEntries != null,
                    x => entityLogEntry.BoolPropertyLogEntries = x,
                    x => entityLogEntry.BoolPropertyLogEntries.Add(x));
                
                break;
            
            case string:
                TryCreateAndAddPropertyLogEntry<StringPropertyLogEntry<TEventType>, string>(
                    property, (string)propertyValues.Original, (string)propertyValues.New, entityLogEntry,
                    entityLogEntry.StringPropertyLogEntries != null,
                    x => entityLogEntry.StringPropertyLogEntries = x,
                    x => entityLogEntry.StringPropertyLogEntries.Add(x));
                
                break;
            
            case int:
                TryCreateAndAddPropertyLogEntry<Int32PropertyLogEntry<TEventType>, int>(
                    property, (int)propertyValues.Original, (int)propertyValues.New, entityLogEntry,
                    entityLogEntry.Int32PropertyLogEntries != null,
                    x => entityLogEntry.Int32PropertyLogEntries = x,
                    x => entityLogEntry.Int32PropertyLogEntries.Add(x));
                
                break;
            
            case decimal:
                TryCreateAndAddPropertyLogEntry<DecimalPropertyLogEntry<TEventType>, decimal>(
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
        PropertyType propertyType, TLogValue originalValue, TLogValue newValue,
        EntityLogEntry<TEventType> entityLogEntry, bool isLogInitialized,
        Action<ICollection<TPropertyLogEntry>> collectionSetter,Action<TPropertyLogEntry> addItem)
            where TPropertyLogEntry : PropertyLogEntry<TLogValue, TEventType>, new()
    {
        if (!IsNewEntity() && newValue != null && newValue.Equals(originalValue))
            return;
        
        var propertyLogEntry = new TPropertyLogEntry
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
    
    private async ValueTask ProcessUnhandledException(EventLogEntry<TEventType> eventLogEntry, Exception exception)
    {
        if (!eventLogEntry.ExplicitlyThrownException)
        {
            eventLogEntry.SetFailedStatusAndAddFailureDetails(EventStatus.UnhandledException,
                "--- UNHANDLED EXCEPTION ---", exception.ToString());
        }

        await _eventLogEntryRepository.AddOrUpdateAsync(eventLogEntry, eventLogEntry.CreatedBy);
    }
}
using EventLog.Models;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Service;

public static class EventLogServiceConfigurator
{
    public static void UseCustomTypeDescriptions<TDbContext, TEventType>(
        Action<EventLogConfiguration<TDbContext, TEventType>> configurationBuilder = null)
        where TDbContext : DbContext
        where TEventType : struct, Enum
    {
        var configuration = new EventLogConfiguration<TDbContext, TEventType>();
        configurationBuilder?.Invoke(configuration);
        
        FillEnumTypeDescriptionTables(configuration);
    }
    
    private static void FillEnumTypeDescriptionTables<TDbContext, TEventType>(
        EventLogConfiguration<TDbContext, TEventType> configuration)
        where TDbContext : DbContext
        where TEventType : struct, Enum
    {
        var context = configuration?.DatabaseContext;

        if (context != null)
        {
            context.Set<EventTypeDescription>().ExecuteDelete();
            context.Set<EventTypeDescription>().AddRange(GetEventTypeDescriptions(configuration.EventTypeDescription));
        
            context.Set<EventStatusDescription>().ExecuteDelete();
            context.Set<EventStatusDescription>().AddRange(GetEventStatusDescriptions());
        
            context.SaveChanges();
        }
    }
    
    private static IReadOnlyCollection<EventTypeDescription> GetEventTypeDescriptions<TEventType>(
        IReadOnlyDictionary<TEventType, string> eventTypeDescriptions)
        where TEventType : struct, Enum
    {
        var enumValues = Enum.GetValues<TEventType>();
        var entities = new List<EventTypeDescription>(enumValues.Length);
        
        entities.AddRange(enumValues.Select(value =>
            new EventTypeDescription()
            {
                EnumId = Convert.ToInt32(value),
                Description = eventTypeDescriptions?.TryGetValue(value, out var description) ?? false
                    ? description
                    : Enum.GetName(value)
            }));

        return entities;
    }
    
    private static IReadOnlyCollection<EventStatusDescription> GetEventStatusDescriptions()
    {
        var enumValues = Enum.GetValues<EventStatus>();
        var entities = new List<EventStatusDescription>(enumValues.Length);
        
        entities.AddRange(enumValues.Select(value =>
            new EventStatusDescription()
            {
                EnumId = (int)value,
                Description = GetDescription(value)
            }));

        return entities;
    }
    
    private static string GetDescription(EventStatus eventType) =>
        eventType switch
        {
            EventStatus.Successful => "Successful",
            EventStatus.HandledException => "Handled exception",
            EventStatus.UnhandledException => "Unhandled exception",
            
            _ => "UNKNOWN STATUS TYPE"
        };
    
}
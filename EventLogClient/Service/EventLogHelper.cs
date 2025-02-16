using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;

namespace EventLog.Service;

public static class EventLogHelper
{
    public static IReadOnlyCollection<EventTypeDescription> GetEventTypeDescriptionEntities()
    {
        var enumValues = Enum.GetValues<EventType>();
        var entities = new List<EventTypeDescription>(enumValues.Length);
        
        entities.AddRange(enumValues.Select(value =>
                new EventTypeDescription()
                {
                    EnumId = (int)value,
                    Description = GetDescription(value)
                }));

        return entities;
    }
    
    public static IReadOnlyCollection<EventStatusDescription> GetEventStatusDescriptionEntities()
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
    
    private static string GetDescription(EventType eventType) =>
        eventType switch
        {
            EventType.UpdateApplicationEntity => "UpdateApplicationEntity",
            _ => "UNKNOWN EVENT TYPE"
        };
    
    private static string GetDescription(EventStatus eventType) =>
        eventType switch
        {
            EventStatus.Successful => "Successful",
            EventStatus.HandledException => "Handled exception",
            EventStatus.UnhandledException => "Unhandled exception",
            _ => "UNKNOWN STATUS TYPE"
        };
}
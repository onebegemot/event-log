using EventLog.Models.Enums;

namespace EventLog.Interfaces;

public interface IEventLogConfigurator<in TEventType>
    where TEventType : struct, Enum
{
    IEventLogConfigurator<TEventType> AddEventTypeDescription(
        TEventType eventType, string description);
    
    IEventLogConfigurator<TEventType> AddEventStatusDescription(
        EventStatus eventType, string description);
}
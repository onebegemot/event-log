using AHWS.EventLog.Interfaces.Configurators;
using AHWS.EventLog.Models.Enums;

namespace AHWS.EventLog.Models.Configurations;

public class TypeDescriptionsConfiguration<TEventType> :
    ITypeDescriptionsConfigurator<TEventType>
        where TEventType : struct, Enum
{
    private readonly Dictionary<TEventType, string> _eventTypeDescription = new();
    private readonly Dictionary<EventStatus, string> _eventStatusDescription = new();
    
    public IReadOnlyDictionary<TEventType, string> EventTypeDescription => _eventTypeDescription;
    
    public IReadOnlyDictionary<EventStatus, string> EventStatusDescription => _eventStatusDescription;
    
    public ITypeDescriptionsConfigurator<TEventType> AddEventTypeDescription(
        TEventType eventType, string description)
    {
        _eventTypeDescription[eventType] = description;
        return this;
    }
    
    public ITypeDescriptionsConfigurator<TEventType> AddEventStatusDescription(
        EventStatus eventStatus, string description)
    {
        _eventStatusDescription[eventStatus] = description;
        return this;
    }
}
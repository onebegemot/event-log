using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.Service;

public static class EventLogServiceConfigurator<TDbContext, TEventType>
    where TDbContext : DbContext
    where TEventType : struct, Enum
{
    public static void UseCustomTypeDescriptions(
        Action<EventLogConfiguration<TDbContext, TEventType>> configurationBuilder = null)
    {
        var configuration = CreateDefaultConfiguration();
        configurationBuilder?.Invoke(configuration);
        TryFillCustomDescriptionTables(configuration);
    }

    private static EventLogConfiguration<TDbContext, TEventType> CreateDefaultConfiguration()
    {
        var configuration = new EventLogConfiguration<TDbContext, TEventType>();

        ((IEventLogConfigurator<TEventType>)configuration)
            .AddEventStatusDescription(EventStatus.Successful, "Successful")
            .AddEventStatusDescription(EventStatus.HandledException, "Handled exception")
            .AddEventStatusDescription(EventStatus.UnhandledException, "Unhandled exception");

        return configuration;
    }
    
    private static void TryFillCustomDescriptionTables(
        EventLogConfiguration<TDbContext, TEventType> configuration)
    {
        var context = configuration?.DatabaseContext;

        if (context == null)
            return;

        var eventTypeDescriptions = GetCustomEnumDescriptions<TEventType, EventTypeDescription>(configuration.EventTypeDescription);
        context.Set<EventTypeDescription>().ExecuteDelete();
        context.Set<EventTypeDescription>().AddRange(eventTypeDescriptions);

        var eventStatusDescriptions = GetCustomEnumDescriptions<EventStatus, EventStatusDescription>(configuration.EventStatusDescription);
        context.Set<EventStatusDescription>().ExecuteDelete();
        context.Set<EventStatusDescription>().AddRange(eventStatusDescriptions);
        
        context.SaveChanges();
    }
    
    private static IReadOnlyCollection<TDescriptiveEntity> GetCustomEnumDescriptions<TEnum, TDescriptiveEntity>(
        IReadOnlyDictionary<TEnum, string> enumDescriptions)
            where TDescriptiveEntity : BaseDescriptiveEntity, new()
            where TEnum : struct, Enum
    {
        var enumValues = Enum.GetValues<TEnum>();
        var entities = new List<TDescriptiveEntity>(enumValues.Length);
        
        entities.AddRange(enumValues.Select(value =>
            new TDescriptiveEntity()
            {
                EnumId = Convert.ToInt32(value),
                Description = enumDescriptions?.TryGetValue(value, out var description) ?? false
                    ? description
                    : Enum.GetName(value)
            }));

        return entities;
    }
}
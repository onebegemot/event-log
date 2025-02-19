using EventLog.Configuration;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities;
using EventLog.Models.Enums;

namespace EventLog;

internal class TestEventLog
{
    public async Task TestMultipleLoggingIntoOneEventLogScopeAsync(
        IEventLogService<EventType, EntityType, PropertyType> eventLogService,
        IApplicationEntityRepository applicationEntityRepository,
        IApplicationOtherEntityRepository applicationOtherEntityRepository) =>
            await eventLogService.CreateEventScopeAndRun(EventType.RunTestMethod,
                eventLogScope => TestEventLogActionAsync(eventLogScope,
                    applicationEntityRepository, applicationOtherEntityRepository));

    public async Task TestOnceLoggingIntoOneEventLogScopeAsync(
        IEventLogService<EventType, EntityType, PropertyType> eventLogService,
        IApplicationEntityRepository applicationEntityRepository)
    {
        var applicationEntity = new ApplicationEntity();
        
        await eventLogService.CreateEventScopeAndRun(EventType.RunTestMethod,
            eventLogScope =>
            {
                const int initiatorId = 9;
                
                eventLogScope.EventLogEntry.CreatedBy = initiatorId;
                eventLogScope.EventLogEntry.Details = "OnceLogging => OneEventLogScope";
                eventLogScope.EventLogEntry.FailureDetails = "-- empty --";
                
                return eventLogScope
                    .SaveAndLogEntitiesAsync(
                        () => applicationEntityRepository.AddOrUpdateAsync(applicationEntity),
                        options => options
                            .AddEntityLogging(
                                applicationEntityRepository.GetOriginalPropertyValue,
                                new[] { applicationEntity },
                                ObservableProperties.GetForApplicationEntity));
            });
    }

    private async Task TestEventLogActionAsync(
        EventLogScope<EventType, EntityType, PropertyType> eventLogScope,
        IApplicationEntityRepository applicationEntityRepository,
        IApplicationOtherEntityRepository applicationOtherEntityRepository)
    {
        const int initiatorId = 9;
        
        eventLogScope.EventLogEntry.CreatedBy = initiatorId;
        eventLogScope.EventLogEntry.Details = "MultipleLogging => OneEventLogScope";
        eventLogScope.EventLogEntry.FailureDetails = "-- empty --";
        
        Console.WriteLine("Test EventLogClient!");

        var applicationEntity = new ApplicationEntity()
        {
            TestBool = true,
            TestDate = DateTime.Now,
            TestInt32 = initiatorId,
            TestString = DateTime.Now.Year.ToString()
        };
        
        var applicationOtherEntity = new ApplicationOtherEntity()
        {
            TestDecimal = (decimal)23.24
        };
        
        await eventLogScope.SaveAndLogEntitiesAsync(
            () => Task.WhenAll(
                applicationEntityRepository.AddOrUpdateAsync(applicationEntity),
                applicationOtherEntityRepository.AddOrUpdateAsync(applicationOtherEntity)),
            options => options
                .AddEntityLogging(
                    applicationEntityRepository.GetOriginalPropertyValue,
                    new[] { applicationEntity },
                    ObservableProperties.GetForApplicationEntity)
                .AddEntityLogging(
                    applicationOtherEntityRepository.GetOriginalPropertyValue,
                    new[] { applicationOtherEntity },
                    ObservableProperties.GetForApplicationOtherEntity));
    }
}
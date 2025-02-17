using EventLog.Configuration;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities;
using EventLog.Models.Enums;

namespace EventLog;

internal class TestEventLog
{
    public async Task TestAsync(IEventLogService<EventType, EntityType, PropertyType> eventLogService,
        ITestDataRepository testDataRepository, int initiatorId) =>
            await eventLogService.CreateEventLogEntryAndProcessUnitOfWorkAsync(
                EventType.UpdateApplicationEntity, initiatorId,
                eventLogEntry => TestEventLogActionAsync(
                    eventLogEntry, eventLogService, testDataRepository));
    
    private async Task TestEventLogActionAsync(EventLogEntry<EventType, EntityType, PropertyType> eventLogEntry,
        IEventLogService<EventType, EntityType, PropertyType> eventLogService, ITestDataRepository testDataRepository)
    {
        const int initiatorId = 9;
        
        eventLogEntry.Details = "Test TestEventLogActionAsync is executed";
        Console.WriteLine("Test EventLogClient!");

        var testDate = new ApplicationEntity()
        {
            TestBool = true,
            TestDate = DateTime.Now,
            TestInt32 = initiatorId,
            TestString = DateTime.Now.Year.ToString()
        };
        
        await eventLogService.ExecuteActionAndAddRelatedLogAsync(
            () => testDataRepository.AddOrUpdateAsync(testDate),
            eventLogEntry,
            () => EventLogService<EventType, EntityType, PropertyType>.GetLogEntities(
                testDataRepository.GetOriginalPropertyValue,
                new EntityLogInfo<ApplicationEntity, PropertyType>(
                    new[] { testDate },
                    ObservableProperties.GetForApplicationEntity())));
    }
}
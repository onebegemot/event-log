using EventLog.DatabaseContext;
using EventLog.Enums;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities;
using EventLog.Service;

namespace EventLog;

internal class TestEventLog
{
    public async Task TestAsync(IEventLogService<EventType> eventLogService,
        ITestDataRepository testDataRepository, int initiatorId) =>
            await eventLogService.CreateEventLogEntryAndProcessUnitOfWorkAsync(
                EventType.UpdateApplicationEntity, initiatorId,
                eventLogEntry => TestEventLogActionAsync(
                    eventLogEntry, eventLogService, testDataRepository));
    
    private async Task TestEventLogActionAsync(EventLogEntry<EventType> eventLogEntry,
        IEventLogService<EventType> eventLogService, ITestDataRepository testDataRepository)
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
            () => EventLogService<EventType>.GetLogEntities(testDataRepository.GetOriginalPropertyValue,
                new EntityLogInfo<ApplicationEntity>(
                    new[] { testDate },
                    ObservableProperties.GetForApplicationEntity())));
    }
}
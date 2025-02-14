using EventLog.DbContext;
using EventLog.Interfaces;
using EventLog.Models;
using EventLog.Models.Entities;
using EventLog.Models.Enums;
using EventLog.Repository;
using EventLog.Service;

namespace EventLog;

internal class TestEventLog
{
    public async Task TestAsync(IEventLogService eventLogService,
        ITestDataRepository testDataRepository, int initiatorId) =>
            await eventLogService.CreateEventLogEntryAndProcessUnitOfWorkAsync(
                EventType.UpdateApplicationEntity, initiatorId,
                eventLogEntry => TestEventLogActionAsync(
                    eventLogEntry, eventLogService, testDataRepository));
    
    private async Task TestEventLogActionAsync(EventLogEntry eventLogEntry,
        IEventLogService eventLogService, ITestDataRepository testDataRepository)
    {
        const int initiatorId = 9;
        
        eventLogEntry.Details = "Test TestEventLogActionAsync is executed";
        Console.WriteLine("Test EventLog!");

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
            () => EventLogService.GetLogEntities(testDataRepository.GetOriginalPropertyValue,
                new EntityLogInfo<ApplicationEntity>(
                    new[] { testDate },
                    ObservableProperties.GetForApplicationEntity())));
    }
}
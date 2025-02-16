using System.Text;
using EventLog.Models.Entities;
using EventLog.Models.Enums;

namespace EventLog.Extensions;

public static class EventLogEntryExtensions
{
    public static void SetFailedStatusAndAddFailureDetails<TEventType, TEntityType>(
        this EventLogEntry<TEventType, TEntityType> model,
        EventStatus failedStatus, string description, string exceptionValue = null,
        bool explicitlyThrownException = false)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
    {
        model.Status = failedStatus;
        model.ExplicitlyThrownException = explicitlyThrownException;
        
        var failureDetailsCase = string.IsNullOrWhiteSpace(description)
            ? exceptionValue ?? string.Empty
            : new StringBuilder()
                .AppendLine(description)
                .AppendLine(exceptionValue ?? string.Empty)
                .ToString();
        
        model.FailureDetails = string.IsNullOrWhiteSpace(model.FailureDetails)
            ? failureDetailsCase
            : new StringBuilder()
                .AppendLine(model.FailureDetails)
                .AppendLine("--NEXT--")
                .AppendLine()
                .AppendLine(failureDetailsCase)
                .ToString();
    }
}
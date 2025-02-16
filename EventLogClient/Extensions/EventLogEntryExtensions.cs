using System.Text;
using EventLog.Models.Entities;
using EventLog.Models.Enums;

namespace EventLog.Extensions;

public static class EventLogEntryExtensions
{
    public static void SetFailedStatusAndAddFailureDetails(this EventLogEntry model,
        EventStatus failedStatus, string description, string exceptionValue = null,
        bool explicitlyThrownException = false)
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
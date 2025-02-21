using System.Text;
using AHSW.EventLog.Models.Entities;
using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Extensions;

public static class EventLogEntryExtensions
{
    public static void SetFailedStatusAndAddFailureDetails<TEventType, TEntityType, TPropertyType>(
        this EventLogEntry<TEventType, TEntityType, TPropertyType> model,
        EventStatus failedStatus, string description, string exceptionValue = null,
        bool explicitlyThrownException = false)
            where TEventType : struct, Enum
            where TEntityType : struct, Enum
            where TPropertyType : struct, Enum
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
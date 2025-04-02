using AHSW.EventLog.Models.Enums;

namespace AHSW.EventLog.Models;

public class FailureInfo
{
    /// <summary>
    /// String representation of the <see cref="EventStatus"/>.
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Failure details passed to the EventLog
    /// </summary>
    public string Message { get; set; }
    
    public string ExceptionMessage { get; set; }
    
    public string StackTrace { get; set; }
}
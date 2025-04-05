namespace AHSW.EventLog.Models.Enums;

public enum EventStatus : byte
{
    Successful = 1,
    HandledException = 2,
    UnhandledException = 3,
    TaskCancelledException = 4,
}
namespace AHSW.EventLog.Models.Enums;

public enum EventStatus : byte
{
    NotDefined = 0,
    Successful = 1,
    HandledException = 2,
    UnhandledException = 3,
    TaskCancelledException = 4,
}
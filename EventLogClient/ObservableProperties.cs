using EventLog.Enums;

namespace EventLog;

public static class ObservableProperties
{
    public static PropertyType[] GetForApplicationEntity() =>
        new []
        {
            PropertyType.ApplicationEntityTestString,
            PropertyType.ApplicationEntityTestBool,
            PropertyType.ApplicationEntityTestInt32
        };
}
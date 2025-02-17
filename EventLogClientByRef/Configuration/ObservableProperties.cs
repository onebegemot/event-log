using EventLog.Models.Enums;

namespace EventLog.Configuration;

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
namespace EventLog.Repository.Constants;

internal class EventLogPersistenceConstants
{
    public static string EventLogSchema => "eventlog";
    
    public static string EventLogTableName => "EventLog";
    
    public static string EntityLogTableName => "EntityLog";
    
    public static string BoolPropertyLogTableName => "BoolPropertyLog";
    
    public static string StringPropertyLogTableName => "StringPropertyLog";
    
    public static string Int32PropertyLogTableName => "Int32PropertyLog";
    
    public static string DecimalPropertyLogTableName => "DecimalPropertyLog";
    
    public static string EventTypeDescriptionsTableName => "EventTypeDescriptions";
    
    public static string EventStatusDescriptionsTableName => "EventStatusDescriptions";
}
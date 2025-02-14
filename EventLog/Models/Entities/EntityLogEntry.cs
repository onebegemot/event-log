using EventLog._NugetCode.Entities.Abstracts;
using EventLog.Models.Entities.PropertyLogEntryModels;
using EventLog.Models.Enums;

namespace EventLog.Models.Entities;

public class EntityLogEntry : PkEntity
{
    public ActionType ActionType { get; set; }
    
    public EntityType EntityType { get; set; }

    public int EntityId { get; set; }
    
    public int EventLogEntryId { get; set; }
    
    public EventLogEntry EventLogEntry { get; set; }
    
    public ICollection<BoolPropertyLogEntry> BoolPropertyLogEntries { get; set; }
    
    public ICollection<StringPropertyLogEntry> StringPropertyLogEntries { get; set; }
    
    public ICollection<Int32PropertyLogEntry> Int32PropertyLogEntries { get; set; }
    
    public ICollection<DecimalPropertyLogEntry> DecimalPropertyLogEntries { get; set; }

    public bool HasPropertyLogEntries =>
        (BoolPropertyLogEntries != null && BoolPropertyLogEntries.Any()) ||
        (StringPropertyLogEntries != null && StringPropertyLogEntries.Any()) ||
        (Int32PropertyLogEntries != null && Int32PropertyLogEntries.Any()) ||
        (DecimalPropertyLogEntries != null && DecimalPropertyLogEntries.Any());
}
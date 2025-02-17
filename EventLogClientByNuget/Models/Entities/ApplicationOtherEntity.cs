using EventLog.Interfaces.Entities;

namespace EventLog.Models.Entities;

public class ApplicationOtherEntity : IPkEntity
{
    public int Id { get; set; }
    
    public decimal TestDecimal { get; set; }
}
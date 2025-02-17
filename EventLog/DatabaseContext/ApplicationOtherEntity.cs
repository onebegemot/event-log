using EventLog.Interfaces;

namespace EventLog.DatabaseContext;

public class ApplicationOtherEntity : IPkEntity
{
    public int Id { get; set; }
    
    public decimal TestDecimal { get; set; }
}
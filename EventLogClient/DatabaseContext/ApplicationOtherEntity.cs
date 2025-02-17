using EventLog.Interfaces;
using EventLog.Interfaces.Entities;

namespace EventLog.DatabaseContext;

public class ApplicationOtherEntity : IPkEntity
{
    public int Id { get; set; }
    
    public decimal TestDecimal { get; set; }
}
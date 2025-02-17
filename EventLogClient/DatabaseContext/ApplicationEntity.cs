using EventLog.Interfaces;
using EventLog.Interfaces.Entities;

namespace EventLog.DatabaseContext;

public class ApplicationEntity : IPkEntity
{
    public int Id { get; set; }
    
    public DateTime TestDate { get; set; }
    
    public string TestString { get; set; }
    
    public bool TestBool { get; set; }
    
    public int TestInt32 { get; set; }
}
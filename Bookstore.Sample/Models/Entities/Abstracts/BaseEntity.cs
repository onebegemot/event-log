using AHWS.EventLog.Interfaces.Entities;

namespace EventLog.Models.Entities.Abstracts;

public abstract class BaseEntity : IPkEntity
{
    public int Id { get; set; }
}
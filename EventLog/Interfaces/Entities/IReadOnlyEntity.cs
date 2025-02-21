namespace AHSW.EventLog.Interfaces.Entities;

public interface IReadOnlyEntity : IPkEntity
{
    int? CreatedBy { get; set; }

    DateTime CreatedAt { get; set; }
}
namespace EventLog.Interfaces;

public interface IReadOnlyEntity : IPkEntity
{
    int? CreatedBy { get; set; }

    DateTime CreatedAt { get; set; }
}
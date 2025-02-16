using EventLog._NugetCode.Interfaces;

namespace EventLog._NugetCode.Entities.Abstracts;

public abstract class ReadOnlyEntity : PkEntity //, IReadOnlyEntity
{
    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}
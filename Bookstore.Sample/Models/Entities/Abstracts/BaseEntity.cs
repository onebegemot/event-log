using AHSW.EventLog.Interfaces.Entities;

namespace Bookstore.Sample.Models;

internal abstract class BaseEntity : IPkEntity
{
    public int Id { get; set; }
}
using EventLog.Models.Entities.Abstracts;

namespace EventLog.Models.Entities;

public class ShelfEntity : BaseEntity
{
    public decimal Height { get; set; }
    
    public ICollection<BookEntity> Books { get; set; }
}
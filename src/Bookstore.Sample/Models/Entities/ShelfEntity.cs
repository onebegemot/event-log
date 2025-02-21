namespace Bookstore.Sample.Models;

internal class ShelfEntity : BaseEntity
{
    public decimal Height { get; set; }
    
    public ICollection<BookEntity> Books { get; set; }
}
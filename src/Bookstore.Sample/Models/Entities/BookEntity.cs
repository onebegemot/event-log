namespace Bookstore.Sample.Models;

internal class BookEntity : BaseEntity
{
    public string Title { get; set; }
    
    public Condition Condition { get; set; }
    
    public Labels? Labels { get; set; }
    
    public DateTime Published { get; set; }
    
    public DateTime? FirstSale { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public double? Price { get; set; }
    
    public int LikeCount { get; set; }

    public int? ShelfId { get; set; }
    
    public ShelfEntity Shelf { get; set; }
}
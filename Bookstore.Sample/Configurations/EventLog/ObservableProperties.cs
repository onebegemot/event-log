namespace Bookstore.Sample.Configurations;

public static class ObservableProperties
{
    public static PropertyType[] GetForBookEntity() =>
        new []
        {
            PropertyType.BookTitle,
            PropertyType.BookIsAvailable,
            PropertyType.BookLikeCount
        };
    
    public static PropertyType[] GetForApplicationShelfEntity() =>
        new []
        {
            PropertyType.ShelfHeight
        };
}
namespace Bookstore.Sample.Configurations;

internal static class ObservableProperties
{
    public static PropertyType[] GetForBookEntity() =>
        new []
        {
            PropertyType.BookTitle,
            PropertyType.BookIsAvailable,
            PropertyType.BookLikeCount
        };
    
    public static PropertyType[] GetForShelfEntity() =>
        new []
        {
            PropertyType.ShelfHeight
        };
}
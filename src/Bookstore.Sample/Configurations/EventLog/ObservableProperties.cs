namespace Bookstore.Sample.Configurations;

internal static class ObservableProperties
{
    public static PropertyType[] GetForBookEntity() =>
        new []
        {
            PropertyType.BookTitle,
            PropertyType.BookCondition,
            PropertyType.BookLabels,
            PropertyType.BookPublished,
            PropertyType.BookFirstSale,
            PropertyType.BookIsAvailable,
            PropertyType.BookLikeCount,
            PropertyType.BookPrice
        };
    
    public static PropertyType[] GetForShelfEntity() =>
        new []
        {
            PropertyType.ShelfHeight
        };
}
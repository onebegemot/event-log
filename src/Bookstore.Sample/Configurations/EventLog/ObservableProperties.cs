﻿namespace Bookstore.Sample.Configurations;

internal static class ObservableProperties
{
    public static PropertyType[] GetForBookEntity() =>
        new []
        {
            PropertyType.BookTitle,
            PropertyType.BookPublished,
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
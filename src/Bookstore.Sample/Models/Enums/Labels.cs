namespace Bookstore.Sample.Models;

[Flags]
internal enum Labels
{
    Bestseller = 1 << 0,
    Discount = 1 << 1 
}
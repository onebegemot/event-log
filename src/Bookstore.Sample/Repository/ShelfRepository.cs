using Bookstore.Sample.DatabaseContext;
using Bookstore.Sample.Interfaces;
using Bookstore.Sample.Models;

namespace Bookstore.Sample.Repository;

internal class ShelfRepository :
    BaseRepository<ShelfEntity>,
    IShelfRepository
{
    public ShelfRepository(BookstoreDbContext dbContext)
        : base(dbContext)
    {
    }
}
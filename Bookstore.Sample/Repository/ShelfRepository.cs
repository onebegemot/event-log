using Bookstore.Sample.Configuration.DatabaseContext;
using EventLog.Models.Entities;
using EventLog.Repository.Abstracts;
using EventLog.Repository.Interfaces;

namespace Bookstore.Sample.Configuration.Repository;

public class ShelfRepository :
    BaseRepository<ShelfEntity>,
    IShelfRepository
{
    public ShelfRepository(BookstoreDbContext dbContext)
        : base(dbContext)
    {
    }
}
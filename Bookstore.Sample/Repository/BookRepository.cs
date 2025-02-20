using Bookstore.Sample.Configuration.DatabaseContext;
using EventLog.Models.Entities;
using EventLog.Repository.Abstracts;
using EventLog.Repository.Interfaces;

namespace Bookstore.Sample.Configuration.Repository;

public class BookRepository :    
    BaseRepository<BookEntity>,
    IBookRepository
{
    public BookRepository(BookstoreDbContext dbContext)
        : base(dbContext)
    {
    }
}
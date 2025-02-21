using Bookstore.Sample.DatabaseContext;
using Bookstore.Sample.Interfaces;
using Bookstore.Sample.Models;

namespace Bookstore.Sample.Repository;

internal class BookRepository :    
    BaseRepository<BookEntity>,
    IBookRepository
{
    public BookRepository(BookstoreDbContext dbContext)
        : base(dbContext)
    {
    }
}
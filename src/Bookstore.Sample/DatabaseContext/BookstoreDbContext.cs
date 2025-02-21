using AHSW.EventLog.DatabaseContext;
using Bookstore.Sample.Configurations;
using Bookstore.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Sample.DatabaseContext;

internal class BookstoreDbContext :
    EventLogDbContext<BookstoreDbContext, EventType, EntityType, PropertyType>
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<BookEntity> ApplicationEntities { get; set; }
    
    public DbSet<ShelfEntity> ApplicationOtherEntities { get; set; }
}
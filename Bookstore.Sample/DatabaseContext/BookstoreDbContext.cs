using AHWS.EventLog.DatabaseContext;
using Bookstore.Sample.Configurations;
using EventLog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Sample.Configuration.DatabaseContext;

public class BookstoreDbContext :
EventLogDbContext<BookstoreDbContext, EventType, EntityType, PropertyType>
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<BookEntity> ApplicationEntities { get; set; }
    
    public DbSet<ShelfEntity> ApplicationOtherEntities { get; set; }
}
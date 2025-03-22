using AHSW.EventLog.Repositories.Extensions;
using Bookstore.Sample.Configurations;
using Bookstore.Sample.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Sample.DatabaseContext;

internal class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<BookEntity> Books { get; set; }
    
    public DbSet<ShelfEntity> Shelves { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookstoreDbContext).Assembly);
        modelBuilder.ApplyEventLogConfigurations<EventType, EntityType, PropertyType>();
    }
}
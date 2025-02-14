using Microsoft.EntityFrameworkCore;

namespace EventLog.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ApplicationEntity> TestData { get; set; }
}
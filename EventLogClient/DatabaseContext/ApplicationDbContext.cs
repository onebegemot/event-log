using Microsoft.EntityFrameworkCore;

namespace EventLog.DatabaseContext;

public class ApplicationDbContext : EventLogDbContext<ApplicationDbContext>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ApplicationEntity> TestData { get; set; }
}
using EventLog.Enums;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.DatabaseContext;

public class ApplicationDbContext : EventLogDbContext<ApplicationDbContext, EventType, EntityType>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ApplicationEntity> TestData { get; set; }
}
using EventLog.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.DatabaseContext;

public class ApplicationDbContext :
EventLogDbContext<ApplicationDbContext, EventType, EntityType, PropertyType>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ApplicationEntity> TestData { get; set; }
}
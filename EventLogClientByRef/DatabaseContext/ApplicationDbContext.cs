using EventLog.Models.Entities;
using EventLog.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventLog.DatabaseContext;

public class ApplicationDbContext :
EventLogDbContext<ApplicationDbContext, EventType, EntityType, PropertyType>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<ApplicationEntity> ApplicationEntities { get; set; }
    
    public DbSet<ApplicationOtherEntity> ApplicationOtherEntities { get; set; }
}
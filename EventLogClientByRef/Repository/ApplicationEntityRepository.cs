using EventLog.DatabaseContext;
using EventLog.Interfaces;
using EventLog.Models.Entities;

namespace EventLog.Repository;

public class ApplicationEntityRepository :    
    BaseRepository<ApplicationEntity>,
    IApplicationEntityRepository
{
    public ApplicationEntityRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
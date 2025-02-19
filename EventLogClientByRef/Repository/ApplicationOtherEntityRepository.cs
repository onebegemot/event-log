using EventLog.DatabaseContext;
using EventLog.Interfaces;
using EventLog.Models.Entities;

namespace EventLog.Repository;

public class ApplicationOtherEntityRepository :
    BaseRepository<ApplicationOtherEntity>,
    IApplicationOtherEntityRepository
{
    public ApplicationOtherEntityRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
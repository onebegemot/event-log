using AHWS.EventLog.Interfaces;
using Bookstore.Sample.Configurations;
using EventLog.Repository.Interfaces;

namespace EventLog.Models.Entities;

public class ResolvedServices
{
    public IEventLogService<EventType, EntityType, PropertyType> EventLog { get; init; }
    
    public IBookRepository BookRepository { get; init;}
    
    public IShelfRepository ShelfRepository { get; init;}
}
![LastNuGetVersion](https://img.shields.io/nuget/v/AHSW.EventLog)
![NugetDownloads](https://img.shields.io/nuget/dt/AHSW.EventLog)

<a href="https://github.com/cat-begemot/event-log/tree/master/src/EventLog">
	<img width="111" height="111" src="https://github.com/cat-begemot/event-log/blob/master/images/logo-1.png"/>
</a>

## Event Log
EventLog is a library that identifies application events, records diagnostic and statistical information about them, and stores them in a relational database for further analysis. Within the scope of an application event, it has the ability to track the state of application domain models.

The concept behind using EventLog is to gather information about application activity and domain model mutations for various purposes. Some possible examples of its use include:
- Monitoring the health state of a long-running application
- Identifying performance issues
- Being aware of user activity in the system, tracking who is responsible for changing any property of domain models
- Keeping a history of domain model changes
- Collecting statistics on user controls and application features to improve the UI
- Analyzing what happens in the application through API invocation and data changes for internal investigations

### Usage Example in Code  
As an example, imagine the following code is an API endpoint that creates a domain model, **Book**, and saves it in storage.  
As a wrapper, **EventLog** records the ```AddBooksOnShelf``` event, adds some detailed information to the ```EventLogEntry```, executes the initial repository method while simultaneously adding an ```EntityLogEntry``` related to the ```EventLogEntry```, and records the ```Book``` property value states.  

```cs
var book = CreateBookEntity();

await services.EventLog.CreateEventScopeAndRun(
    EventType.AddBooksOnShelf,
    async eventLogScope =>
    {
	eventLogScope.EventLogEntry.CreatedBy = userId;
	eventLogScope.EventLogEntry.Details = "Adding a book";
    
	await eventLogScope.SaveAndLogEntitiesAsync(
	    () => services.BookRepository.AddOrUpdateAsync(book),
	    options => options
		.AddEntityLogging(
		    services.BookRepository.GetOriginalPropertyValue,
		    new[] { book },
		    PropertyType.BookTitle,
		    PropertyType.BookLikeCount));
    });
```

### Database Result Output  
As a result, **EventLog** tables will contain the following data:  

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/EventLog_Raw.png"/> | 
|:--:| 
| <b>Figure 1.1 - EventLog table content</b> |

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/EntityLog_Raw.png"/> | 
|:--:| 
| <b>Figure 1.2 - EntityLog table content</b> |

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/StringPropertyLog_Raw.png"/> | 
|:--:| 
| <b>Figure 1.3 - StringPropertyLog table content</b> |

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/Int32PropertyLog_Raw.png"/> | 
|:--:| 
| <b>Figure 1.4 - Int32PropertyLog table content</b> |

### Database Result Output (User-Friendly View for Analytical Purposes)  
Using join queries, the output might be more user-friendly:  

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/EventLog_Pretty.png"/> | 
|:--:| 
| <b>Figure 1.1 - EventLog table view</b> |

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/EntityLog_Pretty.png"/> | 
|:--:| 
| <b>Figure 1.2 - EntityLog table view</b> |

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/StringPropertyLog_Pretty.png"/> | 
|:--:| 
| <b>Figure 1.3 - StringPropertyLog table view</b> |

| <img height="50" src="https://github.com/cat-begemot/event-log/blob/master/images/Samples/Int32PropertyLog_Pretty.png"/> | 
|:--:| 
| <b>Figure 1.4 - Int32PropertyLog table view</b> |

## Sample Project
[Bookstore](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample) is a sample console application to demonstrate the way of configuring EventLog and the most common use cases.  

## How to use

### Define TEventType, TEntityType, and TPropertyType empty enums
These enums will be filled later, when required event and property logging is added to the code. It is desirable to follow the next suggestions.
* Explicitly numerate enum members to avoid damaging the log in the future
```cs
internal enum EventType
{
    // Books = 1000
    AddBooksOnShelf = 1001, // Explicit number is assigned to each enum member
    UpdateBooksOnShelf = 1002,
    
    // Shelves = 2000
    AddShelf = 2001,
    DeleteShelf = 2002,
}
```
* Group enum members to simplify further maintenance and extends types
```cs
internal enum PropertyType
{
    // Book = 1000 - All properties grouped by entity
    BookTitle = 1001,
    BookPublished = 1002,
    BookIsAvailable = 1003,
    BookLikeCount = 1004,
    BookPrice = 1005,
    BookFirstSale = 1006,
    BookCondition = 1007,
    BookLabels = 1008,
    
    // Shelf = 2000
    ShelfHeight = 2001
```
* Make consistency numeration between TEntityType and TPropertyType
```cs
internal enum EntityType
{
    Book = 1000, // PropertyType has Book entity group that starts with 1000 as well
    Shelf = 2000
}
```

### Each application EF entity must implement IPkEntity interface

### Embed EventLog tables to the application database context
* Add `modelBuilder.ApplyEventLogConfigurations<EventType, EntityType, PropertyType>();` to the overriden OnModelCreating method in the application database context class. Make sure it goes first in the settings in roder to apply all database schemes correctly
```cs
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyEventLogConfigurations<EventType, EntityType, PropertyType>(); // Must be first
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookstoreDbContext).Assembly);
    }
```
* Create and apply the new migration with EventLog convfigurations

### Add GetOriginalPropertyValue to the base application repository
```cs
    public object GetOriginalPropertyValue(TEntity entity, string propertyName) =>
        _dbContext.Entry(entity).Property(propertyName).OriginalValue;
```

### Add ObservableProperties static class with predefined methods with observable property collection for convenient and consistent using
```cs
internal static class ObservableProperties
{
    public static PropertyType[] GetForBookEntity() =>
        new []
        {
            PropertyType.BookTitle,
            PropertyType.BookCondition,
        };
    
    public static PropertyType[] GetForShelfEntity() =>
        new []
        {
            PropertyType.ShelfHeight
        };
}
```

### Register EventLog service
```cs
services.AddEventLog<BookstoreDbContext, EventType, EntityType, PropertyType>();
```

### Configure EventLog service on the application startup
See more detail example in the [Program.cs](https://github.com/cat-begemot/event-log/blob/master/src/Bookstore.Sample/Program.cs).
Setting up enum member custom names is a convenient way to make look of SQL query results more user-friendly.
All observable properties and appropriate entities must be resigtered in the service configuration.
```cs
EventLogServiceConfiguration<EventType, EntityType, PropertyType>.Configure<BookstoreDbContext>(
                configurationBuilder => configurationBuilder
                    .UseCustomTypeDescriptions(context,
                        options => options
                            .AddEventTypeDescription(EventType.AddBooksOnShelf, "Add books on a shelf")
                            .AddEventTypeDescription(EventType.UpdateBooksOnShelf, "Update books on a shelf")
                            .AddEntityTypeDescription(EntityType.Book, "Book")
                            .AddEntityTypeDescription(EntityType.Shelf, "Shelf")
                            .AddPropertyTypeDescription(PropertyType.BookTitle, "Title")
                            .AddPropertyTypeDescription(PropertyType.ShelfHeight, "Height")
                    )
                    .RegisterEntity<BookEntity>(EntityType.Book,
                        options => options
                            .RegisterProperty(PropertyType.BookTitle,
                                x => x.Title, nameof(BookEntity.Title))
                    )
                    .RegisterEntity<ShelfEntity>(EntityType.Shelf,
                        options => options
                            .RegisterProperty(PropertyType.ShelfHeight,
                                x => x.Height, nameof(ShelfEntity.Height))
                    )
            );
        });
```

### Add EventLog recording
See more examples in the [Program.cs](https://github.com/cat-begemot/event-log/blob/master/src/Bookstore.Sample/Program.cs).
All entity changes must be made before SaveAndLogEntitiesAsync() invocation. Inside this method entities must be only updated in repository and saves.
```cs
        var book = CreateBookEntity();

        await services.EventLog.CreateEventScopeAndRun(
            EventType.AddBooksOnShelf, // Specify event logging level here
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "Adding a book";
            
                await eventLogScope.SaveAndLogEntitiesAsync(
                    () => services.BookRepository.AddOrUpdateAsync(book), // All entity changes must be made before SaveAndLogEntitiesAsync() invocation
                    options => options
                        .AddEntityLogging( // Specify entity level logging here
                            services.BookRepository.GetOriginalPropertyValue,
                            new[] { book }, ObservableProperties.GetForBookEntity) // Specify property level logging here
                );
            }
        );
```

### Use SQL queries for log investigation
Samples of SQL queries can be found [here](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts)

### Use log tables in any way in code for extend application functionality

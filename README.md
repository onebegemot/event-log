![LastNuGetVersion](https://img.shields.io/nuget/v/AHSW.EventLog)
![NugetDownloads](https://img.shields.io/nuget/dt/AHSW.EventLog)

<a href="https://github.com/cat-begemot/event-log/tree/master/src/EventLog">
	<img width="150" height="150" src="https://github.com/cat-begemot/event-log/blob/master/images/logo-1.png"/>
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

### Sample Project
[Bookstore](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample) is a sample console application to demonstrate the way of configuring EventLog and the most common use cases.  


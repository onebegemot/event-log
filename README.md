![LastNuGetVersion](https://img.shields.io/nuget/v/AHSW.EventLog)
![NugetDownloads](https://img.shields.io/nuget/dt/AHSW.EventLog)

<a href="https://github.com/cat-begemot/event-log/tree/master/src/EventLog">
	<img width="150" height="150" src="https://github.com/cat-begemot/event-log/blob/master/images/logo-1.png"/>
</a>

## Event Log
EventLog is a library that identifies application events, records diagnostic and statistical information about them, and stores them in a relational database for further analysis. Within the scope of an application event, it has the ability to track the state of application domain models.

The concept behind using EventLog is gathering information of application activity and domain models mutation for different purposes. Some possible example of using:
- Monitoring helth state for a long running application
- Indetify performance issues
- Be aware about users activity in the system. Traking who brings responsibility of changing any property of domain models
- Keep history of changing domain models
- Statistics of using user controls and application features for making decisions of improving UI
- Analyze what happens in the application throw API invokation and data changing for any kind of internal investigation
### Usage example in code
As an example imagine the following code is an API endpoint which creates a domain model Book and saves it in the storage.
As a wrapper the <b>EventLog</b> records the ```AddBooksOnShelf``` event, add some details info into the ```EventLogEntry```, executes the initial repository method simultaneously adding an ```EntityLogEntry``` related to the ```EventLogEntry```, and record the ```Book``` property value states.

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
### Database result output
As a result <b>EventLog</b> tables will contain the following data:

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

### Database result output (user-friendly view for analitical purposes)
Using join queries the output might be more user-friendly:

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
[Bookstore](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample) is a sample console application to demonstrate they way of configuring EventLog. and most often use cases.

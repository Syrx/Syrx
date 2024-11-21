# Quick Start
This quick start builds on concepts incrementally. We start with very basic structures and then get more and more advanced as we go. 


# Context
In this quick start we're going to build a fictional database, domain data models, repositories, services and web APIs to go along with it. 
We'll be using SQL Server for our sample database which you can find [here](sample-database.md).

## The Database
The sample database for this quick start can be found on the [Sample Database](sample-database.md) file.


## Model | Country
In this tutorial we'll start with a basic domain model representing a country entry on a database. This model will evolve significantly over later iterations.
Somehting to note is that the property names on the model differ from the field names on the table. 

In this quick start we're using a `record` type deliberately although we can 


```csharp
public record Country(string Code, string Name);
```


## Repository | Country
Next, we define an interface for the repository. Our interface will have both read and write methods by the time we're finished. 
For the time being, we're going to start off with a simple `RetrieveAll` method which retrieves a paginated list of `Country` objects. 


#### Country Repository | Interface
```csharp
public interface ICountryRepository
{
    IEnumerable<Country> RetrieveAll(int page = 1, int size = 100);
}
```
#### Country Repository | Implementation
The implementation of the `ICountryRepository` is really quite straightforward. We inject `ÌCommander<CountryRepository>` 

```csharp
public class CountryRepository : ICountryRepository
{
    private readonly ICommander<CountryRepository> _commander;

    public CountryRepository(ICommander<CountryRepository> commander) => _commander = commander;

    public IEnumerable<Country> RetrieveAll(int page = 1, int size = 100) => _commander.Query<Country>(new { page, size });
}
```

##### Country Repository | Primary Constructors Example
If you're using _[Primary Constructors](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors)_ the code becomes even more concise. 

```csharp
public class CountryRepository(ICommander<CountryRepository> commander) : ICountryRepository
{
    public IEnumerable<Country> RetrieveAll(int page = 1, int size = 100) => commander.Query<Country>(new { page, size });
}
```

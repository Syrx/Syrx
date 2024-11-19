# Quick Start
This quick start builds on concepts incrementally. We start with very basic structures and then get more and more advanced as we go. 


# Context
In this quick start we're going to build a fictional database, domain data models, repositories, services and web APIs to go along with it. 
We'll be using SQL Server for our sample database which you can find [here](sample-database.md).

## The Database
The sample database for this quick start can be found on the [Sample Database](sample-database.md) file.


## The Solution
Using your favourite IDE, create a new solution called `Syrx.Samples` with this layout. 

##### Layout
```
Syrx.Samples
- src
- tests
-- integration
-- unit
```

### The Projects
For this quick start, we recommend using our [solution layout](solution-layouts.md).


### Project | Domain Data Models 
With Syrx, your domain data model can also serve as a DTO (Data Transfer Object). Unlike some other frameworks you don't have to map from one type to another. 
Moreover, Syrx supports (and encourages) using immutable complex types. This offers incredible benefits for unit and integration testing as well as allowing you to write extremely robust code. 

In the `src` folder, create a new project called `Syrx.Samples.Models`.    
In the `tests\unit` folder create a corresponding xUnit project called `Syrx.Samples.Models.Tests.Unit`

##### Layout
```
Syrx.Samples
- src
-- Syrx.Samples.Models
- tests
-- integration
-- unit
--- Syrx.Samples.Models.Tests.Unit
```

### Project | Repository

In the `src` folder, create a new project called `Syrx.Samples.Repositories`.    
In the `tests\integration` folder create a corresponding xUnit project called `Syrx.Samples.Repositories.Tests.Integration`

##### Layout
```
Syrx.Samples
- src
-- Syrx.Samples.Models
-- Syrx.Samples.Repositories
- tests
-- integration
--- Syrx.Samples.Repositories.Tests.Integration
-- unit
--- Syrx.Samples.Models.Tests.Unit
```

### Model | Country
In this tutorial we'll start with a basic domain model representing a country entry on a database. This model will evolve significantly over later iterations.
Somehting to note is that the property names on the model differ from the field names on the table. 

In this quick start we're using a `record` type deliberately although we can 


```csharp
public record Country(string Code, string Name);
```


### Repository | Country
Next, we define an interface for the repository. Our interface will have both read and write methods by the time we're finished. 
For the time being, we're going to start off with a simple `RetrieveAll` method which retrieves a paginated list of `Country` objects. 


#### Country Repository | Interface
```csharp
public interface ICountryRepository
{
    Task<IEnumerable<Country>> RetrieveAllAsync(int page = 1, int size = 100, CancellationToken cancellationToken = default);
}
```

#### Country Repository | Implementation
The implementation of the `ICountryRepository` is really quite straightforward.  

```csharp
public class CountryRepository : ICountryRepository
{
    private readonly ICommander<CountryRepository> _commander;

    public CountryRepository(ICommander<CountryRepository> commander)
    {
        _commander = commander;
    }
    
    public async Task<IEnumerable<Country>> RetrieveAllAsync(int page = 1, int size = 100, CancellationToken cancellationToken = default)
    {
        return await _commander.QueryAsync<Country>(new { page, size }, cancellationToken);
    }
}
```


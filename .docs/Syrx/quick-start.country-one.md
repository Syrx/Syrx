# Quick Start 

## Context
In this quick start we'll show you how to wire Syrx up to a database to persist and retrieve a simple POCO object. We'll be using SQL Server 2019, .NET 8 and C# 12.

We're keeping this sample deliberately simple so will not be including stuff like input validation, unit and integration testing, etc. in this sample (but will in others).
We're also sticking with synchronous methods instead of async methods so that we're really focusing on the wire up and not distracted by any async nuances. 

**NOTE:** Although they won't be part of this sample, we strongly recommend following a TDD approach and validating your inputs. 


### What to expect
In this quick start we're going to write a simple repository to persist and retrieve a domain data model representing a `Country` object. 
While we'll be keeping this sample deliberately simple, structurally it won't be much different from what you'd expect to see from a regular N-tier system. 

---

## The Database
Let's start off by creating a simple table to hold a list of country codes and the corresponding country names. 

```sql
drop table if exists [dbo].[country];

create table [dbo].[country]
(
    [code] varchar(2) not null
   ,[name] varchar(255) not null
constraint [pk_country] primary key ([code] asc)
);

```

---

## The Model
Next, we'll create a POCO object to represent a domain data model. 

```csharp
public record Country(string Code, string Name);
```
---
## The Repository
Now we'll start implementing repository CRUD (Create, Retrieve, Update, Delete) methods. We'll implement each method individually and talk through each one. 
We'll start with the `RetrieveAll` method.  

### Interface
The interface is a pretty straight forward set of CRUD operations. 

You might notice that we haven't inherited from `IDisposable`. 
This is because all Syrx database commander methods dispose of managed connections implicitly as part of their implementation. 
Your own code may need to inherit from `IDisposable` for reasons outside of the Syrx usage. 


```csharp
public interface ICountryRepository
{
    Country Create(Country country);
    Country Retrieve(string code);
    IEnumerable<Country> RetrieveAll(int page = 1, int size = 100);
    Country Update(Country country);
    Country Delete(Country country);
}
```

### Implementation
This is what a default implementation of the `CountryRepository` might look like. We'll start making changes to it bit by bit. 

```csharp
public class CountryRepository : ICountryRepository
{
    public Country? Create(Country country) => throw new NotImplementedException();
    
    public Country? Retrieve(string code) => throw new NotImplementedException();
    
    public IEnumerable<Country> RetrieveAll(int page = 1, int size = 100) => throw new NotImplementedException();
    
    public Country? Update(Country country) => throw new NotImplementedException();
    
    public Country? Delete(Country country) => throw new NotImplementedException();
}
``` 

#### Injecting the ICommander
First things first - let's inject an `ICommander<>` into the implementation. 
The `ICommander<>` accepts a generic type so that we can easily wire up the repository code to the SQL. We'll see more on that later. 

Add these two lines to the repository to inject an instance of `ICommander<CountryRepository>`. 

```csharp
private readonly ICommander<CountryRepository> _commander;

public CountryRepository(ICommander<CountryRepository> commander) => _commander = commander;

```

#### RetrieveAll
Next, we'll start off by implementing the `RetrieveAll` method as there's not much to it - we make a call to the `Query<>` method and pass in an anonymous type to host our pagination parameters.

Replace the default implementation with this line:

```csharp
public IEnumerable<Country> RetrieveAll(int page = 1, int size = 100) => _commander.Query<Country>(new { page, size });
```

After we wire up the config thie method will return a paginated list of `Country` objects. 
Notice how we're passing in the `page` and `size` parameters as an anonymous type - `new { page, size }`. We can pass in complex types as parameters as well, exactly as you would with _Dapper_.

#### Retrieve
For the `Retrieve` method we're going to do something much the same with a small difference - as the signature for all `Query<>` methods in Syrx return an `IEnumerable<>` we will need to limit the response to a single or default value. 

Replace the default implementation of the `Retrieve` method with this line: 


```csharp
public Country? Retrieve(string code) => _commander.Query<Country>(new { code }).SingleOrDefault();
```

#### Create, Update, Delete
All three of these methods are write operations and will use the `Execute<>` method.    
The `Execute` method returns a `bool`. 
Now, this is interesting to note because of what this means for callers. 
As a caller to the repository, you should expect one of three reponses to a state changing operation: 

* **Success:** If the operation is successful, you should recieve an instance of `Country` as a result. In fact, you should get the exact same instance as the one that was passed. 
* **Exception:** If the operation throws an exception, perhaps a primary key violation or deadlock, you will want the excpetion to be returned back to the caller. 
* **Null:** This is the most rare of outcomes. If the operation was not successful - for a valid reason - you may want to expect `null` as a result. For instance, if you were calling an update statement on the underlying store and the record did not exist, you may want to return null instead of throwing an exception. Returning the same instance would indicate a successful result and would be untrue.

As a result, our method call will be 
```csharp
_commander.Execute(country) ? country : null;
```

**NOTE:** Unlike our `Retrieve*` methods, we're passing the domain data model in as is. 

Because all three of these methods have the same signature, we can implement all three at once. 
After we've added the write support our repository now looks like this. 

```csharp
public class CountryRepository : ICountryRepository
{
    private readonly ICommander<CountryRepository> _commander;

    public CountryRepository(ICommander<CountryRepository> commander) => _commander = commander;

    public Country? Create(Country country) => _commander.Execute(country) ? country : null;

    public Country? Retrieve(string code) => _commander.Query<Country>(new { code }).SingleOrDefault();

    public IEnumerable<Country> RetrieveAll(int page = 1, int size = 100) => _commander.Query<Country>(new { page, size });

    public Country? Update(Country country) => _commander.Execute(country) ? country : null;

    public Country? Delete(Country country) => _commander.Execute(country) ? country : null;
}
```
---
# Wire Up
Syrx offers a unique configuration structure in that it mimics the structure of your code. In most scenarios where you need to execute code against a database, that SQL statement will be executed from a specific method. Syrx uses the fully qualified type and method names from your code to resolve the command to be executed against the underlying
datastore. 

There's a wide set of configration extensions to simplify and support configuration. We offer both a declarative, very flexible, code-first approach as well as configuration files. 

In this sample, we'll be following the declarative approach. 

**NOTE:** You're absolutely welcome to wire Syrx up in anyway that you like. You don't have to follow this approach at all if it doesn't suit your needs. 

Considering the code we've written above, we can see that the we want the `RetrieveAll` method of the `CountryRepository` to execute our SQL query against an underlying data store. 
Structurally, you could think of it as being part of a hierarchy:

```
Syrx.Samples                                // <-- the namespace
    CountryRepository                       // <-- the type
        RetrieveAllAsync                    // <-- the method
            select * from [dbo].[country]   // <-- the SQL
```

And it's in considering that hierarchy that we see how Syrx configuration mimics your code structure. 

### Install Package
To install declarative configuration support for SQL Server, install the `Syrx.SqlServer.Extensions` package from nuget. 

```powersehll
install-package Syrx.SqlServer.Extensions
```

### Dependency Injection
Wherever you handle your dependency injection bootstrapping, add a new extension method to accept and return an `IServiceCollection` instance. 

```csharp
public static IServiceCollection Install(this IServiceCollection services)
{
    return services
        .UseSyrx(factory => factory
            .UseSqlServer(builder => builder
                .AddConnectionString("syrx", "data source=(localdb)\\mssqllocaldb;initial catalog=samples;integrated security=SSPI")
                .AddCommand(command => command
                    .ForType<CountryRepository>(type => type
                        .ForMethod(nameof(CountryRepository.RetrieveAll), method => method
                            .UseConnectionAlias("syrx")
                            .UseCommandText(@"select [Code]
                                                    ,[Name]
                                              from [dbo].[country]
                                              order by [code] offset (@page - 1) * @size rows fetch next @size rows only;"))
                    )
                )
            )
        )
    );
}
```


# Conclusion
This sample was meant to take you a little deeper into how to use Syrx beyond what you see on the [readme](readme.md). You can download a copy of this sample from our Samples repository.
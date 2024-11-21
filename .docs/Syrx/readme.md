# Syrx

Syrx allows you to write the same repository code to target multiple different databases. 
It is a framework which decouples your .NET repository code from the underlying data store, allowing you to shield your 
repository code from the underlying storage provider. It's not quite an ORM although it uses a micro-ORM to materialize and persist your objects.

Syrx places emphasis on:
* _Control_: You should be in control of your data and execution. 
* _Speed_: Performance is a feature. Syrx inherits its speed from Dapper.
* _Flexibilty_: Your choice of database technology should be as easy to change as any other component in your solution. 
* _Testability_: We believe strongly in the power of testing at all levels of your applications. Syrx is fully testable and fully tested.
* _Extensibility_: Syrx is granular and componentised, allowing you two swap out components for your own needs. 
* _Readability_: Syrx aims to keep the intent of your code clear and concise. 

## Quick Look
Syrx is predicated on the concept that all operations against a data store are fundamentally either a read or write operation.
The central construct in Syrx is the ICommander interface. This interface exposes overloads of only 2 methods, `Query` and `Execute` 
(as well as their async implementations `QueryAsync` and `ExecuteAsync`. 


* `Query` methods are used to retrieve data.
* `Execute` methods are used to create/modify/delete data. 

### Query
Syrx allows you to retrieve both primitive and complex types from the underlying database very simply. 
There are no special attribute declarations, no special inheritance structures. Very often, complex 
types can be retrieved with a single line of code. More complex types can be composed using predicates. 
Syrx supports Func<> predicate with up to 16 inputs for composition. 


_Example_

Let's assume your app needs to retrieve a list of countries. 
With Syrx, this is a single line of C# code. 

```csharp
// retrieving a collection of countries
public IEnumerable<Country> RetrieveAll() => _commander.Query<Country>();

// retrieving a collection of countries asynchronously
public async Task<IEnumerable<Country>> RetrieveAllAsync() => await _commander.QueryAsync<Country>();

// with parameters
public IEnumerable<Country> RetrieveAll(string languageSpoken) => _commander.Query<Country>(new { languageSpoken });

// with separate mapping predicate (assume mapping Language and Currency to a Country type)
public IEnumerable<Country> RetrieveAll() => _commander.Query(Compose.ToCountry);

// composition predicate stored in a separate class, keeping your repository
// cleaner and more readable. 
private static class Compose
{
    // composition predicate providing you with full control over how your complex type is 
    // constructed. in this example, Country, Language and Country are all complex types.     
    static Func<Country, Language, Currency, Country> ToCountry(country, language, currency) => 
    {
        country.Language = language;
        country.Currency = currency;
        return country;
    };
}
```

### Execute
Persistence with Syrx follows a similarly simple signature. Syrx implementations against RDBMS vendors
are transactional by default. Any exceptions thrown during the Execute method are bubbled back up the 
stack with their stack trace preserved. 

_Execute_ methods return bool (or Task<bool> for async calls) so returning the peristed instance is 
done using a conditional operator. 

As Update/Delete are essentially variants of a write operation, they can use the exact same signature as
Create. The differentiation is made by the SQL supplied for the method. 

#### Example
```csharp
// persisting the country type
public Country Create(Country country) => _commander.Execute(country) ? country : null;

// persisting the country type asynchronously
public async Task<Country> Create(Country country) => await _commander.ExecuteAsync(country) ? country : null;

// update
public Country Update(Country country) => _commander.Execute(country) ? country : null;

// delete
public Country Delete(Country country) => _commander.Execute(country) ? country : null;
```

## Configuration
Syrx offers a unique configuration structure in that it mimics the structure of your code. In most scenarios where you need to execute code against a database, that SQL statement will be executed from a specific method. Syrx uses the fully qualified type and method names from your code to resolve the command to be executed against the underlying
datastore. 

There's a wide set of configration extensions to simplify and support configuration. 

#### Example
Using our `Country` example from earlier, let's assume we we have the repository definition below. Our repository retrieves a list of countries stored in our database. Using a very simple select statement: 

```sql
select * from [dbo].[country];
```

We have a very simple repository type which we wrote to handle the various CRUD operation for this domain model. The expectation is that the `RetrieveAllAsync` method will execute that very simple SQL statement. 

```csharp
namespace Syrx.Samples
{
    public class CountryRepository(ICommander<CountryRepository> commander) : ICountryRepository
    {
        // instance of our ICommander passed in via dependency injection. 
        private readonly ICommander<CountryRepository> _commander = commander;

        // the method responsible for executing our select statement. 
        public async Task<IEnumerable<Country>> RetrieveAllAsync(CancellationToken cancellationToken = default)
        {
            return await _commander.QueryAsync<Country>(cancellationToken: cancellationToken);
        }
    }    
}
```

Looking at this code, we can see that the we want the `RetrieveAllAsync` method of the `CountryRepository` to execute our SQL query against an underlying data store. 
Structurally, you could think of it as being part of a hierarchy:

```
Syrx.Samples                                // <-- the namespace
    CountryRepository                       // <-- the type
        RetrieveAllAsync                    // <-- the method
            select * from [dbo].[country]   // <-- the SQL
```

And it's in considering that hierarchy that we see how Syrx configuration mimics your code structure. 

To wire this up to a SQL Server database:
* First install the [Syrx.SqlServer.Extensions](https://www.nuget.org/packages/Syrx.SqlServer.Extensions) package `install-package Syrx.SqlServer.Extensions`.
* Then start wiring up the SQL to be executed against the method which will execute it. 
* This declarative approach employs the builder pattern and several different builders to make the wiring up simple and less open to error.

```csharp
namespace Syrx.Samples
{
    public static class SyrxInstaller
    {
        public static  IServiceCollection Install(this IServiceCollection services)
        {   
            return services.UseSyrx(builder =>                                                  // call the UseSyrx extension method on IServiceCollection. 
                builder.UseSqlServer(sqlServer =>                                               // add support for the relevant provider. in this case, SQL Server. 
                    sqlServer.AddCommand(types => types.ForType<CountryRepository>(             // start adding commands per repository type. 
                                methods => methods.ForMethod(nameof(CountryRepository.RetrieveAllAsync),  // reference the method that will execute the command. 
                                command => command // start building up the command
                                        .UseConnectionAlias("Example")                          // reference a connection string by an alias provided separately. 
                                        .UseCommandText("select * from [dbo].[country];")       // supply the SQL that you want to be executed.  
                                        )))));
        }
    }
}
```


### Support Runtimes and Vendors
Syrx is cross-platforn and currently supports up to .NET 8. 

Syrx currently supports the RDBMS vendors vendors below and this list is growing. Implementing support for a new ADO.NET provider is easily accomplished. 

|Vendor|Package|Extensions Package|
|--|--|--|
|SQL Server|[Syrx.SqlServer](https://www.nuget.org/packages/Syrx.SqlServer)|[Syrx.SqlServer.Extensions](https://www.nuget.org/packages/Syrx.SqlServer.Extensions)|
|MySql|[Syrx.MySql](https://www.nuget.org/packages/Syrx.MySql)|[Syrx.MySql.Extensions](https://www.nuget.org/packages/Syrx.MySql.Extensions)|
|PostgreSql|[Syrx.Npgsql](https://www.nuget.org/packages/Syrx.Npgsql)|[Syrx.Npgsql.Extensions](https://www.nuget.org/packages/Syrx.Npgsql.Extensions)|
|Oracle|[Syrx.Oracle](https://www.nuget.org/packages/Syrx.Oracle)|[Syrx.Oracle.Extensions](https://www.nuget.org/packages/Syrx.Oracle.Extensions)|

In fact, any vendor that has a DbProviderFactory can be supported. 

### Credits and Caveats
Syrx inspired by and in part based on [Dapper](https://github.com/StackExchange/Dapper).
Syrx will not generate SQL for you (although this feature may be added in the future). 
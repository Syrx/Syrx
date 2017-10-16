# Syrx

Syrx allows you to write the same repository code to target multiple different databases. 
It is a framework which decouples your .NET repository code from the underlying data store, allowing you to shield your 
repository code from the underlying storage provider. It's not quite an ORM although it uses an ORM to materialize your objects. 

Syrx places emphasis on:
* _Control_: You should be in control of your data and execution. 
* _Speed_: Performance is a feature. Syrx inherits its speed from Dapper.
* _Flexibilty_: Your choice of database technology should be as easy to change as any other component in your solution. 
* _Testability_: We believe strongly in the power of testing at all levels of your applications. Syrx is fully testable and fully tested.
* _Extensibility_: Syrx is granular and componentised, allowing you two swap out components for your own needs. 
* _Readability_: Syrx aims to keep the intent of your code clear and concise. 

## Quick Look
Syrx is predicated on the concept that all operations against a data store are fundamentally either a read or write operation.
The central construct in Syrx is the ICommander interface. This interface exposes overloads of only 2 methods, _Query_ and _Execute_ 
(as well as their async implementations _QueryAsync_ and _ExecuteAsync_. 


_Query_ methods are used to retrieve data.

_Execute_ methods are used to create/modify/delete data. 

#### Query
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

_Example_
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

### Configuration
Syrx offers a unique configuration structure in that it mimics the structure of your code. Configuration can be supplied 
either in any format that can be deserialized into an instance deriving from the Syrx.ISettings object. JSON is popular 
format for storing configuration data for Syrx but it's by no means the only format. 

Syrx uses the fully qualified type and method names from your code to resolve the command to be executed against the underlying
datastore. You can find more information on how to configure Syrx on our wiki. 


### Support Runtimes and Vendors
Syrx is written as a collection of .NET Standard 2.0 libraries so should run anywhere .NET standard 2.0 is supported. 

Syrx currently supports the RDBMS vendors vendors below and this list is growing. Implementing support for a new ADO.NET provider is easily accomplished. 
* SQL Server
* MySql
* PostgreSql



### Credits and Caveats
Syrx inspired by and in part based on [Dapper](https://github.com/StackExchange/Dapper).
Syrx will not generate SQL for you (although this feature may be added in the future). 

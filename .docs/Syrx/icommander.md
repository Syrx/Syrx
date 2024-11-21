# ICommander

The `ICommander<>` is the heart of Syrx. It is the interface through which operations against an underlying data store are executed. While the readme file at the root of the project provides an overview and quick look at the `ICommander<>` interface, this document will dig into more of the technical details.

In short, the `ICommander<>` provides read and write operations in both synchronous and asynchronous varieties.    
Read operations are implemented via `Query<>` methods. The `Query<>` methods allow you to compose object graphs of any complexity and of any depth.       
Write opertions are implemented via `Execute<>` methods. The `Execute` methods should be inherently transactional. 

There are several overloads for both. 

## Query
All overloads of `Query<>` return an `IEnumerable<T>`. 

The `Query<>` method has 32 overloads to support all overloads of the `Func<>` delegate. 
This allows you to compose any variety of object graph. 

In practice, most read operations will not need to leverage the more complex overloads as this would be a bit of a code smell. However, the option is there for those that might need it. 

As Syrx was originally inspired by - and continues to use - [Dapper](https://github.com/DapperLib/Dapper) the query method is further subdivided into the same "multimap" and "multiple" concepts used by Dapper. However, where Syrx differs is that it natively supports overloads for `Func<>` up to 16 generic type arguments (Dapper goes up to 7 natively). 


### Multimap
_Multimap_ allows you to compose a complex object graph using `Func<>` delegates from a single record. 

The mutlimap overloads all accept a `Func<>` delegate with a generic type (e.g. `Func<T1, T2, TResult>`). 

#### Example



### Multiple
_Multiple_ allows you to support multiple result sets. The overloads for multiple also allow you to compose complex object graphs using `Func<>` delegates. 

The multiple overloads all accept a `Func<>` delegate with an `IEnumerable<>` of generic type (e.g. `Func<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<TResult>`). 

#### Example

## Execute
Write operations have fewer overloads and should be implemented as implicitly transactional. For those of you from a functional background, the `Execute<>` method looks like a prime candidate for monadic treatment. And you'd be right. However, the current version of Syrx is opinionated enough without having to introduce monads as well. We might introduce monads in a future version. For now, though, we're keeping it really basic. 

The `Execute<>` method has only 3 overloads. Two of the overloads return `bool` with the final returning a `TResult`. 

The overloads and their usages are: 

`bool Execute<TModel>()`: Executes an arbitrary command against the underlying data store. This is useful where you are not expecting a result.    
`bool Execute<TModel>(T model)`: Executes a potentially state changing operation against the underlying data store (create/update/delete) using a domain data model.     
`TResult Execute<TResult>(Func<TResult> map, TransactionScopeOption scopeOption = TransactionScopeOption.Suppress)`: Executes any number of potentially state changing operations against an underlying data store. This is for more advanced persistence scenarios where the majority of the action happens within the `Func<TResult>` delegate. 


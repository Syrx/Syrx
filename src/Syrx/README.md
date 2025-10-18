# Syrx

Core interfaces and abstractions for the Syrx data access framework.

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Installation](#installation)
- [Core Interfaces](#core-interfaces)
  - [ICommander<T>](#icommandert)
  - [IConnector](#iconnector)
- [Usage](#usage)
  - [Basic Query Operations](#basic-query-operations)
  - [Execute Operations](#execute-operations)
  - [Multi-map Queries](#multi-map-queries)
- [Method Resolution](#method-resolution)
- [Repository Pattern](#repository-pattern)
- [Related Packages](#related-packages)
- [License](#license)
- [Credits](#credits)

## Overview

`Syrx` is the core package of the Syrx data access framework, providing the fundamental interfaces and abstractions that enable database-agnostic data access patterns. Built on top of Dapper, Syrx decouples repository code from underlying data stores while emphasizing control, speed, flexibility, testability, extensibility, and readability.

## Key Features

- **Database Agnostic**: Write repository code once, use with any supported database
- **High Performance**: Built on top of Dapper for optimal performance
- **Type Safety**: Strongly-typed interfaces and generic constraints
- **Async Support**: Full async/await support for all operations
- **Multi-map Queries**: Complex object composition with up to 16 input parameters
- **Method Resolution**: Automatic mapping from method names to SQL commands
- **Testability**: Fully mockable interfaces for unit testing

## Installation

```bash
dotnet add package Syrx
```

**Package Manager**
```bash
Install-Package Syrx
```

**PackageReference**
```xml
<PackageReference Include="Syrx" Version="2.4.3" />
```

> **Note**: This is the core package providing interfaces only. You'll also need a database-specific provider package such as `Syrx.SqlServer`, `Syrx.MySql`, `Syrx.Npgsql`, or `Syrx.Oracle`.

## Core Interfaces

### ICommander<T>

The central interface for all data access operations:

```csharp
public interface ICommander<T>
{
    // Query operations - retrieve data
    Task<IEnumerable<TResult>> QueryAsync<TResult>([CallerMemberName] string method = null);
    Task<IEnumerable<TResult>> QueryAsync<TResult>(object parameters, [CallerMemberName] string method = null);
    
    // Execute operations - mutate data
    Task<bool> ExecuteAsync([CallerMemberName] string method = null);
    Task<bool> ExecuteAsync(object parameters, [CallerMemberName] string method = null);
    
    // Multi-map queries - complex object composition
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(
        Func<TFirst, TSecond, TReturn> map,
        object parameters = null,
        [CallerMemberName] string method = null);
}
```

### IConnector

Base interface for database connectivity abstraction:

```csharp
public interface IConnector
{
    // Database connection abstraction
}
```

## Usage

### Basic Query Operations

```csharp
public class UserRepository
{
    private readonly ICommander<UserRepository> _commander;

    public UserRepository(ICommander<UserRepository> commander)
    {
        _commander = commander;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _commander.QueryAsync<User>();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var users = await _commander.QueryAsync<User>(new { id });
        return users.FirstOrDefault();
    }
}
```

### Execute Operations

```csharp
public async Task<User> CreateUserAsync(User user)
{
    var success = await _commander.ExecuteAsync(user);
    return success ? user : null;
}

public async Task<User> UpdateUserAsync(User user)
{
    return await _commander.ExecuteAsync(user) ? user : default;
}

public async Task<User> DeleteUserAsync(User user)
{
    return await _commander.ExecuteAsync(user) ? user : default;
}
```

### Multi-map Queries

For complex object composition:

```csharp
public async Task<IEnumerable<User>> GetUsersWithProfilesAsync()
{
    return await _commander.QueryAsync<User, Profile, User>(
        (user, profile) => 
        {
            user.Profile = profile;
            return user;
        });
}

public async Task<IEnumerable<Order>> GetOrdersWithItemsAsync()
{
    return await _commander.QueryAsync<Order, OrderItem, Product, Order>(
        (order, item, product) =>
        {
            item.Product = product;
            order.Items ??= new List<OrderItem>();
            order.Items.Add(item);
            return order;
        });
}
```

## Method Resolution

Syrx uses the `[CallerMemberName]` attribute to automatically resolve method names to SQL commands:

```csharp
public async Task<User> GetActiveUserByEmailAsync(string email)
{
    // Resolves to: Namespace.UserRepository.GetActiveUserByEmailAsync
    return (await _commander.QueryAsync<User>(new { email })).FirstOrDefault();
}
```

The command resolution follows the pattern:
```
{Namespace}.{ClassName}.{MethodName}
```

## Repository Pattern

Typical repository implementation using Syrx:

```csharp
public class ProductRepository
{
    private readonly ICommander<ProductRepository> _commander;

    public ProductRepository(ICommander<ProductRepository> commander)
    {
        _commander = commander;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _commander.QueryAsync<Product>(new { categoryId });
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        return await _commander.ExecuteAsync(product) ? product : null;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await _commander.QueryAsync<Product>(new { searchTerm });
    }
}
```

## Related Packages

### Database Providers
- **[Syrx.SqlServer](https://www.nuget.org/packages/Syrx.SqlServer/)**: SQL Server support
- **[Syrx.MySql](https://www.nuget.org/packages/Syrx.MySql/)**: MySQL support  
- **[Syrx.Npgsql](https://www.nuget.org/packages/Syrx.Npgsql/)**: PostgreSQL support
- **[Syrx.Oracle](https://www.nuget.org/packages/Syrx.Oracle/)**: Oracle support

### Extension Packages
- **[Syrx.SqlServer.Extensions](https://www.nuget.org/packages/Syrx.SqlServer.Extensions/)**: SQL Server extensions
- **[Syrx.MySql.Extensions](https://www.nuget.org/packages/Syrx.MySql.Extensions/)**: MySQL extensions
- **[Syrx.Npgsql.Extensions](https://www.nuget.org/packages/Syrx.Npgsql.Extensions/)**: PostgreSQL extensions
- **[Syrx.Oracle.Extensions](https://www.nuget.org/packages/Syrx.Oracle.Extensions/)**: Oracle extensions

### Core Framework
- **[Syrx.Commanders.Databases](https://www.nuget.org/packages/Syrx.Commanders.Databases/)**: Database command abstractions
- **[Syrx.Settings](https://www.nuget.org/packages/Syrx.Settings/)**: Configuration settings
- **[Syrx.Readers](https://www.nuget.org/packages/Syrx.Readers/)**: Configuration readers

## License

This project is licensed under the [MIT License](https://github.com/Syrx/Syrx/blob/main/LICENSE).

## Credits

Syrx is built on top of [Dapper](https://github.com/DapperLib/Dapper) and follows its performance-focused philosophy.
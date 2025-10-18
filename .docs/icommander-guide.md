# ICommander Interface Guide

This document provides comprehensive guidance on using the `ICommander<TRepository>` interface, which serves as the primary entry point for all data access operations in Syrx.

## Table of Contents

- [Overview](#overview)
- [Interface Design](#interface-design)
- [Query Operations](#query-operations)
- [Execute Operations](#execute-operations)
- [Multi-map Queries](#multi-map-queries)
- [Method Resolution](#method-resolution)
- [Error Handling](#error-handling)
- [Best Practices](#best-practices)
- [Advanced Usage](#advanced-usage)

## Overview

The `ICommander<TRepository>` interface is the central abstraction in Syrx that provides a clean, type-safe API for database operations. It automatically resolves method names to SQL commands, handles parameter mapping, and provides comprehensive support for both simple and complex query scenarios.

## Interface Design

### Core Principles

1. **Type Safety**: Generic constraints ensure compile-time type safety
2. **Method Resolution**: Automatic mapping from C# methods to SQL commands
3. **Async-First**: All operations support asynchronous execution
4. **Flexibility**: Support for simple queries to complex multi-mapping scenarios

### Generic Type Parameter

```csharp
public interface ICommander<TRepository> : IDisposable
{
    // Interface methods...
}
```

The `TRepository` type parameter serves multiple purposes:
- **Type Resolution**: Used to resolve the repository type in configuration
- **Method Context**: Combined with `[CallerMemberName]` for command resolution
- **Scoping**: Ensures commands are scoped to specific repository types

## Query Operations

Query operations are used for data retrieval (SELECT statements) and return collections of strongly-typed objects.

### Basic Query Syntax

```csharp
// Simple query without parameters
Task<IEnumerable<TResult>> QueryAsync<TResult>([CallerMemberName] string method = null);

// Query with parameters
Task<IEnumerable<TResult>> QueryAsync<TResult>(object parameters, [CallerMemberName] string method = null);
```

### Usage Examples

#### Simple Query
```csharp
public class UserRepository
{
    private readonly ICommander<UserRepository> _commander;

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // Resolves to: Namespace.UserRepository.GetAllUsersAsync
        return await _commander.QueryAsync<User>();
    }
}
```

#### Parameterized Query
```csharp
public async Task<IEnumerable<User>> GetUsersByStatusAsync(UserStatus status)
{
    return await _commander.QueryAsync<User>(new { status });
}

public async Task<User> GetUserByIdAsync(int id)
{
    var users = await _commander.QueryAsync<User>(new { id });
    return users.FirstOrDefault();
}
```

#### Complex Parameters
```csharp
public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm, int pageSize, int pageNumber)
{
    return await _commander.QueryAsync<User>(new 
    { 
        searchTerm, 
        pageSize, 
        pageNumber,
        offset = (pageNumber - 1) * pageSize 
    });
}
```

## Execute Operations

Execute operations are used for data modification (INSERT, UPDATE, DELETE) and return a boolean indicating success or failure.

### Basic Execute Syntax

```csharp
// Execute without parameters
Task<bool> ExecuteAsync([CallerMemberName] string method = null);

// Execute with model/parameters
Task<bool> ExecuteAsync<TResult>(TResult model, [CallerMemberName] string method = null);
```

### Usage Examples

#### Create Operations
```csharp
public async Task<User> CreateUserAsync(User user)
{
    return await _commander.ExecuteAsync(user) ? user : null;
}
```

#### Update Operations
```csharp
public async Task<User> UpdateUserAsync(User user)
{
    return await _commander.ExecuteAsync(user) ? user : null;
}
```

#### Delete Operations
```csharp
public async Task<User> DeleteUserAsync(User user)
{
    return await _commander.ExecuteAsync(user) ? user : null;
}

public async Task<User> DeleteUserByIdAsync(int userId)
{
    var user = new User { Id = userId }; // Or retrieve first if needed
    return await _commander.ExecuteAsync(user) ? user : null;
}
```

## Multi-map Queries

Multi-map queries enable complex object composition by mapping multiple result sets or joined data into strongly-typed objects.

### Supported Overloads

Syrx supports multi-map queries with up to 16 input types:

```csharp
// 2 types
Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(
    Func<T1, T2, TResult> map, 
    object parameters = null, 
    CancellationToken cancellationToken = default,
    [CallerMemberName] string method = null);

// 3 types
Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, TResult>(
    Func<T1, T2, T3, TResult> map, 
    object parameters = null, 
    CancellationToken cancellationToken = default,
    [CallerMemberName] string method = null);

// ... up to 16 types
```

### Usage Examples

#### Basic Multi-mapping
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
```

#### Complex Multi-mapping
```csharp
public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
{
    return await _commander.QueryAsync<Order, OrderItem, Product, Customer, Order>(
        (order, item, product, customer) =>
        {
            // Set up relationships
            item.Product = product;
            order.Customer = customer;
            
            // Handle collections
            order.Items ??= new List<OrderItem>();
            order.Items.Add(item);
            
            return order;
        });
}
```

#### Mapping with Conditional Logic
```csharp
public async Task<IEnumerable<User>> GetUsersWithOptionalDataAsync()
{
    return await _commander.QueryAsync<User, Address, PhoneNumber, User>(
        (user, address, phone) =>
        {
            // Handle null values from LEFT JOINs
            if (address?.Id > 0)
                user.Address = address;
                
            if (!string.IsNullOrEmpty(phone?.Number))
                user.PhoneNumber = phone;
                
            return user;
        });
}
```

## Method Resolution

Syrx uses the `[CallerMemberName]` attribute to automatically resolve C# method names to SQL commands.

### Resolution Strategy

1. **Automatic Resolution**: Method name is captured automatically
2. **Explicit Override**: Optional method parameter allows manual specification
3. **Hierarchical Lookup**: Namespace → Type → Method → Command

### Resolution Examples

```csharp
public class ProductRepository
{
    private readonly ICommander<ProductRepository> _commander;

    // Resolves to: "YourNamespace.ProductRepository.GetActiveProductsAsync"
    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _commander.QueryAsync<Product>();
    }

    // Resolves to: "YourNamespace.ProductRepository.GetProductsByCategory"
    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _commander.QueryAsync<Product>(new { categoryId });
    }

    // Manual override - resolves to: "YourNamespace.ProductRepository.GetAllProducts"
    public async Task<IEnumerable<Product>> GetAllActiveProductsAsync()
    {
        return await _commander.QueryAsync<Product>(method: "GetAllProducts");
    }
}
```

## Error Handling

### Exception Management

Syrx preserves the full exception stack trace from underlying database operations:

```csharp
public async Task<User> GetUserByIdAsync(int id)
{
    try
    {
        var users = await _commander.QueryAsync<User>(new { id });
        return users.FirstOrDefault();
    }
    catch (SqlException sqlEx)
    {
        // Handle SQL Server specific exceptions
        _logger.LogError(sqlEx, "Database error retrieving user {UserId}", id);
        throw;
    }
    catch (Exception ex)
    {
        // Handle general exceptions
        _logger.LogError(ex, "Error retrieving user {UserId}", id);
        throw;
    }
}
```

### Transaction Handling

```csharp
public async Task<User> CreateUserWithAuditAsync(User user)
{
    return await _commander.ExecuteAsync(() =>
    {
        // Both operations will be in the same transaction
        var userCreated = _commander.Execute(user);
        var auditLogged = _commander.Execute(new AuditLog 
        { 
            Action = "UserCreated", 
            UserId = user.Id 
        });
        
        return userCreated && auditLogged ? user : null;
    });
}
```

## Best Practices

### 1. Repository Design

```csharp
public class UserRepository : IUserRepository
{
    private readonly ICommander<UserRepository> _commander;

    public UserRepository(ICommander<UserRepository> commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
    }

    // Prefer specific, intention-revealing method names
    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _commander.QueryAsync<User>();
    }

    // Use parameters for all dynamic values
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var users = await _commander.QueryAsync<User>(new { email });
        return users.FirstOrDefault();
    }

    // Follow DDD pattern: return the same instance on success, null on failure
    public async Task<User> CreateUserAsync(User user)
    {
        return await _commander.ExecuteAsync(user) ? user : null;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        return await _commander.ExecuteAsync(user) ? user : null;
    }

    public async Task<User> DeleteUserAsync(User user)
    {
        return await _commander.ExecuteAsync(user) ? user : null;
    }
}
```

### 2. Parameter Handling

```csharp
// Good: Use anonymous objects for simple parameters
public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
{
    return await _commander.QueryAsync<Product>(new { categoryId });
}

// Good: Use strongly-typed objects for complex parameters
public async Task<IEnumerable<Order>> SearchOrdersAsync(OrderSearchCriteria criteria)
{
    return await _commander.QueryAsync<Order>(criteria);
}

// Avoid: String concatenation or manual SQL building
// This should be handled in the SQL configuration, not in C# code
```

### 3. Async/Await Usage

```csharp
// Good: Proper async/await usage
public async Task<IEnumerable<User>> GetUsersAsync()
{
    return await _commander.QueryAsync<User>();
}

// Good: ConfigureAwait(false) in library code
public async Task<IEnumerable<User>> GetUsersAsync()
{
    return await _commander.QueryAsync<User>().ConfigureAwait(false);
}
```

### 4. Cancellation Token Support

```csharp
public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default)
{
    return await _commander.QueryAsync<User>(cancellationToken: cancellationToken);
}
```

## Advanced Usage

### Custom Type Mapping

```csharp
public async Task<IEnumerable<UserSummary>> GetUserSummariesAsync()
{
    return await _commander.QueryAsync<User, Profile, Address, UserSummary>(
        (user, profile, address) => new UserSummary
        {
            Id = user.Id,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            ProfileCompleteness = CalculateCompleteness(profile),
            City = address?.City,
            State = address?.State
        });
}

private static int CalculateCompleteness(Profile profile)
{
    if (profile == null) return 0;
    
    var fields = new[] { profile.Bio, profile.Website, profile.Company };
    return (int)((double)fields.Count(f => !string.IsNullOrEmpty(f)) / fields.Length * 100);
}
```

### Dynamic Query Building

```csharp
public async Task<IEnumerable<Product>> SearchProductsAsync(ProductSearchCriteria criteria)
{
    // SQL should handle the dynamic aspects, not C# code
    // Pass the entire criteria object and let SQL do conditional logic
    return await _commander.QueryAsync<Product>(criteria);
}
```

### Performance Optimization

```csharp
public class CachedUserRepository : IUserRepository
{
    private readonly ICommander<UserRepository> _commander;
    private readonly IMemoryCache _cache;

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _cache.GetOrCreateAsync($"user:{id}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            var users = await _commander.QueryAsync<User>(new { id });
            return users.FirstOrDefault();
        });
    }
}
```

This comprehensive guide covers all aspects of using the `ICommander<TRepository>` interface effectively in your Syrx-based applications.
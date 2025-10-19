# GitHub Copilot Instructions for Syrx

This document provides guidance for GitHub Copilot and AI assistants working with the Syrx codebase. Follow these instructions to maintain consistency with the project's architecture, coding standards, and documentation practices.

## Project Overview

Syrx is a database-agnostic micro-ORM framework built on top of Dapper. It emphasizes:
- **Control**: Developers maintain full control over SQL and data execution
- **Speed**: High performance through Dapper's proven micro-ORM approach  
- **Flexibility**: Easy switching between database providers without code changes
- **Testability**: Clean abstractions that support comprehensive testing
- **Extensibility**: Modular design allowing custom components
- **Readability**: Clear, intention-revealing APIs and code structure

## Architecture Principles

### 1. Core Framework Design
- **Central Interface**: `ICommander<TRepository>` is the primary data access interface
- **Method Resolution**: Use `[CallerMemberName]` for automatic method-to-SQL mapping
- **Generic Type Safety**: Leverage generics for compile-time type safety
- **Separation of Concerns**: Clear boundaries between configuration, connection, and execution

### 2. Configuration Philosophy
- **Hierarchical Structure**: Mirror code structure (Namespace → Type → Method → Command)
- **External Configuration**: SQL commands are defined outside of C# code
- **Provider Abstraction**: Database-specific details are abstracted through providers
- **Fluent Builder Pattern**: Use builder patterns for configuration APIs

### 3. Extension Architecture  
- **Provider Pattern**: Each database provider implements core interfaces
- **Service Registration**: Extensions use dependency injection for service registration
- **Fluent APIs**: Provide fluent configuration APIs for each provider
- **Consistent Naming**: Follow established naming conventions for providers

## Code Style and Standards

### 1. Naming Conventions

#### Interfaces
```csharp
// Core framework interfaces
public interface ICommander<TRepository> : IDisposable { }
public interface IConnector<TConnection, TCommandSetting> { }
public interface ICommandReader<TCommandSetting> { }

// Settings interfaces  
public interface ISettings<TCommandSetting> { }
public interface ICommandSetting { }
```

#### Classes and Methods
```csharp
// Use descriptive, intention-revealing names
public class UserRepository : IUserRepository
{
    public async Task<User> GetUserByIdAsync(int id) { }
    public async Task<IEnumerable<User>> GetActiveUsersAsync() { }
}

// Extension methods follow pattern: Use{ProviderName}
public static SyrxBuilder UseSqlServer(this SyrxBuilder builder) { }
public static SyrxBuilder UseMySql(this SyrxBuilder builder) { }
```

#### Database Providers
```csharp
// Package naming: Syrx.{Provider}[.Extensions]
// Syrx.SqlServer, Syrx.SqlServer.Extensions
// Syrx.MySql, Syrx.MySql.Extensions  

// Command settings naming
public class SqlServerCommandSetting : ICommandSetting { }
public class MySqlCommandSetting : ICommandSetting { }
```

### 2. File Organization

#### Core Framework Structure
```
src/
├── Syrx/                          # Core interfaces and abstractions
│   ├── ICommander.cs              # Main interface declaration
│   ├── ICommander.Execute.cs      # Execute operations
│   ├── ICommander.ExecuteAsync.cs # Async execute operations
│   ├── ICommander.Multimap.cs     # Multi-mapping queries
│   ├── ICommander.MultimapAsync.cs# Async multi-mapping
│   └── ICommander.Multiple.cs     # Multiple result sets
├── Syrx.Settings/                 # Configuration abstractions
├── Syrx.Connectors/               # Connection abstractions
├── Syrx.Readers/                  # Command reading abstractions
└── Syrx.Extensions/               # DI and service registration
```

#### Documentation Structure
```
.docs/
├── architecture.md                # Framework architecture
├── quick-start.md                 # Getting started guide
├── icommander-guide.md           # ICommander interface guide
├── configuration-guide.md         # Configuration documentation
└── Syrx/
    └── readme.md                  # Main package documentation
```

### 3. XML Documentation Standards

#### Interface Documentation
```csharp
/// <summary>
/// Provides comprehensive data access operations for repository pattern implementations.
/// This interface serves as the primary abstraction for all database operations in Syrx.
/// </summary>
/// <typeparam name="TRepository">
/// The repository type used for command resolution and type safety.
/// This parameter enables method-to-SQL mapping and scoped configuration.
/// </typeparam>
/// <remarks>
/// The ICommander interface automatically resolves method names to SQL commands
/// using the CallerMemberName attribute and hierarchical configuration lookup.
/// </remarks>
public partial interface ICommander<TRepository> : IDisposable
{
    /// <summary>
    /// Executes a query operation against the data source and returns strongly-typed results.
    /// </summary>
    /// <typeparam name="TResult">The type to map query results to.</typeparam>
    /// <param name="parameters">Optional parameters to pass to the query. Can be anonymous object or typed object.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <param name="method">
    /// The method name used for command resolution. Automatically populated via CallerMemberName.
    /// Can be overridden for custom command mapping.
    /// </param>
    /// <returns>A task representing the asynchronous operation that returns a collection of TResult.</returns>
    /// <exception cref="ArgumentException">Thrown when command configuration is not found.</exception>
    /// <exception cref="SqlException">Thrown when database operation fails.</exception>
    Task<IEnumerable<TResult>> QueryAsync<TResult>(
        object parameters = null,
        CancellationToken cancellationToken = default,
        [CallerMemberName] string method = null);
}
```

#### Class Documentation
```csharp
/// <summary>
/// Provides a fluent builder interface for configuring Syrx services and their dependencies
/// within a dependency injection container. This class serves as the entry point for
/// registering database providers, command settings, and other Syrx-related services.
/// </summary>
/// <remarks>
/// The SyrxBuilder is typically used within the UseSyrx() extension method to configure
/// the framework. It encapsulates the service collection and provides extension points
/// for database-specific configuration through provider-specific builder extensions.
/// </remarks>
/// <example>
/// <code>
/// services.UseSyrx(builder => 
/// {
///     builder.UseSqlServer(sqlServer => 
///     {
///         sqlServer.AddConnectionString("Default", connectionString);
///         sqlServer.AddCommand(/* configuration */);
///     });
/// });
/// </code>
/// </example>
public class SyrxBuilder
```

## Implementation Patterns

### 1. Repository Pattern Implementation

```csharp
public class UserRepository : IUserRepository
{
    private readonly ICommander<UserRepository> _commander;

    public UserRepository(ICommander<UserRepository> commander)
    {
        _commander = commander ?? throw new ArgumentNullException(nameof(commander));
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

    public async Task<User> CreateUserAsync(User user)
    {
        var success = await _commander.ExecuteAsync(user);
        return success ? user : null;
    }
}
```

### 2. Multi-mapping Pattern

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

public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
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

### 3. Configuration Pattern

```csharp
services.UseSyrx(builder =>
{
    builder.UseSqlServer(sqlServer =>
    {
        // Connection strings
        sqlServer.AddConnectionString("Default", connectionString);
        sqlServer.AddConnectionString("ReadOnly", readOnlyConnectionString);
        
        // Command configuration
        sqlServer.AddCommand(types => types
            .ForType<UserRepository>(methods =>
            {
                methods.ForMethod(nameof(UserRepository.GetAllUsersAsync), cmd => cmd
                    .UseConnectionAlias("ReadOnly")
                    .UseCommandText("SELECT * FROM Users WHERE IsActive = 1"));
                    
                methods.ForMethod(nameof(UserRepository.CreateUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("INSERT INTO Users..."));
            }));
    });
});
```

### 4. Database Provider Extension Pattern

```csharp
public static class SqlServerExtensions
{
    public static SyrxBuilder UseSqlServer(this SyrxBuilder builder, 
        Action<SqlServerBuilder> configure)
    {
        // Register core services
        builder.ServiceCollection.AddTransient<IConnector<IDbConnection, SqlServerCommandSetting>, 
            SqlServerConnector>();
        builder.ServiceCollection.AddTransient<ICommandReader<SqlServerCommandSetting>, 
            SqlServerCommandReader>();
        
        // Configure provider-specific services
        var sqlServerBuilder = new SqlServerBuilder(builder.ServiceCollection);
        configure(sqlServerBuilder);
        
        return builder;
    }
}
```

## Error Handling Patterns

### 1. Repository Error Handling
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
        _logger.LogError(sqlEx, "Database error retrieving user {UserId}", id);
        throw; // Preserve stack trace
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Unexpected error retrieving user {UserId}", id);
        throw;
    }
}
```

### 2. Configuration Validation
```csharp
public void ValidateConfiguration(IServiceProvider services)
{
    var readers = services.GetServices<ICommandReader>();
    foreach (var reader in readers)
    {
        // Validate required commands are configured
        ValidateRepositoryCommands(reader);
    }
}
```

## Testing Patterns

### 1. Repository Testing
```csharp
[Test]
public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
{
    // Arrange
    var mockCommander = new Mock<ICommander<UserRepository>>();
    var expectedUser = new User { Id = 1, FirstName = "John" };
    
    mockCommander
        .Setup(x => x.QueryAsync<User>(It.IsAny<object>(), default, "GetUserByIdAsync"))
        .ReturnsAsync(new[] { expectedUser });
    
    var repository = new UserRepository(mockCommander.Object);
    
    // Act
    var result = await repository.GetUserByIdAsync(1);
    
    // Assert
    Assert.That(result, Is.EqualTo(expectedUser));
}
```

### 2. Configuration Testing
```csharp
[Test]
public void UseSqlServer_RegistersRequiredServices()
{
    // Arrange
    var services = new ServiceCollection();
    var builder = new SyrxBuilder(services);
    
    // Act
    builder.UseSqlServer(sql => { });
    
    // Assert
    Assert.That(services.Any(s => s.ServiceType == typeof(IConnector<IDbConnection, SqlServerCommandSetting>)));
}
```

## Documentation Guidelines

### 1. README Structure for Projects
```markdown
# Project Name

Brief description of the package purpose.

## Installation
## Key Features  
## Usage Examples
### Basic Usage
### Advanced Usage
## Related Packages
## License
## Credits
```

### 2. Technical Documentation
- **Architecture diagrams**: Use Mermaid diagrams for complex relationships
- **Code examples**: Always include complete, runnable examples
- **Configuration samples**: Show both simple and complex scenarios
- **Error scenarios**: Document common errors and solutions

### 3. API Documentation
- **Comprehensive XML docs**: All public APIs must have complete documentation
- **Parameter descriptions**: Explain purpose, constraints, and examples
- **Return value documentation**: Describe what is returned and when
- **Exception documentation**: List possible exceptions and causes

## Common Anti-patterns to Avoid

### 1. SQL in Repository Code
```csharp
// ❌ Don't do this - SQL should be in configuration
public async Task<User> GetUserByIdAsync(int id)
{
    return await _commander.QueryAsync<User>("SELECT * FROM Users WHERE Id = @id", new { id });
}

// ✅ Do this - let configuration handle SQL
public async Task<User> GetUserByIdAsync(int id)
{
    var users = await _commander.QueryAsync<User>(new { id });
    return users.FirstOrDefault();
}
```

### 2. Hard-coded Connection Strings
```csharp
// ❌ Don't do this
sqlServer.AddConnectionString("Default", "Server=localhost;Database=MyDb;...");

// ✅ Do this
sqlServer.AddConnectionString("Default", configuration.GetConnectionString("DefaultConnection"));
```

### 3. Synchronous Database Operations
```csharp
// ❌ Avoid synchronous operations
public User GetUserById(int id)
{
    return _commander.Query<User>(new { id }).FirstOrDefault();
}

// ✅ Prefer async operations
public async Task<User> GetUserByIdAsync(int id)
{
    var users = await _commander.QueryAsync<User>(new { id });
    return users.FirstOrDefault();
}
```

## Contribution Guidelines

### 1. Code Changes
- Follow established patterns and naming conventions
- Add comprehensive XML documentation
- Include unit tests for new functionality
- Update relevant documentation files

### 2. New Features
- Discuss architecture implications before implementation
- Ensure database provider consistency
- Add configuration examples
- Update quick start guide if applicable

### 3. Bug Fixes
- Include reproduction steps in tests
- Update documentation if behavior changes
- Consider impact on all database providers

## Version and Compatibility

- **Target Framework**: .NET 8.0+
- **Database Support**: SQL Server, MySQL, PostgreSQL, Oracle
- **Dependencies**: Minimal dependencies (primarily Dapper and Microsoft.Extensions.*)
- **Breaking Changes**: Follow semantic versioning principles

Follow these guidelines to maintain consistency and quality across the Syrx framework.

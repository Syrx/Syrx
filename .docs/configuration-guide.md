# Configuration Guide

This document provides comprehensive guidance on configuring Syrx for various database providers and scenarios.

## Table of Contents

- [Overview](#overview)
- [Configuration Hierarchy](#configuration-hierarchy)
- [Basic Configuration](#basic-configuration)
- [Database Provider Configuration](#database-provider-configuration)
- [Advanced Configuration](#advanced-configuration)
- [Connection Management](#connection-management)
- [Command Configuration](#command-configuration)
- [Environment-Specific Configuration](#environment-specific-configuration)
- [Best Practices](#best-practices)
- [Troubleshooting](#troubleshooting)

## Overview

Syrx uses a hierarchical configuration system that mirrors your code structure. This approach provides several benefits:

- **Intuitive Organization**: Configuration follows your namespace and class structure
- **Flexible Resolution**: Commands can be specified at different levels of granularity
- **Override Capability**: More specific configurations override general ones
- **Type Safety**: Strongly-typed configuration prevents runtime errors

## Configuration Hierarchy

The Syrx configuration follows this hierarchy:

```
ISettings<TCommandSetting>
├── Namespaces[]
    ├── Name: "Your.Application.Namespace"
    ├── Types[]
        ├── Name: "UserRepository" 
        ├── Commands[]
            ├── Key: "GetUserByIdAsync"
            ├── CommandText: "SELECT * FROM Users WHERE Id = @id"
            ├── ConnectionString: "..."
            └── Other settings...
```

### Configuration Resolution Order

1. **Method-specific**: Exact namespace.type.method match
2. **Type-level defaults**: Fallback to type-level configuration
3. **Namespace defaults**: Further fallback to namespace-level settings
4. **Global defaults**: System-wide default settings

## Basic Configuration

### Dependency Injection Setup

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.UseSyrx(builder =>
    {
        builder.UseSqlServer(sqlServer =>
        {
            sqlServer.AddConnectionString("Default", connectionString);
            
            sqlServer.AddCommand(types => types
                .ForType<UserRepository>(methods => methods
                    .ForMethod(nameof(UserRepository.GetUserByIdAsync), command => command
                        .UseConnectionAlias("Default")
                        .UseCommandText("SELECT * FROM Users WHERE Id = @id")
                        .UseCommandTimeout(30))));
        });
    });
    
    // Register your repositories
    services.AddScoped<IUserRepository, UserRepository>();
}
```

### Configuration Builder Pattern

```csharp
services.UseSyrx(builder =>
{
    // Configure database provider
    builder.UseSqlServer(sqlServer =>
    {
        // Add connection strings
        sqlServer.AddConnectionString("Primary", primaryConnectionString);
        sqlServer.AddConnectionString("ReadOnly", readOnlyConnectionString);
        
        // Configure commands for UserRepository
        sqlServer.AddCommand(types => types.ForType<UserRepository>(methods =>
        {
            methods.ForMethod("GetAllUsersAsync", cmd => cmd
                .UseConnectionAlias("ReadOnly")
                .UseCommandText("SELECT * FROM Users WHERE IsActive = 1"));
                
            methods.ForMethod("CreateUserAsync", cmd => cmd
                .UseConnectionAlias("Primary")
                .UseCommandText(@"
                    INSERT INTO Users (FirstName, LastName, Email, CreatedDate)
                    VALUES (@FirstName, @LastName, @Email, @CreatedDate)"));
                    
            methods.ForMethod("UpdateUserAsync", cmd => cmd
                .UseConnectionAlias("Primary")
                .UseCommandText(@"
                    UPDATE Users 
                    SET FirstName = @FirstName, 
                        LastName = @LastName, 
                        Email = @Email,
                        ModifiedDate = @ModifiedDate
                    WHERE Id = @Id"));
        }));
    });
});
```

## Database Provider Configuration

### SQL Server Configuration

```csharp
builder.UseSqlServer(sqlServer =>
{
    // Connection string configuration
    sqlServer.AddConnectionString("Default", connectionString);
    
    // Or connection string with options
    sqlServer.AddConnectionString("Default", connectionString, options =>
    {
        options.CommandTimeout = 60;
        options.MultipleActiveResultSets = true;
    });
    
    // Command configuration
    sqlServer.AddCommand(types => types
        .ForType<ProductRepository>(methods => methods
            .ForMethod("GetProductsByCategoryAsync", cmd => cmd
                .UseConnectionAlias("Default")
                .UseCommandText(@"
                    SELECT p.*, c.Name as CategoryName 
                    FROM Products p 
                    INNER JOIN Categories c ON p.CategoryId = c.Id 
                    WHERE p.CategoryId = @categoryId")
                .UseCommandType(CommandType.Text)
                .UseCommandTimeout(30))));
});
```

### MySQL Configuration

```csharp
builder.UseMySql(mysql =>
{
    mysql.AddConnectionString("Default", connectionString);
    
    mysql.AddCommand(types => types
        .ForType<OrderRepository>(methods => methods
            .ForMethod("GetOrdersByDateRangeAsync", cmd => cmd
                .UseConnectionAlias("Default")
                .UseCommandText(@"
                    SELECT o.*, c.FirstName, c.LastName
                    FROM orders o
                    INNER JOIN customers c ON o.customer_id = c.id
                    WHERE o.order_date BETWEEN @startDate AND @endDate"))));
});
```

### PostgreSQL Configuration

```csharp
builder.UseNpgsql(postgres =>
{
    postgres.AddConnectionString("Default", connectionString);
    
    postgres.AddCommand(types => types
        .ForType<InventoryRepository>(methods => methods
            .ForMethod("GetLowStockItemsAsync", cmd => cmd
                .UseConnectionAlias("Default")
                .UseCommandText(@"
                    SELECT * FROM inventory 
                    WHERE quantity <= minimum_stock_level
                    ORDER BY quantity ASC"))));
});
```

## Advanced Configuration

### Multiple Database Support

```csharp
services.UseSyrx(builder =>
{
    // SQL Server for transactional data
    builder.UseSqlServer(sqlServer =>
    {
        sqlServer.AddConnectionString("Transactional", transactionalConnectionString);
        
        sqlServer.AddCommand(types => types
            .ForType<UserRepository>(methods => methods
                .ForMethod("CreateUserAsync", cmd => cmd
                    .UseConnectionAlias("Transactional")
                    .UseCommandText("INSERT INTO Users..."))));
    });
    
    // PostgreSQL for analytics
    builder.UseNpgsql(postgres =>
    {
        postgres.AddConnectionString("Analytics", analyticsConnectionString);
        
        postgres.AddCommand(types => types
            .ForType<ReportsRepository>(methods => methods
                .ForMethod("GetUserAnalyticsAsync", cmd => cmd
                    .UseConnectionAlias("Analytics")
                    .UseCommandText("SELECT ... FROM user_analytics..."))));
    });
});
```

### Stored Procedure Configuration

```csharp
sqlServer.AddCommand(types => types
    .ForType<ReportsRepository>(methods => methods
        .ForMethod("GenerateMonthlyReportAsync", cmd => cmd
            .UseConnectionAlias("Default")
            .UseCommandText("sp_GenerateMonthlyReport")
            .UseCommandType(CommandType.StoredProcedure)
            .UseCommandTimeout(300))));
```

### Complex Query Configuration

```csharp
sqlServer.AddCommand(types => types
    .ForType<OrderRepository>(methods => methods
        .ForMethod("GetOrdersWithDetailsAsync", cmd => cmd
            .UseConnectionAlias("Default")
            .UseCommandText(@"
                SELECT 
                    o.Id, o.OrderDate, o.TotalAmount,
                    oi.Id as ItemId, oi.Quantity, oi.UnitPrice,
                    p.Id as ProductId, p.Name as ProductName, p.Description,
                    c.Id as CustomerId, c.FirstName, c.LastName, c.Email
                FROM Orders o
                INNER JOIN OrderItems oi ON o.Id = oi.OrderId
                INNER JOIN Products p ON oi.ProductId = p.Id
                INNER JOIN Customers c ON o.CustomerId = c.Id
                WHERE o.OrderDate >= @startDate
                ORDER BY o.OrderDate DESC, o.Id, oi.Id"))));
```

## Connection Management

### Connection String Configuration

```csharp
// Direct connection string
sqlServer.AddConnectionString("Default", "Server=localhost;Database=MyApp;Trusted_Connection=true;");

// From configuration
sqlServer.AddConnectionString("Default", Configuration.GetConnectionString("DefaultConnection"));

// With connection options
sqlServer.AddConnectionString("Default", connectionString, options =>
{
    options.CommandTimeout = 60;
    options.ConnectionTimeout = 30;
});
```

### Connection Aliasing

```csharp
// Define multiple connection aliases
sqlServer.AddConnectionString("Primary", primaryConnectionString);
sqlServer.AddConnectionString("ReadOnly", readOnlyConnectionString);
sqlServer.AddConnectionString("Analytics", analyticsConnectionString);

// Use specific connections for different operations
sqlServer.AddCommand(types => types
    .ForType<UserRepository>(methods =>
    {
        // Writes go to primary
        methods.ForMethod("CreateUserAsync", cmd => cmd.UseConnectionAlias("Primary"));
        methods.ForMethod("UpdateUserAsync", cmd => cmd.UseConnectionAlias("Primary"));
        methods.ForMethod("DeleteUserAsync", cmd => cmd.UseConnectionAlias("Primary"));
        
        // Reads can use read-only replica
        methods.ForMethod("GetUserByIdAsync", cmd => cmd.UseConnectionAlias("ReadOnly"));
        methods.ForMethod("GetAllUsersAsync", cmd => cmd.UseConnectionAlias("ReadOnly"));
        
        // Analytics queries use dedicated connection
        methods.ForMethod("GetUserStatisticsAsync", cmd => cmd.UseConnectionAlias("Analytics"));
    }));
```

## Command Configuration

### Basic Command Settings

```csharp
.ForMethod("MethodName", cmd => cmd
    .UseConnectionAlias("Default")           // Connection to use
    .UseCommandText("SELECT * FROM Users")   // SQL command
    .UseCommandType(CommandType.Text)        // Command type
    .UseCommandTimeout(30))                  // Timeout in seconds
```

### Command Types

```csharp
// Text commands (default)
.UseCommandType(CommandType.Text)
.UseCommandText("SELECT * FROM Users WHERE Id = @id")

// Stored procedures
.UseCommandType(CommandType.StoredProcedure)
.UseCommandText("sp_GetUserById")

// Table direct (rarely used)
.UseCommandType(CommandType.TableDirect)
.UseCommandText("Users")
```

### Parameter Handling

```csharp
// Parameters are automatically mapped from method parameters or anonymous objects
public async Task<User> GetUserByIdAsync(int id)
{
    // The @id parameter in SQL maps to the 'id' method parameter
    return (await _commander.QueryAsync<User>(new { id })).FirstOrDefault();
}

// Complex parameter objects
public async Task<IEnumerable<User>> SearchUsersAsync(UserSearchCriteria criteria)
{
    // All properties of criteria become SQL parameters
    return await _commander.QueryAsync<User>(criteria);
}
```

## Environment-Specific Configuration

### Using Configuration Sources

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    
    services.UseSyrx(builder =>
    {
        builder.UseSqlServer(sqlServer =>
        {
            // Environment-specific connection strings
            var connectionString = environment switch
            {
                "Development" => Configuration.GetConnectionString("Development"),
                "Staging" => Configuration.GetConnectionString("Staging"),
                "Production" => Configuration.GetConnectionString("Production"),
                _ => Configuration.GetConnectionString("Default")
            };
            
            sqlServer.AddConnectionString("Default", connectionString);
            
            // Environment-specific timeouts
            var commandTimeout = environment == "Development" ? 300 : 30;
            
            sqlServer.AddCommand(types => types
                .ForType<UserRepository>(methods => methods
                    .ForMethod("GetAllUsersAsync", cmd => cmd
                        .UseConnectionAlias("Default")
                        .UseCommandText("SELECT * FROM Users")
                        .UseCommandTimeout(commandTimeout))));
        });
    });
}
```

## Best Practices

### 1. Organization and Structure

```csharp
// Group related configurations together
services.UseSyrx(builder =>
{
    builder.UseSqlServer(sqlServer =>
    {
        // Connection strings first
        ConfigureConnectionStrings(sqlServer);
        
        // Then commands organized by namespace/repository
        ConfigureUserCommands(sqlServer);
        ConfigureOrderCommands(sqlServer);
        ConfigureProductCommands(sqlServer);
    });
});

private static void ConfigureUserCommands(SqlServerBuilder sqlServer)
{
    sqlServer.AddCommand(types => types
        .ForNamespace("MyApp.Repositories", ns => ns
            .ForType<UserRepository>(methods =>
            {
                methods.ForMethod("GetUserByIdAsync", cmd => cmd
                    .UseConnectionAlias("ReadOnly")
                    .UseCommandText("SELECT * FROM Users WHERE Id = @id"));
                    
                methods.ForMethod("CreateUserAsync", cmd => cmd
                    .UseConnectionAlias("Primary")
                    .UseCommandText("INSERT INTO Users..."));
            })));
}
```

### 2. SQL Management

```csharp
// Use embedded resources for complex SQL
public static class SqlQueries
{
    public static readonly string GetUserWithProfile = @"
        SELECT 
            u.Id, u.FirstName, u.LastName, u.Email,
            p.Id as ProfileId, p.Bio, p.Website, p.Company
        FROM Users u
        LEFT JOIN UserProfiles p ON u.Id = p.UserId
        WHERE u.Id = @id";
        
    public static readonly string CreateUserWithProfile = @"
        BEGIN TRANSACTION;
        
        INSERT INTO Users (FirstName, LastName, Email, CreatedDate)
        VALUES (@FirstName, @LastName, @Email, @CreatedDate);
        
        DECLARE @UserId INT = SCOPE_IDENTITY();
        
        INSERT INTO UserProfiles (UserId, Bio, Website, Company)
        VALUES (@UserId, @Bio, @Website, @Company);
        
        COMMIT TRANSACTION;";
}

// Use in configuration
methods.ForMethod("GetUserWithProfileAsync", cmd => cmd
    .UseConnectionAlias("Default")
    .UseCommandText(SqlQueries.GetUserWithProfile));
```

### 3. Connection String Security

```csharp
// Use Azure Key Vault or similar for production
services.UseSyrx(builder =>
{
    builder.UseSqlServer(sqlServer =>
    {
        if (Environment.IsDevelopment())
        {
            sqlServer.AddConnectionString("Default", 
                Configuration.GetConnectionString("DefaultConnection"));
        }
        else
        {
            // Production: Get from secure storage
            var connectionString = await keyVaultClient.GetSecretAsync("DatabaseConnectionString");
            sqlServer.AddConnectionString("Default", connectionString.Value);
        }
    });
});
```

### 4. Performance Configuration

```csharp
sqlServer.AddCommand(types => types
    .ForType<ReportsRepository>(methods =>
    {
        // Long-running reports get extended timeout
        methods.ForMethod("GenerateAnnualReportAsync", cmd => cmd
            .UseConnectionAlias("Analytics")
            .UseCommandText("sp_GenerateAnnualReport @year")
            .UseCommandTimeout(600)); // 10 minutes
            
        // Quick lookups get short timeout for fail-fast behavior
        methods.ForMethod("GetDashboardSummaryAsync", cmd => cmd
            .UseConnectionAlias("ReadOnly")
            .UseCommandText("SELECT COUNT(*) FROM Users")
            .UseCommandTimeout(5)); // 5 seconds
    }));
```

## Troubleshooting

### Common Configuration Issues

#### 1. Method Not Found
```
Error: No command configuration found for method 'GetUserByIdAsync'
```

**Solution**: Ensure method name matches exactly, including casing:
```csharp
// Wrong
methods.ForMethod("getUserByIdAsync", ...)

// Correct
methods.ForMethod("GetUserByIdAsync", ...)
// Or use nameof for safety
methods.ForMethod(nameof(UserRepository.GetUserByIdAsync), ...)
```

#### 2. Connection String Not Found
```
Error: Connection string 'Default' not found
```

**Solution**: Ensure connection alias matches:
```csharp
// Define the connection
sqlServer.AddConnectionString("Default", connectionString);

// Use the same alias
.UseConnectionAlias("Default")
```

#### 3. SQL Parameter Mismatch
```
Error: Must declare the scalar variable "@userId"
```

**Solution**: Ensure parameter names match between SQL and C# object:
```csharp
// SQL: WHERE Id = @userId
// C# parameter object:
new { userId = 123 }  // Property name must match @userId in SQL
```

### Debugging Configuration

```csharp
// Enable logging to see configuration resolution
services.AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));

// Add custom configuration validator
services.AddTransient<IConfigurationValidator, CustomConfigurationValidator>();
```

### Configuration Validation

```csharp
public class ConfigurationValidator
{
    public void ValidateAtStartup(IServiceProvider services)
    {
        // Get all registered repositories
        var repositories = services.GetServices<ICommandReader>();
        
        foreach (var repo in repositories)
        {
            // Validate that all required commands are configured
            ValidateRepositoryConfiguration(repo);
        }
    }
}
```

This comprehensive guide covers all aspects of configuring Syrx for various scenarios and database providers.
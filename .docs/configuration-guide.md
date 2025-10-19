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
- [Configuration from External Sources](#configuration-from-external-sources)
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

## Configuration from External Sources

Syrx supports loading configuration from external files (JSON and XML) using the Settings Extensions packages. This approach is ideal for applications that need to separate configuration from code or support environment-specific settings.

### Required Packages

For external configuration, install the appropriate extensions alongside your core Syrx packages:

>**Note**    
These packages are automatically installed with provider specific implementations. For instance, for SQL Server **[SyrxSqlServer.Extensions](https://www.nuget.org/packages/Syrx.SqlServer.Extensions/)**.


```xml
<!-- For JSON configuration files -->
<PackageReference Include="Syrx.Commanders.Databases.Settings.Extensions.Json" Version="3.0.0" />

<!-- For XML configuration files -->
<PackageReference Include="Syrx.Commanders.Databases.Settings.Extensions.Xml" Version="3.0.0" />

<!-- For programmatic builder pattern -->
<PackageReference Include="Syrx.Commanders.Databases.Settings.Extensions" Version="3.0.0" />
```

### JSON Configuration

#### Service Registration with JSON Files

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Syrx.Extensions;
using Syrx.Commanders.Databases.Settings.Extensions.Json;

public void ConfigureServices(IServiceCollection services)
{
    var configBuilder = new ConfigurationBuilder();
    
    // Single JSON file
    services.UseSyrx(builder => builder
        .UseFile("syrx.json", configBuilder));
    
    // Multiple JSON files with environment overrides
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    services.UseSyrx(builder => {
        var syrxBuilder = builder.UseFile("syrx.json", configBuilder);
        
        // Environment-specific override file
        var environmentFile = $"syrx.{environment}.json";
        if (File.Exists(environmentFile))
        {
            syrxBuilder.UseFile(environmentFile, configBuilder);
        }
        
        return syrxBuilder;
    });
}
```

#### JSON Configuration File Structure (`syrx.json`)

```json
{
  "Connections": [
    {
      "Alias": "DefaultConnection",
      "ConnectionString": "Server=localhost;Database=MyApp;Trusted_Connection=true;"
    },
    {
      "Alias": "ReadOnlyConnection",
      "ConnectionString": "Server=readonly;Database=MyApp;Trusted_Connection=true;"
    }
  ],
  "Namespaces": [
    {
      "Name": "MyApp.Repositories",
      "Types": [
        {
          "Name": "UserRepository",
          "Commands": {
            "GetAllUsersAsync": {
              "CommandText": "SELECT * FROM Users WHERE IsActive = 1",
              "ConnectionAlias": "ReadOnlyConnection",
              "CommandTimeout": 30
            },
            "GetUserByIdAsync": {
              "CommandText": "SELECT * FROM Users WHERE Id = @id",
              "ConnectionAlias": "ReadOnlyConnection",
              "CommandTimeout": 30
            },
            "CreateUserAsync": {
              "CommandText": "INSERT INTO Users (Name, Email, CreatedDate) VALUES (@Name, @Email, @CreatedDate)",
              "ConnectionAlias": "DefaultConnection",
              "CommandTimeout": 60
            },
            "GetUsersWithProfilesAsync": {
              "CommandText": "SELECT u.*, p.* FROM Users u JOIN UserProfiles p ON u.Id = p.UserId WHERE u.IsActive = 1",
              "ConnectionAlias": "ReadOnlyConnection",
              "SplitOn": "Id",
              "CommandTimeout": 45
            }
          }
        },
        {
          "Name": "ProductRepository", 
          "Commands": {
            "GetProductsAsync": {
              "CommandText": "SELECT * FROM Products WHERE IsActive = 1",
              "ConnectionAlias": "ReadOnlyConnection"
            },
            "ProcessInventoryAsync": {
              "CommandText": "sp_ProcessInventory",
              "ConnectionAlias": "DefaultConnection",
              "CommandType": "StoredProcedure",
              "CommandTimeout": 300,
              "IsolationLevel": "Serializable"
            }
          }
        }
      ]
    }
  ]
}
```

### XML Configuration

#### Service Registration with XML Files

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Syrx.Extensions;
using Syrx.Commanders.Databases.Settings.Extensions.Xml;

public void ConfigureServices(IServiceCollection services)
{
    var configBuilder = new ConfigurationBuilder();
    
    // Single XML file
    services.UseSyrx(builder => builder
        .UseFile("syrx.xml", configBuilder));
    
    // Multiple XML files with environment overrides
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    services.UseSyrx(builder => {
        var syrxBuilder = builder.UseFile("syrx.xml", configBuilder);
        
        // Environment-specific override file
        var environmentFile = $"syrx.{environment}.xml";
        if (File.Exists(environmentFile))
        {
            syrxBuilder.UseFile(environmentFile, configBuilder);
        }
        
        return syrxBuilder;
    });
}
```

#### XML Configuration File Structure (`syrx.xml`)

```xml
<?xml version="1.0" encoding="utf-8"?>
<CommanderSettings xmlns="http://schemas.syrx.dev/commander-settings">
  <Connections>
    <ConnectionStringSetting>
      <Alias>DefaultConnection</Alias>
      <ConnectionString>Server=localhost;Database=MyApp;Trusted_Connection=true;</ConnectionString>
    </ConnectionStringSetting>
    <ConnectionStringSetting>
      <Alias>ReadOnlyConnection</Alias>
      <ConnectionString>Server=readonly;Database=MyApp;Trusted_Connection=true;</ConnectionString>
    </ConnectionStringSetting>
  </Connections>
  <Namespaces>
    <NamespaceSetting>
      <Name>MyApp.Repositories</Name>
      <Types>
        <TypeSetting>
          <Name>UserRepository</Name>
          <Commands>
            <Command Key="GetAllUsersAsync">
              <CommandText>SELECT * FROM Users WHERE IsActive = 1</CommandText>
              <ConnectionAlias>ReadOnlyConnection</ConnectionAlias>
              <CommandTimeout>30</CommandTimeout>
            </Command>
            <Command Key="GetUserByIdAsync">
              <CommandText>SELECT * FROM Users WHERE Id = @id</CommandText>
              <ConnectionAlias>ReadOnlyConnection</ConnectionAlias>
              <CommandTimeout>30</CommandTimeout>
            </Command>
            <Command Key="CreateUserAsync">
              <CommandText>INSERT INTO Users (Name, Email, CreatedDate) VALUES (@Name, @Email, @CreatedDate)</CommandText>
              <ConnectionAlias>DefaultConnection</ConnectionAlias>
              <CommandTimeout>60</CommandTimeout>
            </Command>
            <Command Key="GetUsersWithProfilesAsync">
              <CommandText><![CDATA[
                SELECT u.*, p.*
                FROM Users u
                JOIN UserProfiles p ON u.Id = p.UserId
                WHERE u.IsActive = 1
              ]]></CommandText>
              <ConnectionAlias>ReadOnlyConnection</ConnectionAlias>
              <SplitOn>Id</SplitOn>
              <CommandTimeout>45</CommandTimeout>
            </Command>
          </Commands>
        </TypeSetting>
        <TypeSetting>
          <Name>ProductRepository</Name>
          <Commands>
            <Command Key="GetProductsAsync">
              <CommandText>SELECT * FROM Products WHERE IsActive = 1</CommandText>
              <ConnectionAlias>ReadOnlyConnection</ConnectionAlias>
            </Command>
            <Command Key="ProcessInventoryAsync">
              <CommandText>sp_ProcessInventory</CommandText>
              <ConnectionAlias>DefaultConnection</ConnectionAlias>
              <CommandType>StoredProcedure</CommandType>
              <CommandTimeout>300</CommandTimeout>
              <IsolationLevel>Serializable</IsolationLevel>
            </Command>
          </Commands>
        </TypeSetting>
      </Types>
    </NamespaceSetting>
  </Namespaces>
</CommanderSettings>
```

### Command Text Constants Pattern

For better maintainability, declare command text strings as constants in separate classes:

#### Command Constants Class

```csharp
namespace MyApp.Data.Commands
{
    /// <summary>
    /// SQL command constants for User operations
    /// </summary>
    public static class UserCommands
    {
        public const string GetAllUsers = @"SELECT Id, Name, Email, CreatedDate, IsActive 
                                           FROM Users 
                                           WHERE IsActive = 1
                                           ORDER BY Name";
        
        public const string GetUserById = @"SELECT Id, Name, Email, CreatedDate, IsActive 
                                           FROM Users 
                                           WHERE Id = @id";
        
        public const string CreateUser = @"INSERT INTO Users (Name, Email, CreatedDate, IsActive)
                                          VALUES (@Name, @Email, @CreatedDate, 1);
                                          SELECT SCOPE_IDENTITY();";
        
        public const string UpdateUser = @"UPDATE Users 
                                          SET Name = @Name, Email = @Email, ModifiedDate = @ModifiedDate
                                          WHERE Id = @Id";
        
        public const string DeleteUser = @"UPDATE Users 
                                          SET IsActive = 0, DeletedDate = @DeletedDate
                                          WHERE Id = @Id";
        
        public const string GetUsersWithProfiles = @"SELECT u.*, p.*
                                                     FROM Users u
                                                     JOIN UserProfiles p ON u.Id = p.UserId
                                                     WHERE u.IsActive = 1
                                                     ORDER BY u.Name";
    }
    
    /// <summary>
    /// SQL command constants for Product operations
    /// </summary>
    public static class ProductCommands
    {
        public const string GetAllProducts = @"SELECT Id, Name, Description, Price, CategoryId, IsActive
                                              FROM Products
                                              WHERE IsActive = 1
                                              ORDER BY Name";
        
        public const string GetProductById = @"SELECT Id, Name, Description, Price, CategoryId, IsActive
                                              FROM Products
                                              WHERE Id = @id AND IsActive = 1";
        
        public const string GetProductsByCategory = @"SELECT Id, Name, Description, Price, CategoryId, IsActive
                                                     FROM Products
                                                     WHERE CategoryId = @categoryId AND IsActive = 1
                                                     ORDER BY Name";
        
        public const string CreateProduct = @"INSERT INTO Products (Name, Description, Price, CategoryId, IsActive)
                                             VALUES (@Name, @Description, @Price, @CategoryId, 1);
                                             SELECT SCOPE_IDENTITY();";
        
        public const string ProcessInventory = "sp_ProcessInventory"; // Stored procedure
    }
}
```

#### Using Command Constants in Configuration

**JSON Configuration with Constants:**

```json
{
  "Namespaces": [
    {
      "Name": "MyApp.Repositories",
      "Types": [
        {
          "Name": "UserRepository",
          "Commands": {
            "GetAllUsersAsync": {
              "CommandText": "SELECT Id, Name, Email, CreatedDate, IsActive FROM Users WHERE IsActive = 1 ORDER BY Name",
              "ConnectionAlias": "ReadOnlyConnection"
            },
            "GetUserByIdAsync": {
              "CommandText": "SELECT Id, Name, Email, CreatedDate, IsActive FROM Users WHERE Id = @id",
              "ConnectionAlias": "ReadOnlyConnection"
            }
          }
        }
      ]
    }
  ]
}
```

**Programmatic Configuration with Constants:**

```csharp
using Syrx.Commanders.Databases.Settings.Extensions;
using MyApp.Data.Commands;

public void ConfigureServices(IServiceCollection services)
{
    services.UseSyrx(builder => builder
        .AddConnectionString("Default", connectionString)
        .AddConnectionString("ReadOnly", readOnlyConnectionString)
        .AddCommand(namespaces => namespaces
            .ForNamespace("MyApp.Repositories", ns => ns
                .ForType<UserRepository>(type => type
                    .ForMethod(nameof(UserRepository.GetAllUsersAsync), cmd => cmd
                        .UseCommandText(UserCommands.GetAllUsers)
                        .UseConnectionAlias("ReadOnly"))
                    .ForMethod(nameof(UserRepository.GetUserByIdAsync), cmd => cmd
                        .UseCommandText(UserCommands.GetUserById)
                        .UseConnectionAlias("ReadOnly"))
                    .ForMethod(nameof(UserRepository.CreateUserAsync), cmd => cmd
                        .UseCommandText(UserCommands.CreateUser)
                        .UseConnectionAlias("Default")
                        .SetCommandTimeout(60)))
                .ForType<ProductRepository>(type => type
                    .ForMethod(nameof(ProductRepository.GetAllProductsAsync), cmd => cmd
                        .UseCommandText(ProductCommands.GetAllProducts)
                        .UseConnectionAlias("ReadOnly"))
                    .ForMethod(nameof(ProductRepository.ProcessInventoryAsync), cmd => cmd
                        .UseCommandText(ProductCommands.ProcessInventory)
                        .UseConnectionAlias("Default")
                        .SetCommandType(CommandType.StoredProcedure)
                        .SetCommandTimeout(300))))));
}
```

### Repository Implementation Pattern

Your repositories should use `ICommander<TRepository>` dependency injection:

```csharp
using Syrx.Commanders.Databases;

namespace MyApp.Repositories
{
    public class UserRepository
    {
        private readonly ICommander<UserRepository> _commander;
        
        public UserRepository(ICommander<UserRepository> commander)
        {
            _commander = commander;
        }
        
        // Method names automatically map to configuration commands
        // Pattern: {Namespace}.{ClassName}.{MethodName}
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _commander.QueryAsync<User>();
        }
        
        public async Task<User?> GetUserByIdAsync(int id)
        {
            var users = await _commander.QueryAsync<User>(new { id });
            return users.FirstOrDefault();
        }
        
        public async Task<User> CreateUserAsync(User user)
        {
            user.CreatedDate = DateTime.UtcNow;
            return await _commander.ExecuteAsync(user) ? user : null;
        }
        
        public async Task<bool> UpdateUserAsync(User user)
        {
            user.ModifiedDate = DateTime.UtcNow;
            return await _commander.ExecuteAsync(user);
        }
        
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _commander.ExecuteAsync(new { Id = id, DeletedDate = DateTime.UtcNow });
        }
        
        // Multi-mapping example
        public async Task<IEnumerable<User>> GetUsersWithProfilesAsync()
        {
            return await _commander.QueryAsync<User, UserProfile, User>(
                (user, profile) => 
                {
                    user.Profile = profile;
                    return user;
                });
        }
    }
    
    public class ProductRepository
    {
        private readonly ICommander<ProductRepository> _commander;
        
        public ProductRepository(ICommander<ProductRepository> commander)
        {
            _commander = commander;
        }
        
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _commander.QueryAsync<Product>();
        }
        
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var products = await _commander.QueryAsync<Product>(new { id });
            return products.FirstOrDefault();
        }
        
        public async Task<bool> ProcessInventoryAsync(int productId, int quantity)
        {
            return await _commander.ExecuteAsync(new { ProductId = productId, Quantity = quantity });
        }
    }
}
```

### Environment-Specific Configuration Files

Support multiple environments with override files:

```
Project/
├── syrx.json                    # Base configuration
├── syrx.Development.json        # Development overrides
├── syrx.Staging.json           # Staging overrides
├── syrx.Production.json        # Production overrides
└── Program.cs
```

Configuration loading with environment support:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    var configBuilder = new ConfigurationBuilder();
    
    services.UseSyrx(builder => {
        // Load base configuration
        var syrxBuilder = builder.UseFile("syrx.json", configBuilder);
        
        // Load environment-specific overrides
        var environmentFile = $"syrx.{environment}.json";
        if (File.Exists(environmentFile))
        {
            syrxBuilder.UseFile(environmentFile, configBuilder);
        }
        
        return syrxBuilder;
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
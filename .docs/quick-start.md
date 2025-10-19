# Quick Start Guide

Get up and running with Syrx in minutes. This guide will walk you through creating your first Syrx-powered application.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Project Setup](#project-setup)
- [Basic Configuration](#basic-configuration)
- [Creating Your First Repository](#creating-your-first-repository)
- [Running Your Application](#running-your-application)
- [Next Steps](#next-steps)

## Prerequisites

- .NET 8.0 or later
- SQL Server, MySQL, PostgreSQL, or Oracle database
- Visual Studio 2022 or Visual Studio Code

## Installation

### 1. Install Core Syrx Package

```bash
dotnet add package Syrx
```

### 2. Install Database Provider

Choose your database provider:

#### SQL Server
```bash
dotnet add package Syrx.SqlServer.Extensions
```

#### MySQL
```bash
dotnet add package Syrx.MySql.Extensions
```

#### PostgreSQL
```bash
dotnet add package Syrx.Npgsql.Extensions
```

#### Oracle
```bash
dotnet add package Syrx.Oracle.Extensions
```

## Project Setup

### 1. Create a Simple Model

```csharp
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

### 2. Create a Repository Interface

```csharp
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}
```

### 3. Implement the Repository

```csharp
using Syrx;

public class UserRepository : IUserRepository
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

    public async Task<User> CreateUserAsync(User user)
    {
        var success = await _commander.ExecuteAsync(user);
        return success ? user : null;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var success = await _commander.ExecuteAsync(user);
        return success ? user : null;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _commander.ExecuteAsync(new { id });
    }
}
```

## Basic Configuration

### For ASP.NET Core Applications

Add to your `Program.cs` or `Startup.cs`:

```csharp
using Syrx.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Syrx with SQL Server
builder.Services.UseSyrx(syrxBuilder =>
{
    syrxBuilder.UseSqlServer(sqlServer =>
    {
        // Add connection string
        sqlServer.AddConnectionString("Default", 
            builder.Configuration.GetConnectionString("DefaultConnection"));

        // Configure commands for UserRepository
        sqlServer.AddCommand(types => types
            .ForType<UserRepository>(methods =>
            {
                methods.ForMethod(nameof(UserRepository.GetAllUsersAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("SELECT * FROM Users"));

                methods.ForMethod(nameof(UserRepository.GetUserByIdAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("SELECT * FROM Users WHERE Id = @id"));

                methods.ForMethod(nameof(UserRepository.CreateUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText(@"
                        INSERT INTO Users (FirstName, LastName, Email, CreatedDate)
                        VALUES (@FirstName, @LastName, @Email, @CreatedDate)"));

                methods.ForMethod(nameof(UserRepository.UpdateUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText(@"
                        UPDATE Users 
                        SET FirstName = @FirstName, 
                            LastName = @LastName, 
                            Email = @Email
                        WHERE Id = @Id"));

                methods.ForMethod(nameof(UserRepository.DeleteUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("DELETE FROM Users WHERE Id = @id"));
            }));
    });
});

// Register your repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure your app...
app.Run();
```

### For Console Applications

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syrx.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.UseSyrx(builder =>
        {
            builder.UseSqlServer(sqlServer =>
            {
                sqlServer.AddConnectionString("Default", 
                    "Server=localhost;Database=TestDb;Trusted_Connection=true;");

                // Configure your commands here...
            });
        });

        services.AddScoped<IUserRepository, UserRepository>();
    })
    .Build();

// Use your repository
var userRepository = host.Services.GetRequiredService<IUserRepository>();
var users = await userRepository.GetAllUsersAsync();

foreach (var user in users)
{
    Console.WriteLine($"{user.FirstName} {user.LastName} - {user.Email}");
}
```

### Connection String Configuration

Add to your `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=YourDatabase;Trusted_Connection=true;"
  }
}
```

## Creating Your First Repository

### 1. Database Table

Create a simple Users table:

```sql
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

### 2. Complete Repository Example

```csharp
using Syrx;

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

    public async Task<IEnumerable<User>> GetUsersByNameAsync(string searchTerm)
    {
        return await _commander.QueryAsync<User>(new { searchTerm });
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.CreatedDate = DateTime.UtcNow;
        var success = await _commander.ExecuteAsync(user);
        return success ? user : null;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var success = await _commander.ExecuteAsync(user);
        return success ? user : null;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _commander.ExecuteAsync(new { id });
    }
}
```

### 3. Extended Configuration

```csharp
builder.Services.UseSyrx(syrxBuilder =>
{
    syrxBuilder.UseSqlServer(sqlServer =>
    {
        // Connection strings
        sqlServer.AddConnectionString("Default", 
            builder.Configuration.GetConnectionString("DefaultConnection"));

        // Commands
        sqlServer.AddCommand(types => types
            .ForType<UserRepository>(methods =>
            {
                // Basic queries
                methods.ForMethod(nameof(UserRepository.GetAllUsersAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("SELECT * FROM Users ORDER BY LastName, FirstName"));

                methods.ForMethod(nameof(UserRepository.GetUserByIdAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("SELECT * FROM Users WHERE Id = @id"));

                // Search functionality
                methods.ForMethod(nameof(UserRepository.GetUsersByNameAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText(@"
                        SELECT * FROM Users 
                        WHERE FirstName LIKE '%' + @searchTerm + '%' 
                           OR LastName LIKE '%' + @searchTerm + '%'
                        ORDER BY LastName, FirstName"));

                // Insert with return
                methods.ForMethod(nameof(UserRepository.CreateUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText(@"
                        INSERT INTO Users (FirstName, LastName, Email, CreatedDate)
                        OUTPUT INSERTED.Id
                        VALUES (@FirstName, @LastName, @Email, @CreatedDate)"));

                // Update
                methods.ForMethod(nameof(UserRepository.UpdateUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText(@"
                        UPDATE Users 
                        SET FirstName = @FirstName, 
                            LastName = @LastName, 
                            Email = @Email
                        WHERE Id = @Id"));

                // Delete
                methods.ForMethod(nameof(UserRepository.DeleteUserAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("DELETE FROM Users WHERE Id = @id"));
            }));
    });
});
```

## Running Your Application

### 1. ASP.NET Core Controller Example

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        var createdUser = await _userRepository.CreateUserAsync(user);
        if (createdUser == null)
            return BadRequest();

        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        var updatedUser = await _userRepository.UpdateUserAsync(user);
        if (updatedUser == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userRepository.DeleteUserAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
```

### 2. Console Application Example

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHost();
        
        var userRepository = host.Services.GetRequiredService<IUserRepository>();

        // Create a new user
        var newUser = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        var createdUser = await userRepository.CreateUserAsync(newUser);
        Console.WriteLine($"Created user: {createdUser.FirstName} {createdUser.LastName}");

        // Get all users
        var users = await userRepository.GetAllUsersAsync();
        Console.WriteLine($"Total users: {users.Count()}");

        foreach (var user in users)
        {
            Console.WriteLine($"- {user.FirstName} {user.LastName} ({user.Email})");
        }
    }

    static IHost CreateHost()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.UseSyrx(builder =>
                {
                    builder.UseSqlServer(sqlServer =>
                    {
                        sqlServer.AddConnectionString("Default", 
                            "Server=localhost;Database=TestDb;Trusted_Connection=true;");

                        // Add your command configurations here
                    });
                });

                services.AddScoped<IUserRepository, UserRepository>();
            })
            .Build();
    }
}
```

## Next Steps

Now that you have a basic Syrx application running, explore these advanced features:

### 1. **Multi-mapping Queries**
Learn to handle complex object relationships:

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

### 2. **Multiple Database Support**
Configure different databases for different operations:

```csharp
services.UseSyrx(builder =>
{
    builder.UseSqlServer(sqlServer => { /* Transactional DB */ });
    builder.UseNpgsql(postgres => { /* Analytics DB */ });
});
```

### 3. **Advanced Configuration**
Explore configuration from JSON, environment variables, or external sources.

### 4. **Testing**
Learn how to create testable repositories with Syrx.

### 5. **Performance Optimization**
Discover caching strategies and performance best practices.

## Resources

- [Architecture Documentation](architecture.md)
- [ICommander Interface Guide](icommander-guide.md)
- [Configuration Guide](configuration-guide.md)
- [Testing Guide](testing-guide.md)
- [Performance Guide](performance-guide.md)

## Getting Help

- **GitHub Issues**: [https://github.com/Syrx/Syrx/issues](https://github.com/Syrx/Syrx/issues)
- **Documentation**: [https://github.com/Syrx/Syrx](https://github.com/Syrx/Syrx)
- **Stack Overflow**: Tag your questions with `syrx`

You're now ready to build powerful, database-agnostic applications with Syrx!

# ICommander Interface Guide

This document provides comprehensive guidance on using the `ICommander<TRepository>` interface, which serves as the primary entry point for all data access operations in Syrx.

## Table of Contents

- [Overview](#overview)
- [Interface Design](#interface-design)
- [Query Operations](#query-operations)
- [Execute Operations](#execute-operations)
- [Multi-map Queries](#multi-map-queries)
- [Multiple Result Sets](#multiple-result-sets)
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

## Multiple Result Sets

Multiple Result Sets queries enable processing of SQL commands that return multiple independent result sets, such as stored procedures or batch queries that execute multiple SELECT statements. Unlike multi-mapping which combines related data into objects, Multiple Result Sets handle completely separate data collections returned from a single database call.

### Supported Overloads

Syrx supports Multiple Result Sets queries with up to 16 result sets, each returning its own collection of strongly-typed objects:

```csharp
// Single result set (equivalent to regular QueryAsync but with mapping function)
Task<IEnumerable<TResult>> QueryAsync<T1, TResult>(
    Func<IEnumerable<T1>, IEnumerable<TResult>> map,
    object parameters = null,
    CancellationToken cancellationToken = default,
    [CallerMemberName] string method = null);

// Two result sets
Task<IEnumerable<TResult>> QueryAsync<T1, T2, TResult>(
    Func<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<TResult>> map,
    object parameters = null,
    CancellationToken cancellationToken = default,
    [CallerMemberName] string method = null);

// Three result sets
Task<IEnumerable<TResult>> QueryAsync<T1, T2, T3, TResult>(
    Func<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<TResult>> map,
    object parameters = null,
    CancellationToken cancellationToken = default,
    [CallerMemberName] string method = null);

// ... up to 16 result sets
```

### Database-Specific SQL Patterns

Each database provider requires different SQL syntax for multiple result sets:

#### SQL Server
```sql
-- Using dynamic SQL with loops
DECLARE @sets INT = 3,
        @loop INT = 1,
        @template NVARCHAR(MAX) = N'SELECT * FROM Users WHERE DepartmentId = @deptId AND Id <= @maxId';

WHILE @loop <= @sets
BEGIN
    EXEC sp_executesql @template, N'@deptId INT, @maxId INT', @deptId = @loop, @maxId = @loop * 10;
    SET @loop = @loop + 1;
END;
```

#### MySQL
```sql
-- Multiple SELECT statements
SELECT * FROM Users WHERE DepartmentId = 1;
SELECT * FROM Users WHERE DepartmentId = 2;
SELECT * FROM Users WHERE DepartmentId = 3;
```

#### PostgreSQL  
```sql
-- Multiple SELECT statements
SELECT * FROM users WHERE department_id = 1;
SELECT * FROM users WHERE department_id = 2;
SELECT * FROM users WHERE department_id = 3;
```

#### Oracle
```sql
-- Using REF CURSORS
BEGIN
    OPEN :1 FOR SELECT * FROM users WHERE department_id = 1;
    OPEN :2 FOR SELECT * FROM users WHERE department_id = 2; 
    OPEN :3 FOR SELECT * FROM users WHERE department_id = 3;
END;
```

### Usage Examples

#### Basic Two Result Sets
```csharp
public class ReportsRepository
{
    private readonly ICommander<ReportsRepository> _commander;

    public async Task<DashboardData> GetDashboardDataAsync()
    {
        return await _commander.QueryAsync<User, Order, DashboardData>(MapDashboardData);
    }

    // Mapping function defined as a private method
    private static IEnumerable<DashboardData> MapDashboardData(IEnumerable<User> users, IEnumerable<Order> orders)
    {
        return new List<DashboardData>
        {
            new DashboardData
            {
                TotalUsers = users.Count(),
                ActiveUsers = users.Count(u => u.IsActive),
                TotalOrders = orders.Count(),
                TotalRevenue = orders.Sum(o => o.Total),
                AverageOrderValue = orders.Any() ? orders.Average(o => o.Total) : 0
            }
        };
    }
}
```

**Configuration (JSON):**
```json
{
  "Commands": {
    "GetDashboardDataAsync": {
      "CommandText": "SELECT * FROM Users; SELECT * FROM Orders WHERE CreatedDate >= DATEADD(month, -1, GETDATE());",
      "ConnectionAlias": "Default"
    }
  }
}
```

#### Department Analytics with Three Result Sets
```csharp
public class DepartmentRepository
{
    private readonly ICommander<DepartmentRepository> _commander;

    public async Task<DepartmentAnalytics> GetDepartmentAnalyticsAsync(int departmentId)
    {
        return await _commander.QueryAsync<Employee, Project, Budget, DepartmentAnalytics>(
            (employees, projects, budgets) => MapDepartmentAnalytics(employees, projects, budgets, departmentId),
            new { departmentId });
    }

    // Mapping function defined as a private method
    private static IEnumerable<DepartmentAnalytics> MapDepartmentAnalytics(
        IEnumerable<Employee> employees, 
        IEnumerable<Project> projects, 
        IEnumerable<Budget> budgets,
        int departmentId)
    {
        var analytics = new DepartmentAnalytics
        {
            DepartmentId = departmentId,
            EmployeeCount = employees.Count(),
            ActiveProjects = projects.Count(p => p.Status == "Active"),
            CompletedProjects = projects.Count(p => p.Status == "Completed"),
            TotalBudget = budgets.Sum(b => b.AllocatedAmount),
            SpentBudget = budgets.Sum(b => b.SpentAmount),
            AverageSalary = employees.Any() ? employees.Average(e => e.Salary) : 0,
            TopPerformers = employees
                .OrderByDescending(e => e.PerformanceScore)
                .Take(5)
                .ToList()
        };

        return new List<DepartmentAnalytics> { analytics };
    }
}
```

**Configuration (XML):**
```xml
<Command Key="GetDepartmentAnalyticsAsync">
  <CommandText><![CDATA[
    SELECT * FROM Employees WHERE DepartmentId = @departmentId;
    SELECT * FROM Projects WHERE DepartmentId = @departmentId;
    SELECT * FROM Budgets WHERE DepartmentId = @departmentId AND FiscalYear = YEAR(GETDATE());
  ]]></CommandText>
  <ConnectionAlias>Default</ConnectionAlias>
  <CommandTimeout>60</CommandTimeout>
</Command>
```

#### Complex Financial Report with Multiple Result Sets
```csharp
public class FinancialRepository
{
    private readonly ICommander<FinancialRepository> _commander;

    public async Task<FinancialReport> GenerateQuarterlyReportAsync(int year, int quarter)
    {
        return await _commander.QueryAsync<Sale, Expense, Customer, Product, Employee, FinancialReport>(
            (sales, expenses, customers, products, employees) => 
                MapFinancialReport(sales, expenses, customers, products, employees, year, quarter),
            new { year, quarter });
    }

    // Mapping function defined as a private method
    private static IEnumerable<FinancialReport> MapFinancialReport(
        IEnumerable<Sale> sales,
        IEnumerable<Expense> expenses,
        IEnumerable<Customer> customers,
        IEnumerable<Product> products,
        IEnumerable<Employee> employees,
        int year,
        int quarter)
    {
        var report = new FinancialReport
        {
            Year = year,
            Quarter = quarter,
            GeneratedDate = DateTime.UtcNow,
            
            // Sales Analysis
            TotalSales = sales.Sum(s => s.Amount),
            SalesCount = sales.Count(),
            AverageSaleAmount = sales.Any() ? sales.Average(s => s.Amount) : 0,
            TopSellingProducts = sales
                .GroupBy(s => s.ProductId)
                .Select(g => new { ProductId = g.Key, TotalSales = g.Sum(s => s.Amount) })
                .OrderByDescending(x => x.TotalSales)
                .Take(10)
                .Join(products, s => s.ProductId, p => p.Id, (s, p) => new TopProduct
                {
                    ProductName = p.Name,
                    TotalSales = s.TotalSales
                })
                .ToList(),
            
            // Expense Analysis
            TotalExpenses = expenses.Sum(e => e.Amount),
            ExpensesByCategory = expenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount)),
            
            // Customer Analysis
            NewCustomers = customers.Count(c => c.CreatedDate >= GetQuarterStart(year, quarter)),
            ActiveCustomers = customers.Count(c => c.LastOrderDate >= GetQuarterStart(year, quarter)),
            CustomerRetentionRate = CalculateRetentionRate(customers),
            
            // Performance Metrics
            NetProfit = sales.Sum(s => s.Amount) - expenses.Sum(e => e.Amount),
            TopPerformers = employees
                .OrderByDescending(e => e.QuarterlySales)
                .Take(5)
                .ToList()
        };

        return new List<FinancialReport> { report };
    }

    private static DateTime GetQuarterStart(int year, int quarter)
    {
        return new DateTime(year, (quarter - 1) * 3 + 1, 1);
    }

    private static decimal CalculateRetentionRate(IEnumerable<Customer> customers)
    {
        var totalCustomers = customers.Count();
        if (totalCustomers == 0) return 0;
        
        var retainedCustomers = customers.Count(c => c.IsRetained);
        return (decimal)retainedCustomers / totalCustomers * 100;
    }
}
```

#### Stored Procedure with Multiple Result Sets
```csharp
public class UserRepository
{
    private readonly ICommander<UserRepository> _commander;

    public async Task<ComprehensiveUserReport> GetUserReportAsync(int userId)
    {
        return await _commander.QueryAsync<UserProfile, Order, Address, PaymentMethod, ComprehensiveUserReport>(
            MapUserReport,
            new { userId });
    }

    // Mapping function defined as a private method
    private IEnumerable<ComprehensiveUserReport> MapUserReport(
        IEnumerable<UserProfile> profiles,
        IEnumerable<Order> orders,
        IEnumerable<Address> addresses,
        IEnumerable<PaymentMethod> paymentMethods)
    {
        var profile = profiles.FirstOrDefault();
        if (profile == null)
            return Enumerable.Empty<ComprehensiveUserReport>();

        var report = new ComprehensiveUserReport
        {
            User = profile,
            OrderHistory = orders.OrderByDescending(o => o.CreatedDate).ToList(),
            Addresses = addresses.ToList(),
            PaymentMethods = paymentMethods.Where(pm => pm.IsActive).ToList(),
            TotalSpent = orders.Sum(o => o.Total),
            OrderCount = orders.Count(),
            AverageOrderValue = orders.Any() ? orders.Average(o => o.Total) : 0,
            LastOrderDate = orders.Any() ? orders.Max(o => o.CreatedDate) : (DateTime?)null,
            PreferredShippingAddress = addresses.FirstOrDefault(a => a.IsDefault),
            AccountStatus = DetermineAccountStatus(profile, orders)
        };

        return new List<ComprehensiveUserReport> { report };
    }

    private static string DetermineAccountStatus(UserProfile profile, IEnumerable<Order> orders)
    {
        // Implementation logic for determining account status
        return "Active"; // Simplified for example
    }
}
```

**Stored Procedure (SQL Server):**
```sql
CREATE PROCEDURE sp_GetUserReport
    @userId INT
AS
BEGIN
    -- User Profile
    SELECT * FROM Users WHERE Id = @userId;
    
    -- Order History (last 2 years)
    SELECT * FROM Orders 
    WHERE UserId = @userId 
      AND CreatedDate >= DATEADD(year, -2, GETDATE())
    ORDER BY CreatedDate DESC;
    
    -- User Addresses
    SELECT * FROM UserAddresses WHERE UserId = @userId;
    
    -- Payment Methods
    SELECT * FROM PaymentMethods 
    WHERE UserId = @userId AND IsActive = 1;
END;
```

### Error Handling and Best Practices

#### Graceful Error Handling
```csharp
public class ReportsRepository
{
    private readonly ICommander<ReportsRepository> _commander;
    private readonly ILogger<ReportsRepository> _logger;

    public async Task<DashboardData> GetDashboardDataSafeAsync()
    {
        try
        {
            return await _commander.QueryAsync<User, Order, DashboardData>(MapSafeDashboard);
        }
        catch (SqlException ex) when (ex.Number == 2) // Timeout
        {
            _logger.LogWarning("Dashboard query timed out, returning empty data");
            return new DashboardData { HasData = false, ErrorMessage = "Data temporarily unavailable" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating dashboard data");
            throw;
        }
    }

    // Mapping function defined as a private method
    private static IEnumerable<DashboardData> MapSafeDashboard(IEnumerable<User> users, IEnumerable<Order> orders)
    {
        // Handle null collections
        users ??= Enumerable.Empty<User>();
        orders ??= Enumerable.Empty<Order>();

        return new List<DashboardData>
        {
            new DashboardData
            {
                TotalUsers = users.Count(),
                TotalOrders = orders.Count(),
                HasData = users.Any() || orders.Any()
            }
        };
    }
}
```

#### Performance Optimization
```csharp
public async Task<LargeDatasetReport> GetOptimizedReportAsync(int pageSize = 1000)
{
    Func<IEnumerable<Summary>, IEnumerable<Detail>, IEnumerable<LargeDatasetReport>> mapOptimizedReport =
        (summaries, details) =>
        {
            // Process data efficiently
            var summaryLookup = summaries.ToDictionary(s => s.Id);
            
            var report = new LargeDatasetReport
            {
                Summaries = summaries.ToList(),
                // Only include details that have corresponding summaries
                ProcessedDetails = details
                    .Where(d => summaryLookup.ContainsKey(d.SummaryId))
                    .GroupBy(d => d.SummaryId)
                    .ToDictionary(g => g.Key, g => g.ToList())
            };

            return new List<LargeDatasetReport> { report };
        };

    return await _commander.QueryAsync<Summary, Detail, LargeDatasetReport>(
        mapOptimizedReport,
        new { pageSize });
}
```

### Configuration Examples

#### Programmatic Configuration
```csharp
services.UseSyrx(builder => builder
    .UseSqlServer(sqlServer => sqlServer
        .AddConnectionString("Default", connectionString)
        .AddCommand(types => types
            .ForType<ReportsRepository>(methods => methods
                .ForMethod(nameof(ReportsRepository.GetDashboardDataAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText(@"
                        SELECT Id, Name, Email, IsActive, CreatedDate FROM Users;
                        SELECT Id, UserId, Total, CreatedDate FROM Orders 
                        WHERE CreatedDate >= DATEADD(month, -1, GETDATE());")
                    .SetCommandTimeout(120))
                .ForMethod(nameof(ReportsRepository.GetUserReportAsync), cmd => cmd
                    .UseConnectionAlias("Default")
                    .UseCommandText("sp_GetUserReport")
                    .SetCommandType(CommandType.StoredProcedure)
                    .SetCommandTimeout(60))))));
```

#### JSON Configuration
```json
{
  "Namespaces": [
    {
      "Name": "MyApp.Repositories",
      "Types": [
        {
          "Name": "ReportsRepository",
          "Commands": {
            "GetDashboardDataAsync": {
              "CommandText": "SELECT * FROM Users; SELECT * FROM Orders WHERE CreatedDate >= DATEADD(month, -1, GETDATE());",
              "ConnectionAlias": "Default",
              "CommandTimeout": 120
            },
            "GetDepartmentAnalyticsAsync": {
              "CommandText": "SELECT * FROM Employees WHERE DepartmentId = @departmentId; SELECT * FROM Projects WHERE DepartmentId = @departmentId; SELECT * FROM Budgets WHERE DepartmentId = @departmentId;",
              "ConnectionAlias": "Default",
              "CommandTimeout": 90
            }
          }
        }
      ]
    }
  ]
}
```

### Key Differences from Multi-mapping

| Feature | Multi-mapping | Multiple Result Sets |
|---------|---------------|---------------------|
| **Purpose** | Combine related data into objects | Process independent result sets |
| **SQL Pattern** | JOINs with split-on points | Multiple SELECT statements |
| **Input Types** | Individual records from joined results | Complete collections per result set |
| **Mapping Function** | `Func<T1, T2, TResult>` | `Func<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<TResult>>` |
| **Use Cases** | Object composition, parent-child relationships | Dashboards, reports, batch operations |
| **Performance** | Efficient for related data | Efficient for independent queries |

Multiple Result Sets is ideal for scenarios where you need to execute multiple independent queries in a single database round-trip, such as dashboard data loading, comprehensive reports, or batch data processing operations.

## Method Resolution

Method Resolution

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
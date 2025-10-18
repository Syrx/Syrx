# Syrx.Extensions

Dependency injection and service collection extensions for the Syrx data access framework.

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Installation](#installation)
- [Usage](#usage)
  - [Basic Registration](#basic-registration)
  - [With Configuration](#with-configuration)
- [Related Packages](#related-packages)
- [License](#license)
- [Credits](#credits)

## Overview

`Syrx.Extensions` provides extension methods and utilities for integrating Syrx with Microsoft.Extensions.DependencyInjection and other .NET extension frameworks. This package simplifies the setup and configuration of Syrx components in your applications.

## Key Features

- **Service Collection Extensions**: Easy registration of Syrx components with DI containers
- **Configuration Builders**: Fluent APIs for setting up Syrx services
- **Lifetime Management**: Proper service lifetime management for Syrx components
- **Integration Helpers**: Utilities for integrating with ASP.NET Core and other frameworks

## Installation

```bash
dotnet add package Syrx.Extensions
```

**Package Manager**
```bash
Install-Package Syrx.Extensions
```

**PackageReference**
```xml
<PackageReference Include="Syrx.Extensions" Version="2.4.3" />
```

## Usage

Add Syrx services to your dependency injection container:

```csharp
using Syrx.Extensions;

public void ConfigureServices(IServiceCollection services)
{
    services.UseSyrx(builder => builder
        // Configure your database providers and settings
    );
}
```

### With Configuration

```csharp
services.UseSyrx(factory => factory
    .UseSqlServer(sqlServer => sqlServer
        .AddConnectionString("Default", connectionString)
        .AddCommand(/* command configuration */)
    )
);
```

## Related Packages

- **[Syrx](https://www.nuget.org/packages/Syrx/)**: Core Syrx interfaces and abstractions
- **[Syrx.SqlServer.Extensions](https://www.nuget.org/packages/Syrx.SqlServer.Extensions/)**: SQL Server-specific extensions
- **[Syrx.MySql.Extensions](https://www.nuget.org/packages/Syrx.MySql.Extensions/)**: MySQL-specific extensions

## License

This project is licensed under the [MIT License](https://github.com/Syrx/Syrx/blob/main/LICENSE).

## Credits

Syrx is built on top of [Dapper](https://github.com/DapperLib/Dapper) and follows its performance-focused philosophy.
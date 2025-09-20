# Syrx.Connectors

Core connector abstractions for the Syrx data access framework.

## Table of Contents

- [Overview](#overview)
- [Key Components](#key-components)
- [Installation](#installation)
- [Usage](#usage)
- [Database Providers](#database-providers)
- [License](#license)
- [Credits](#credits)

## Overview

`Syrx.Connectors` provides the foundational interfaces and abstractions for connecting to data sources within the Syrx ecosystem. This package defines the `IConnector<TConnection, TSetting>` interface that all database-specific connectors implement.

## Key Components

- **IConnector Interface**: Generic connector abstraction for creating connections to data sources
- **Connection Management**: Base functionality for managing data source connections
- **Type-safe Configuration**: Strongly-typed settings for connection configuration

## Installation

```bash
dotnet add package Syrx.Connectors
```

**Package Manager**
```bash
Install-Package Syrx.Connectors
```

**PackageReference**
```xml
<PackageReference Include="Syrx.Connectors" Version="2.4.5" />
```

## Usage

This package is typically consumed by database-specific implementations rather than directly by application code. For example:

```csharp
public class DatabaseConnector : IConnector<IDbConnection, CommandSetting>
{
    public IDbConnection CreateConnection(CommandSetting setting)
    {
        // Implementation specific logic
    }
}
```

## Database Providers

This package serves as the foundation for all Syrx database providers:
- [Syrx.SqlServer](https://www.nuget.org/packages/Syrx.SqlServer/)
- [Syrx.MySql](https://www.nuget.org/packages/Syrx.MySql/)
- [Syrx.Npgsql](https://www.nuget.org/packages/Syrx.Npgsql/)
- [Syrx.Oracle](https://www.nuget.org/packages/Syrx.Oracle/)

## License

This project is licensed under the [MIT License](https://github.com/Syrx/Syrx/blob/main/LICENSE).

## Credits

Syrx is built on top of [Dapper](https://github.com/DapperLib/Dapper) and follows its performance-focused philosophy.
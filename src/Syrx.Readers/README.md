# Syrx.Readers

Command and configuration readers for the Syrx data access framework.

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Installation](#installation)
- [Key Interfaces](#key-interfaces)
  - [IReader](#ireader)
- [Usage](#usage)
- [Command Resolution](#command-resolution)
- [Related Packages](#related-packages)
- [License](#license)
- [Credits](#credits)

## Overview

`Syrx.Readers` provides the core reading abstractions and implementations for retrieving command settings and configurations within the Syrx ecosystem. This package defines interfaces and base implementations for reading command configurations from various sources.

## Key Features

- **Configuration Readers**: Abstractions for reading command settings
- **Type Resolution**: Utilities for resolving types and methods to their configurations
- **Extensible Design**: Support for custom configuration sources
- **Thread-Safe Operations**: Safe for concurrent access in multi-threaded scenarios

## Installation

```bash
dotnet add package Syrx.Readers
```

**Package Manager**
```bash
Install-Package Syrx.Readers
```

**PackageReference**
```xml
<PackageReference Include="Syrx.Readers" Version="2.4.3" />
```

## Key Interfaces

### IReader

The core interface for reading configurations:

```csharp
public interface IReader<TSetting> where TSetting : ICommandSetting
{
    TSetting GetCommand(Type type, string method);
}
```

## Usage

This package is typically used as a foundation for database-specific reader implementations:

```csharp
public class DatabaseCommandReader : IReader<CommandSetting>
{
    public CommandSetting GetCommand(Type type, string method)
    {
        // Implementation for retrieving command settings
        // based on namespace.type.method resolution
    }
}
```

## Command Resolution

Syrx uses a hierarchical approach to resolve commands:

```
Namespace → Type → Method → Command Setting
```

This allows for organized configuration that mirrors your code structure.

## Related Packages

- **[Syrx](https://www.nuget.org/packages/Syrx/)**: Core Syrx interfaces and abstractions
- **[Syrx.Settings](https://www.nuget.org/packages/Syrx.Settings/)**: Configuration settings definitions
- **[Syrx.Commanders.Databases.Settings.Readers](https://www.nuget.org/packages/Syrx.Commanders.Databases.Settings.Readers/)**: Database-specific readers

## License

This project is licensed under the [MIT License](https://github.com/Syrx/Syrx/blob/main/LICENSE).

## Credits

Syrx is built on top of [Dapper](https://github.com/DapperLib/Dapper) and follows its performance-focused philosophy.
# Syrx.Settings

Configuration settings and abstractions for the Syrx data access framework.

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Installation](#installation)
- [Core Interfaces](#core-interfaces)
  - [ISettings<TCommandSetting>](#isettingstcommandsetting)
  - [ICommandSetting](#icommandsetting)
  - [INamespaceSetting<TCommandSetting>](#inamespacesettingtcommandsetting)
- [Configuration Hierarchy](#configuration-hierarchy)
- [Usage](#usage)
- [Related Packages](#related-packages)
- [License](#license)
- [Credits](#credits)

## Overview

`Syrx.Settings` provides the foundational configuration interfaces and types used throughout the Syrx ecosystem. This package defines the core settings abstractions that enable Syrx's flexible configuration system.

## Key Features

- **Configuration Interfaces**: Core abstractions for all Syrx settings
- **Hierarchical Structure**: Settings organized by namespace, type, and method
- **Type Safety**: Strongly-typed configuration definitions
- **Extensibility**: Support for custom setting implementations

## Installation

```bash
dotnet add package Syrx.Settings
```

**Package Manager**
```bash
Install-Package Syrx.Settings
```

**PackageReference**
```xml
<PackageReference Include="Syrx.Settings" Version="2.4.5" />
```

## Core Interfaces

### ISettings<TCommandSetting>

The root settings interface for all Syrx commanders:

```csharp
public interface ISettings<TCommandSetting> where TCommandSetting : ICommandSetting
{
    IEnumerable<INamespaceSetting<TCommandSetting>> Namespaces { get; }
}
```

### ICommandSetting

Base interface for all command settings:

```csharp
public interface ICommandSetting
{
    // Base interface for command configuration
}
```

### INamespaceSetting<TCommandSetting>

Namespace-level settings container:

```csharp
public interface INamespaceSetting<TCommandSetting> where TCommandSetting : ICommandSetting
{
    string Name { get; }
    IEnumerable<ITypeSetting<TCommandSetting>> Types { get; }
}
```

## Configuration Hierarchy

Syrx settings follow a hierarchical structure that mirrors your code organization:

```
ISettings
├── NamespaceSettings[]
    ├── TypeSettings[]
        ├── CommandSettings[]
```

This structure allows for organized configuration that directly maps to:
- `Namespace`: Your .NET namespace
- `Type`: Your repository or service class
- `Method`: Your repository method
- `Command`: The SQL command and its configuration

## Usage

This package provides the foundation for all Syrx configuration. Database-specific implementations extend these interfaces:

```csharp
public class CommandSetting : ICommandSetting
{
    public string CommandText { get; set; }
    public int CommandTimeout { get; set; }
    public CommandType CommandType { get; set; }
    // Additional database-specific properties
}
```

## Related Packages

- **[Syrx](https://www.nuget.org/packages/Syrx/)**: Core Syrx interfaces and abstractions
- **[Syrx.Commanders.Databases.Settings](https://www.nuget.org/packages/Syrx.Commanders.Databases.Settings/)**: Database-specific settings implementations
- **[Syrx.Readers](https://www.nuget.org/packages/Syrx.Readers/)**: Configuration readers

## License

This project is licensed under the [MIT License](https://github.com/Syrx/Syrx/blob/main/LICENSE).

## Credits

Syrx is built on top of [Dapper](https://github.com/DapperLib/Dapper) and follows its performance-focused philosophy.
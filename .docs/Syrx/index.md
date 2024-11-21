# Index

This doc serves a dual purpose - sets out an index for the documentation as well as helps me gather my thoughts on what documentation needs writing. 


|tite|purpose|
|--|--|
|readme|Elevator pitch to people who find the project.|
|quick-start|Repository implementation for a contrived example. Will probably benefit from doing this in even smaller, more targeted docs.|
|quick-start-retrieval|Quick start focusing on the `Query<>` methods.
|quick-start-retrieval-composition|Sample showing how to use `Func<>` to compose more complex object graphs|

query
query-parameters
query-composition
query-cancellation
query-asynch
query-multimap
query-multiple
execute
execute-async
configuration-declarative
configuration-json
configuration-xml

# Project Documentation
Documentation per project. The intention of Syrx is to provide data access to any underlying data store. We're starting with RDBMS and will, hopefully, add non-relational stores and caches as well. 

## Core
The core interfaces for the Syrx project. These are primarily interface definitions. 

|library|purpose|documentation|
|--|--|
|Syrx|Home of `ICommander<>`|
|Syrx.Connectors|Interface contract to establish connections to a data store.|
|Syrx.Extensions|Entry point for configuration extension methods.|
|Syrx.Readers|Interface for retrieving an `ICommandSetting`.|
|Syrx.Settings|Interfaces for establishing the configuration system.|

## Databases

#### Implementation 

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases|Implementation of `ICommander<>` for RDBMS.||
|Syrx.Commanders.Databases.Builders|Experimental project for future usage.|
|Syrx.Commanders.Databases.Connectors|Base implmentation of `IConnector` for databases.|
|Syrx.Commanders.Databases.Connectors.Extensions|Extension methods for setting up database connectors using options pattern.|
|Syrx.Commanders.Databases.Extensions|Extension methods for configuring whihc RDBMS to use.|
|Syrx.Commanders.Databases.Settings|Implmentation for configuration of database commands. Part of the reason Syrx exists at all.|
|Syrx.Commanders.Databases.Settings.Extensions|Extension methods for configuration using options/builder patterns.|
|Syrx.Commanders.Databases.Settings.Extensions.Json|Extensions to support configuration from JSON files.|
|Syrx.Commanders.Databases.Settings.Extensions.Xml|Extensions to support configuration form XML files.|
|Syrx.Commanders.Databases.Settings.Readers|Implmentation of the `IReader` for retrieving database commands.|
|Syrx.Commanders.Databases.Settings.Readers.Extensions|Extension methods for configuring readers using options pattern.|

#### Tests

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Builders.Tests.Unit||
|Syrx.Commanders.Databases.Connectors.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Connectors.Tests.Unit||
|Syrx.Commanders.Databases.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Settings.Extensions.Json.Tests.Unit||
|Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Settings.Extensions.Xml.Tests.Unit||
|Syrx.Commanders.Databases.Settings.Readers.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Settings.Readers.Tests.Unit||
|Syrx.Commanders.Databases.Settings.Tests.Unit||
|Syrx.Commanders.Databases.Tests.Integration||
|Syrx.Commanders.Databases.Tests.Integration.Models||
|Syrx.Tests.Extensions|


## Databases | SQL Server

#### Implementation 

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.SqlServer|Establishes an IDbConnection against SQL Server instances.|
|Syrx.Commanders.Databases.Connectors.SqlServer.Extensions||

#### Tests

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.SqlServer.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Connectors.SqlServer.Tests.Unit||
|Syrx.SqlServer.Tests.Integration||

## Databases | MySQL

#### Implementation 

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.MySql|Establishes an IDbConnection against MySql instances.|
|Syrx.Commanders.Databases.Connectors.MySql.Extensions||

#### Tests

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.MySql.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Connectors.MySql.Tests.Unit||
|Syrx.MySql.Tests.Integration|

## Databases | Postgres

#### Implementation 

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.Npgsql|Establishes an IDbConnection against Postgre instances.|
|Syrx.Commanders.Databases.Connectors.Npgsql.Extensions||

#### Tests
|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.Npgsql.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Connectors.Npgsql.Tests.Unit||
|Syrx.Npgsql.Tests.Integration|


## Databases | Oracle

#### Implementation 

|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.Oracle|Establishes an IDbConnection against Oracle instances|
|Syrx.Commanders.Databases.Connectors.Oracle.Extensions||
|Syrx.Commanders.Databases.Oracle|Provides multiple result set support to Syrx (via Dapper) from Oracle. This is necessary so that all implementations of the Syrx database commander provide support for multiple result sets.|

#### Tests
|library|purpose|documentation|
|--|--|
|Syrx.Commanders.Databases.Connectors.Oracle.Extensions.Tests.Unit||
|Syrx.Commanders.Databases.Connectors.Oracle.Tests.Unit||
|Syrx.Oracle.Tests.Integration||
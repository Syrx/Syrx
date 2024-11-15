global using Microsoft.Extensions.DependencyInjection;
global using Syrx.Commanders.Databases.Connectors.Oracle.Extensions;
global using Syrx.Commanders.Databases.Oracle;
global using Syrx.Commanders.Databases.Settings.Extensions;
global using Syrx.Commanders.Databases.Tests.Integration.Models;
global using Syrx.Commanders.Databases.Tests.Integration.Models.Immutable;
global using Syrx.Extensions;
global using Syrx.Oracle.Tests.Integration.DatabaseCommanderTests;
global using Syrx.Tests.Extensions;
global using System.Transactions;
global using Testcontainers.Oracle;
global using static Syrx.Commanders.Databases.Oracle.OracleDynamicParameters;
global using static Syrx.Validation.Contract;
global using static Xunit.Assert;

global using DotNet.Testcontainers.Builders;
global using DotNet.Testcontainers.Containers;
global using Microsoft.Extensions.Logging;
global using System.Diagnostics;

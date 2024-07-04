//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:41)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Commanders.Databases.Settings;
using Syrx.Commanders.Databases.Settings.Extensions;
using System.Data;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Tests.Unit.SqlServerDatabaseConnectorTests
{
    public class CreateConnection
    {
        private readonly CommanderSettings _settings;
        private readonly IDatabaseConnector _connector;
        public CreateConnection()
        {
            _settings = CommanderSettingsBuilderExtensions.Build(
                a => a.AddConnectionString("test.alias", "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=master;Integrated Security=true;")
                .AddCommand(
                    b => b.ForType<CreateConnection>(
                    c => c.ForMethod(nameof(Successfully), 
                    d => d.UseConnectionAlias("test.alias")
                          .UseCommandText("select 'readers.test.settings'")))));

            _connector = new SqlServerDatabaseConnector(_settings);

        }

        [Fact]
        public void Successfully()
        {
            var setting = _settings.Namespaces.First().Types.First().Commands.First().Value;
            var result = _connector.CreateConnection(setting);
            Equal(ConnectionState.Closed, result.State);
        }
    }
}
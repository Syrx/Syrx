//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================
namespace Syrx.Commanders.Databases.Connectors.MySql.Tests.Unit.MySqlDatabaseConnectorTests
{
    public class CreateConnection
    {
        private readonly CommanderSettings _settings;
        private readonly IDatabaseConnector _connector;
        public CreateConnection()
        {
            _settings = CommanderSettingsBuilderExtensions.Build(
                a => a.AddConnectionString("test.alias", "Host=localhost;Port=5432;Database=syrx;Username=postgres;Password=syrxforpostgres;")
                .AddCommand(
                    b => b.ForType<CreateConnection>(
                    c => c.ForMethod(nameof(Successfully),
                    d => d.UseConnectionAlias("test.alias")
                          .UseCommandText("select 'readers.test.settings'")))));

            _connector = new MySqlDatabaseConnector(_settings);

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
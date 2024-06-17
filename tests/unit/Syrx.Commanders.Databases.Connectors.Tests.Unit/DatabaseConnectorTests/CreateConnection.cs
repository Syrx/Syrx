//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Moq;
using Syrx.Commanders.Databases.Settings;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Tests.Extensions;
using System.Data.Common;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.Tests.Unit.DatabaseConnectorTests
{

    public class CreateConnection
    {
        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";
        private const string CommandText = "select 'readers.test.settings'";
        private const string Method = "Retrieve";

        private readonly CommanderSettings _settings;

        public CreateConnection()
        {
            _settings = CommanderOptionsBuilderExtensions.Build(
                a => a
                .AddConnectionString(Alias, ConnectionString)
                .AddCommand(
                    b => b.ForType<CreateConnection>(
                    c => c.ForMethod(Method, 
                    d => d.UseConnectionAlias(Alias).UseCommandText(CommandText))))
                      );
        }

        [Fact]
        public void NoAliasedConnectionThrowsNullReferenceException()
        {
            var providerMock = new Mock<DbProviderFactory>();
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new CommandSetting { ConnectionAlias = "does.not.exist", CommandText = CommandText };

            var result = Throws<NullReferenceException>(() => connector.CreateConnection(setting));
            Equal(
                $"There is no connection with the alias '{setting.ConnectionAlias}' in the settings. Please check settings.",
                result.Message);
        }

        [Fact]
        public void NullCommandSettingThrowsArgumentNullException()
        {
            var providerMock = new Mock<DbProviderFactory>();
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var result = Throws<ArgumentNullException>(() => connector.CreateConnection(null));
            result.ArgumentNull("options");
        }

        [Fact]
        public void ProviderExceptionReturnedToCaller()
        {
            var providerMock = new Mock<DbProviderFactory>();
            providerMock.Setup(x => x.CreateConnection()).Throws(new NotImplementedException("Unit test"));
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new CommandSetting { CommandText = CommandText, ConnectionAlias = Alias };
            var result = Throws<NotImplementedException>(() => connector.CreateConnection(setting));
            Equal("Unit test", result.Message);
        }

        [Fact]
        public void ProviderReturnsNullConnectionThrowsNullReferenceException()
        {
            var providerMock = new Mock<DbProviderFactory>();
            providerMock.Setup(x => x.CreateConnection()).Returns(() => null);
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new CommandSetting { CommandText = CommandText, ConnectionAlias = Alias };
            var result = Throws<NullReferenceException>(() => connector.CreateConnection(setting));
            Equal(
                $"The provider predicate did not return a connection for the aliased connection '{setting.ConnectionAlias}'.",
                result.Message);
        }

        [Fact]
        public void Successfully()
        {
            var connectionMock = new Mock<DbConnection>();
            var providerMock = new Mock<DbProviderFactory>();
            providerMock.Setup(x => x.CreateConnection()).Returns(() => connectionMock.Object);
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new CommandSetting { CommandText = CommandText, ConnectionAlias = Alias };
            var result = connector.CreateConnection(setting);
            NotNull(result);
            Equal(System.Data.ConnectionState.Closed, result.State);
        }
    }
}
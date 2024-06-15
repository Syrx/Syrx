//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Moq;
using Syrx.Commanders.Databases.Settings;
using Syrx.Tests.Extensions;
using System.Data.Common;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.Tests.Unit.DatabaseConnectorTests
{
    public class CreateConnection
    {
        public CreateConnection()
        {
            _settings = new DatabaseCommanderSettings(
                new List<DatabaseCommandNamespaceSetting>
                {
                    new DatabaseCommandNamespaceSetting(
                        typeof(DatabaseCommandNamespaceSetting).Namespace,
                        new List<DatabaseCommandTypeSetting>
                        {
                            new DatabaseCommandTypeSetting(
                                typeof(DatabaseCommandTypeSetting).FullName,
                                new Dictionary<string, DatabaseCommandSetting>
                                {
                                    ["Retrieve"] =
                                    new DatabaseCommandSetting("test.alias", "select 'Readers.Test.Settings'")
                                })
                        })
                }
                , new List<ConnectionStringSetting>
                {
                    new ConnectionStringSetting("test.alias", "connectionString")
                });
        }

        private readonly IDatabaseCommanderSettings _settings;

        [Fact]
        public void NoAliasedConnectionThrowsNullReferenceException()
        {
            var providerMock = new Mock<DbProviderFactory>();
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new DatabaseCommandSetting("Does.Not.Exist", "test");

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
            result.ArgumentNull("commandSetting");
        }

        [Fact]
        public void ProviderExceptionReturnedToCaller()
        {
            var providerMock = new Mock<DbProviderFactory>();
            providerMock.Setup(x => x.CreateConnection()).Throws(new NotImplementedException("Unit test"));
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new DatabaseCommandSetting("test.alias", "select 1");
            var result = Throws<NotImplementedException>(() => connector.CreateConnection(setting));
            Equal("Unit test", result.Message);
        }

        [Fact]
        public void ProviderReturnsNullConnectionThrowsNullReferenceException()
        {
            var providerMock = new Mock<DbProviderFactory>();
            providerMock.Setup(x => x.CreateConnection()).Returns(() => null);
            var connector = new DatabaseConnector(_settings, () => providerMock.Object);
            var setting = new DatabaseCommandSetting("test.alias", "select 1");
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
            var setting = new DatabaseCommandSetting("test.alias", "select 1");
            var result = connector.CreateConnection(setting);
            NotNull(result);
            Equal(System.Data.ConnectionState.Closed, result.State);
        }
    }
}
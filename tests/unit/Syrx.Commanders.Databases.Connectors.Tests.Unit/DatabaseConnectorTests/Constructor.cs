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
    public class Constructor
    {
        public Constructor()
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
        public void NullPredicateThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseConnector(_settings, null));
            result.ArgumentNull("providerPredicate");
        }

        [Fact]
        public void NullSettingsThrowsArgumentNullException()
        {
            var providerMock = new Mock<DbProviderFactory>();
            var result = Throws<ArgumentNullException>(() => new DatabaseConnector(null, () => providerMock.Object));
            result.ArgumentNull("settings");
        }

        [Fact]
        public void Successfully()
        {
            var providerMock = new Mock<DbProviderFactory>();
            var result = new DatabaseConnector(_settings, () => providerMock.Object);
            NotNull(result);
        }
    }
}
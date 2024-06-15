//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Tests.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.DatabaseCommanderSettingsTests
{
    public class Constructor
    {
        public Constructor()
        {
            _connectionStringSettings =
            [
                new ConnectionStringSetting("test", "connectionString")
            ];

            _namespaceSettings =
            [
                new("test.namespace",
                [
                    new("test.type", new Dictionary<string, DatabaseCommandSetting>
                    {
                        ["Retrieve"] = new DatabaseCommandSetting("alias", "select 1")
                    })
                ])
            ];
        }

        private readonly IEnumerable<DatabaseCommandNamespaceSetting> _namespaceSettings;
        private readonly IEnumerable<ConnectionStringSetting> _connectionStringSettings;

        [Fact]
        public void EmptyConnectionStringSettingListThrowsArgumentException()
        {
            var result = Throws<ArgumentException>(() =>
                new DatabaseCommanderSettings(_namespaceSettings, new List<ConnectionStringSetting>()));
            Equal("At least 1 ConnectionStringSetting was expected.", result.Message);
        }

        [Fact]
        public void EmptyNamespaceSettingListThrowsArgumentException()
        {
            var result = Throws<ArgumentException>(() =>
                new DatabaseCommanderSettings(new List<DatabaseCommandNamespaceSetting>(), _connectionStringSettings));
            Equal(
                "At least 1 DatabaseCommandNamespaceSetting was expected to be passed to the DatabaseCommanderSettings constructor.",
                result.Message);
        }

        [Fact]
        public void NullConnectionStringSettingsSupported()
        {
            var result = new DatabaseCommanderSettings(_namespaceSettings);
            Equal(_namespaceSettings, result.Namespaces);
        }

        [Fact]
        public void NullNamespacesSettingThrowsArgumentNullException()
        {
            var result =
                Throws<ArgumentNullException>(() =>
                    new DatabaseCommanderSettings(null, _connectionStringSettings));
            //Equal("Value cannot be null.\r\nParameter name: namespaces", result.Message);
            result.ArgumentNull("namespaces");
        }

        [Fact]
        public void Successfully()
        {
            var result = new DatabaseCommanderSettings(_namespaceSettings, _connectionStringSettings);
            Equal(_connectionStringSettings, result.Connections);
            Equal(_namespaceSettings, result.Namespaces);
        }
    }
}
//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Moq;
using Syrx.Commanders.Databases.Extensions.Configuration;
using Syrx.Commanders.Databases.Extensions.Configuration.Builders;
using Syrx.Commanders.Databases.Settings;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Tests.Extensions;
using System.Data.Common;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.Tests.Unit.DatabaseConnectorTests
{
    public class Constructor
    {

        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";
        private const string CommandText = "select 'readers.test.settings'";
        private const string Method = "Retrieve";
        private readonly CommanderOptions _settings;

        public Constructor()
        {
            _settings = CommanderOptionsBuilderExtensions.Build(
                a => a
                .AddConnectionString(Alias, ConnectionString)
                .AddCommand(b => b.ForType<DatabaseCommandNamespaceSetting>(
                        c => c.ForMethod(Method, 
                        d => d.UseCommandText(CommandText).UseConnectionAlias(Alias)))));

        }
        
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
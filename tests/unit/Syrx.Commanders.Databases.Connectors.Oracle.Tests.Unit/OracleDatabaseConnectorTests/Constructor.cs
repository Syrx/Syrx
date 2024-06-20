//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:41)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Commanders.Databases.Settings;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Tests.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.Oracle.Tests.Unit.OracleDatabaseConnectorTests
{
    public class Constructor
    {
        private readonly CommanderSettings _settings;
        public Constructor()
        {
            _settings = CommanderSettingsBuilderExtensions.Build(
                a => a.AddCommand(
                    b => b.ForType<Constructor>(
                        c => c.ForMethod(nameof(Successfully),
                        d => d.UseConnectionAlias("test-alias")
                        .UseCommandText("test-command-text")))));

        }

        [Fact]
        public void NullSettingsThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => new OracleDatabaseConnector(null));
            result.ArgumentNull("settings");
        }

        [Fact]
        public void Successfully()
        {
            var result = new OracleDatabaseConnector(_settings);
            NotNull(result);
        }
    }
}
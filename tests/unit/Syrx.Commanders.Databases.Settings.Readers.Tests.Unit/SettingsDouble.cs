//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Commanders.Databases.Settings.Readers.Tests.Unit.DatabaseCommandReaderTests;

namespace Syrx.Commanders.Databases.Settings.Readers.Tests.Unit
{
    public static class SettingsDouble
    {
        public static CommanderSettings GetOptions()
        {
            return CommanderSettingsBuilderExtensions.Build(
                a => a.AddConnectionString("test-alias", "test-connection-string")
                      .AddCommand(
                        b => b.ForType<GetCommand>(
                            c => c.ForMethod("Retrieve",
                                d => d.UseCommandText("test-command-text")
                                      .UseConnectionAlias("test-alias")))));
        }
    }
}
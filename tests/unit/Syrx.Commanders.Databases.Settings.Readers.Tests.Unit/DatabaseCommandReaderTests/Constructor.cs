//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Microsoft.Extensions.Options;

namespace Syrx.Commanders.Databases.Settings.Readers.Tests.Unit.DatabaseCommandReaderTests
{
    public class Constructor
    {
        [Fact]
        public void NullSettingsThrowsArgumentNullException()
        {
            // todo: replace with null
            CommanderSettings options = null;
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandReader(options));
            const string expected = "Value cannot be null. (Parameter 'settings. No settings were passed to DatabaseCommandReader.')";
            result.HasMessage(expected);
        }

        [Fact]
        public void Successfully()
        {
            var settings = SettingsDouble.GetOptions();
            var options = Options.Create(settings);
            var result = new DatabaseCommandReader(settings);
            NotNull(result);
        }
    }
}
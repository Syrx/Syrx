//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Tests.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.DatabaseCommandTypeSettingTests
{
    public class Constructor
    {
        public Constructor()
        {
            _commands = new Dictionary<string, DatabaseCommandSetting>
            {
                ["test"] = new DatabaseCommandSetting("Test", "select 1")
            };
        }

        private readonly IDictionary<string, DatabaseCommandSetting> _commands;

        [Fact]
        public void EmptyCommandsThrowArgumentException()
        {
            var result = Throws<ArgumentException>(() =>
                new DatabaseCommandTypeSetting("test", new Dictionary<string, DatabaseCommandSetting>()));
            const string expect = "'test' must have at least 1 CommandSetting to be valid. Please check settings.";
            Equal(expect, result.Message);
        }

        [Fact]
        public void NullCommandsThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandTypeSetting("test", null));
            const string expect =
                "Value cannot be null. (Parameter 'commands. The commands collection for 'test' is null. Please check settings.')";
            Equal(expect, result.Message);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceNameThrowsArgumentNullException(string name)
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandTypeSetting(name, _commands));

            const string expect =
                "Value cannot be null. (Parameter 'name. All type entries must have a name to be valid. Please check settings.')";
            Equal(expect, result.Message);

        }

        [Fact]
        public void Successfully()
        {
            var result = new DatabaseCommandTypeSetting("test", _commands);
            Equal("test", result.Name);
            NotNull(result.Commands);
            True(result.Commands.Any());
            Single(result.Commands);
        }
    }
}
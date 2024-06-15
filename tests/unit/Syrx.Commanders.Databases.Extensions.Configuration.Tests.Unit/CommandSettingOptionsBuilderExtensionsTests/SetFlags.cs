using Syrx.Commanders.Databases.Extensions.Configuration.Tests.Unit.CommandSettingOptionsBuilderTests;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Tests.Unit.CommandSettingOptionsBuilderExtensionsTests
{
    public class SetFlags(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {

        [Fact]
        public void DefaultsToBufferedNoCache()
        {
            var result = CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetFlags());
            NotNull(result);
            Equal(CommandFlagSetting.Buffered | CommandFlagSetting.NoCache, result.Flags);
        }

        [Theory]
        [MemberData(nameof(AllFlags))]
        public void Successfully(CommandFlagSetting setting)
        {
            var result = CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetFlags(setting));
            NotNull(result);
            Equal(setting, result.Flags);

        }


        private static IEnumerable<object[]> AllFlags()
        {
            var flags = EnumExtensions.GetFlags<CommandFlagSetting>();
            var settings = flags as CommandFlagSetting[] ?? flags.ToArray();

            var values = (from none in settings
                          from buffered in settings
                          from pipelined in settings
                          from cache in settings
                          select none | buffered | pipelined | cache).Distinct();

            return from unique in values
                   select new object[] { unique };
        }
    }
}

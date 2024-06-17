using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderExtensionsTests
{
    public class UseCommandTest(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSplitThrowsArgumentException(string commandText)
        {
            var result = Throws<ArgumentNullException>(() => CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(commandText)
                .UseConnectionAlias(fixture.Alias)));
            result.ArgumentNull(nameof(commandText));
        }


        [Fact]
        public void Successfully()
        {
            var result = CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias));
            NotNull(result);
            Equal(fixture.CommandText, result.CommandText);
        }
    }

}

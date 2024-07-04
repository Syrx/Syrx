using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderExtensionsTests
{
    public class UseCommandTest(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSplitThrowsArgumentException(string commandText)
        {
            var result = Throws<ArgumentNullException>(() => CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(commandText)
                .UseConnectionAlias(fixture.Alias)));
            result.ArgumentNull(nameof(commandText));
        }


        [Fact]
        public void Successfully()
        {
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias));
            NotNull(result);
            Equal(fixture.CommandText, result.CommandText);
        }
    }

}

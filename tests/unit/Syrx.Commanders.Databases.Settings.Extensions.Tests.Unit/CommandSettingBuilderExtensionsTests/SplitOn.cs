using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderExtensionsTests
{
    public class SplitOn(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSplitThrowsArgumentException(string split)
        {
            var result = Throws<ArgumentNullException>(() => CommandSettingBuilderExtensions.Build(x => x.SplitOn(split)));
            result.ArgumentNull(nameof(split));
        }


        [Fact]
        public void DefaultsToId()
        {
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SplitOn());
            NotNull(result);
            Equal("id", result.Split);
        }


        [Fact]
        public void Successfully()
        {
            const string split = "test-split";
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SplitOn(split));
            NotNull(result);
            Equal(split, result.Split);
        }

    }

}

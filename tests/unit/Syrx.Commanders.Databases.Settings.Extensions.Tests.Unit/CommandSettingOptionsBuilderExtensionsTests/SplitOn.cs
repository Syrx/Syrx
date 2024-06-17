using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderExtensionsTests
{
    public class SplitOn(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSplitThrowsArgumentException(string split)
        {
            var result = Throws<ArgumentNullException>(() => CommandSettingOptionsBuilderExtensions.Build(x => x.SplitOn(split)));
            result.ArgumentNull(nameof(split));
        }


        [Fact]
        public void DefaultsToId()
        {
            var result = CommandSettingOptionsBuilderExtensions.Build(
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
            var result = CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SplitOn(split));
            NotNull(result);
            Equal(split, result.Split);
        }

    }

}

using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderExtensionsTests
{
    public class SetCommandTimeout(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CannotBeLessThanOne(int commandTimeout)
        {
            var result = Throws<ArgumentException>(() => CommandSettingBuilderExtensions.Build(x => x.SetCommandTimeout(commandTimeout)));
            result.HasMessage($"CommandTimeout cannot be less than 1. The value '{commandTimeout}' is not valid.");
        }

        [Fact]
        public void DefaultsTo30()
        {
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetCommandTimeout());
            NotNull(result);
            Equal(30, result.CommandTimeout);
        }


        [Fact]
        public void Successfully()
        {
            const int timeout = 23;
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetCommandTimeout(timeout));
            NotNull(result);
            Equal(timeout, result.CommandTimeout);
        }
    }

}

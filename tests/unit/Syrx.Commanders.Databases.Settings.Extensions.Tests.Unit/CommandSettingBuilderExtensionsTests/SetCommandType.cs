using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderExtensionsTests
{
    public class SetCommandType(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        [Fact]
        public void DefaultsToText()
        {
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetCommandType());
            NotNull(result);
            Equal(30, result.CommandTimeout);
        }

        [Theory]
        [InlineData(CommandType.Text)]
        [InlineData(CommandType.StoredProcedure)]
        [InlineData(CommandType.TableDirect)]
        public void Successfully(CommandType commandType)
        {
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetCommandType(commandType));
            NotNull(result);
            Equal(commandType, result.CommandType);
        }
    }

}

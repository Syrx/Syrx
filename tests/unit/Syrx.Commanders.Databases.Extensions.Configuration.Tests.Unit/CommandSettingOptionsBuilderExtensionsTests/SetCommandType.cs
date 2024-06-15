using Syrx.Commanders.Databases.Extensions.Configuration.Tests.Unit.CommandSettingOptionsBuilderTests;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Tests.Unit.CommandSettingOptionsBuilderExtensionsTests
{
    public class SetCommandType(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        [Fact]
        public void DefaultsToText()
        {
            var result = CommandSettingOptionsBuilderExtensions.Build(
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
            var result = CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetCommandType(commandType));
            NotNull(result);
            Equal(commandType, result.CommandType);
        }
    }

}

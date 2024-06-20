using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderExtensionsTests
{
    public class SetIsolationLevel(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {

        [Fact]
        public void DefaultsToSerializable()
        {
            var result = CommandSettingBuilderExtensions.Build(
               x => x
               .UseCommandText(fixture.CommandText)
               .UseConnectionAlias(fixture.Alias)
               .SetIsolationLevel());
            NotNull(result);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
        }


        [Theory]
        [MemberData(nameof(Generators.IsolationLevels), MemberType = typeof(Generators))]
        public void Successfully(IsolationLevel isolationLevel)
        {
            var result = CommandSettingBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetIsolationLevel(isolationLevel));
            NotNull(result);
            Equal(isolationLevel, result.IsolationLevel);
        }
    }

}

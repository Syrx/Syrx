using Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderTests;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderExtensionsTests
{
    public class SetIsolationLevel(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {

        [Fact]
        public void DefaultsToSerializable()
        {
            var result = CommandSettingOptionsBuilderExtensions.Build(
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
            var result = CommandSettingOptionsBuilderExtensions.Build(
                x => x
                .UseCommandText(fixture.CommandText)
                .UseConnectionAlias(fixture.Alias)
                .SetIsolationLevel(isolationLevel));
            NotNull(result);
            Equal(isolationLevel, result.IsolationLevel);
        }
    }

}

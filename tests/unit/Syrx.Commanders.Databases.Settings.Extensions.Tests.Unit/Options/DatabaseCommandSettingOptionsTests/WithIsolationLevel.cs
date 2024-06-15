// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (19:28)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{

    // todo: need to redo these tests to go up one level. 
    public class WithIsolationLevel
    {
        private const string CommandText = "test-command-text";

        public WithIsolationLevel()
        {
        }

        [Fact]
        public void DefaultsToSerializable()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToSerializable))
                    .UseCommandText(CommandText)
                    .WithIsolationLevel());
            Equal(IsolationLevel.Serializable, result.CommandSetting.IsolationLevel);
        }


        [Theory]
        [MemberData(nameof(Generators.IsolationLevels), MemberType = typeof(Generators))]
        public void Successfully(IsolationLevel level)
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(Successfully))
                    .UseCommandText(CommandText)
                    .WithIsolationLevel(level));
            Equal(level, result.CommandSetting.IsolationLevel);

        }

    }
}
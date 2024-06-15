// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (18:33)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class UsingCommandType
    {
        private const string CommandText = "test-command-text";

        [Fact]
        public void DefaultsToText()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToText))
                    .UseCommandText(CommandText));

            Equal(CommandType.Text, result.CommandSetting.CommandType);
        }

        [Fact]
        public void OptionalParameterDefaultsWhenNotSet()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(OptionalParameterDefaultsWhenNotSet))
                    .UseCommandText(CommandText)
                    .UsingCommandType());

            Equal(CommandType.Text, result.CommandSetting.CommandType);
        }

        [Theory]
        [MemberData(nameof(Generators.CommandTypes), MemberType = typeof(Generators))]
        public void Successfully(CommandType commandType)
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(Successfully))
                    .UseCommandText(CommandText)
                    .UsingCommandType(commandType));

            Equal(commandType, result.CommandSetting.CommandType);
        }
    }
}
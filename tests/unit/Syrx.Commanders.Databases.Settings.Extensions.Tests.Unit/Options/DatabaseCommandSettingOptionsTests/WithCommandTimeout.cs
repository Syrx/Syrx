// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (18:37)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class WithCommandTimeout
    {
        private const string CommandText = "test-command-text";

        public WithCommandTimeout()
        {
        }

        [Fact]
        public void DefaultsTo30()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
               .AddCommand(a => a
                   .ForRepositoryType<WithIsolationLevel>()
                   .ForMethodNamed(nameof(DefaultsTo30))
                   .UseCommandText(CommandText));

            Equal(30, result.CommandSetting.CommandTimeout);
        }

        [Fact]
        public void OptionalParameterDefaultsWhenNotSupplied()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
               .AddCommand(a => a
                   .ForRepositoryType<WithIsolationLevel>()
                   .ForMethodNamed(nameof(OptionalParameterDefaultsWhenNotSupplied))
                   .UseCommandText(CommandText)
                   .WithCommandTimeout());

            Equal(30, result.CommandSetting.CommandTimeout);
        }

        [Fact]
        public void Successfully()
        {
            var random = new Random();
            var expected = random.Next(10, 60);
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                           .AddCommand(a => a
                               .ForRepositoryType<WithIsolationLevel>()
                               .ForMethodNamed(nameof(Successfully))
                               .UseCommandText(CommandText)
                               .WithCommandTimeout(expected));

            Equal(expected, result.CommandSetting.CommandTimeout);
        }
    }
}
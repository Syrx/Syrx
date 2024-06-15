// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (18:24)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class UseCommandText
    {
        private const string CommandText = "test-command-text";

        public UseCommandText()
        {
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceThrowsArgumentNullException(string commandText)
        {
            var result = Throws<ArgumentNullException>(() => DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(NullEmptyWhitespaceThrowsArgumentNullException))
                    .UseCommandText(commandText)));
            result.ArgumentNull(nameof(commandText));
        }

        [Fact]
        public void Succesfully()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
               .AddCommand(a => a
                   .ForRepositoryType<WithIsolationLevel>()
                   .ForMethodNamed(nameof(Succesfully))
                   .UseCommandText(CommandText));

            Equal(CommandText, result.CommandSetting.CommandText);
        }
    }
}
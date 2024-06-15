// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (18:13)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class AgainstConnectionAlias
    {
        private const string CommandText = "test-command-text";

        //[Theory]
        //[MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        //public void NullEmptyWhitespaceThrowsArgumentNullException(string connectionAlias)
        //{
        //    var result = Throws<ArgumentNullException>(() => DatabaseCommandSettingOptionsBuilderExtensions
        //        .AddCommand(a => a
        //            .ForRepositoryType(typeof(AgainstConnectionAlias))
        //            .ForMethodNamed(nameof(NullEmptyWhitespaceThrowsArgumentNullException))
        //            .UseCommandText(CommandText)
        //            .AgainstConnectionAlias(connectionAlias)));
        //    result.ArgumentNull(nameof(connectionAlias));
        //}

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void DefaultsToTypeFullNameIfTypeIsSet(string alias)
        {
            var expected = typeof(AgainstConnectionAlias).FullName;
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<AgainstConnectionAlias>()
                    .ForMethodNamed(nameof(DefaultsToTypeFullNameIfTypeIsSet))
                    .UseCommandText(CommandText));

            Equal(expected, result.CommandSetting.ConnectionAlias);
        }
    }
}
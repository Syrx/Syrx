// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (19:11)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class WithCommandFlags
    {
        private const string CommandText = "test-command-text";
        public WithCommandFlags()
        {
        }

        [Fact]
        public void DefaultsToBufferedNoCache()
        {
            const CommandFlagSetting expected = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache;
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToBufferedNoCache))
                    .UseCommandText(CommandText)
                    .WithCommandFlags()
                    .WithIsolationLevel());
            Equal(expected, result.CommandSetting.Flags);
        }

        [Fact]
        public void OptionalParameterDefaultsToBufferedNoCache()
        {
            const CommandFlagSetting expected = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache;
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToBufferedNoCache))
                    .UseCommandText(CommandText)
                    .WithIsolationLevel());
            Equal(expected, result.CommandSetting.Flags);
        }


        [Theory]
        [MemberData(nameof(UniqueCommandFlagSettings))]
        public void Successfully(CommandFlagSetting setting)
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToBufferedNoCache))
                    .UseCommandText(CommandText)
                    .WithCommandFlags(setting)
                    .WithIsolationLevel());
            Equal(setting, result.CommandSetting.Flags);
        }

        public static IEnumerable<object[]> UniqueCommandFlagSettings()
        {
            var flags = EnumExtensions.GetFlags<CommandFlagSetting>();
            var settings = flags as CommandFlagSetting[] ?? flags.ToArray();

            var values = (from none in settings
                          from buffered in settings
                          from pipelined in settings
                          from cache in settings
                          select none | buffered | pipelined | cache).Distinct();

            return from unique in values
                   select new object[] { unique };
        }
    }
}
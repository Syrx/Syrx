namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderTests
{
    public class CommandSettingOptionBuilderFixture
    {
        public CommandSettingOptionsBuilder Builder { get; }
        public string Alias { get; set; } = "test-alias";
        public string CommandText { get; set; } = "test-command-text";

        public CommandSettingOptionBuilderFixture()
        {
            Builder = new CommandSettingOptionsBuilder();
        }
    }
}

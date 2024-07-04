namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests
{
    public class CommandSettingOptionBuilderFixture
    {
        public CommandSettingBuilder Builder { get; }
        public string Alias { get; set; } = "test-alias";
        public string CommandText { get; set; } = "test-command-text";

        public CommandSettingOptionBuilderFixture()
        {
            Builder = new CommandSettingBuilder();
        }
    }
}

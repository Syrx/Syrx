namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class CommandSettingOptionsBuilderExtensions
    {
        public static CommandSetting Build(Action<CommandSettingOptionsBuilder> builder)
        {
            var result = new CommandSettingOptionsBuilder();
            builder(result);
            return result.Build();
        }
    }

}
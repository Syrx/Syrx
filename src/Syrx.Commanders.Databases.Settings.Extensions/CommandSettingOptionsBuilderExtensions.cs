namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class CommandSettingBuilderExtensions
    {
        public static CommandSetting Build(Action<CommandSettingBuilder> factory)
        {
            var builder = new CommandSettingBuilder();
            factory(builder);
            return builder.Build();
        }
    }

}
namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class CommanderSettingsBuilderExtensions
    {
        public static CommanderSettings Build(Action<CommanderSettingsBuilder> factory)
        {
            Throw<ArgumentNullException>(factory != null, nameof(factory));
            var builder = new CommanderSettingsBuilder();
            factory!(builder);
            var result = builder.Build();
            return result;
        }
    }

}

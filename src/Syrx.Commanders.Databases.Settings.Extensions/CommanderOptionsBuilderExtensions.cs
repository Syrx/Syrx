namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class CommanderOptionsBuilderExtensions
    {
        public static CommanderSettings Build(Action<CommanderOptionsBuilder> builder)
        {
            Throw<ArgumentNullException>(builder != null, nameof(builder));
            var options = new CommanderOptionsBuilder();
            builder!(options);
            var result = options.Build();
            return result;
        }
    }

}

namespace Syrx.Commanders.Databases.Extensions.Configuration.Builders
{
    public static class CommanderOptionsBuilderExtensions
    {
        public static CommanderOptions Build(Action<CommanderOptionsBuilder> builder)
        {
            Throw<ArgumentNullException>(builder!= null, nameof(builder));
            var options = new CommanderOptionsBuilder();
            builder!(options);
            var result = options.Build();
            return result;  
        }
    }

}

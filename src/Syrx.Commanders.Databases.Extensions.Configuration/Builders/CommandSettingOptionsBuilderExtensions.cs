namespace Syrx.Commanders.Databases.Extensions.Configuration.Builders
{
    public static class CommandSettingOptionsBuilderExtensions
    {
        public static CommandSettingOptions Build(Action<CommandSettingOptionsBuilder> builder) 
        {
            var result = new CommandSettingOptionsBuilder();
            builder(result);
            return result.Build();
        }
    }

}
namespace Syrx.Commanders.Databases.Connectors.MySql.Extensions
{
    public static class MySqlConnectorExtensions
    {
        public static SyrxBuilder UseMySql(
            this SyrxBuilder builder,
            Action<CommanderSettingsBuilder> factory,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var options = CommanderSettingsBuilderExtensions.Build(factory);
            builder.ServiceCollection
                .AddSingleton<ICommanderSettings, CommanderSettings>(a => options)
                .AddReader(lifetime) // add reader
                .AddMySql(lifetime) // add connector
                .AddDatabaseCommander(lifetime);

            return builder;
        }
    }
}

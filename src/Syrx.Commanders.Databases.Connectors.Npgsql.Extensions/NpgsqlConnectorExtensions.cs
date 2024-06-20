namespace Syrx.Commanders.Databases.Connectors.Npgsql.Extensions
{
    public static class NpgsqlConnectorExtensions
    {
        public static SyrxBuilder UsePostgres(
            this SyrxBuilder builder,
            Action<CommanderSettingsBuilder> factory,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var options = CommanderSettingsBuilderExtensions.Build(factory);
            builder.ServiceCollection
                .AddSingleton<ICommanderSettings, CommanderSettings>(a => options)
                .AddReader(lifetime) // add reader
                .AddPostgres(lifetime) // add connector
                .AddDatabaseCommander(lifetime);

            return builder;
        }

    }
}

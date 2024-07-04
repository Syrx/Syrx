namespace Syrx.Commanders.Databases.Connectors.Oracle.Extensions
{
    public static class OracleConnectorExtensions
    {
        public static SyrxBuilder UseOracle(
            this SyrxBuilder builder,
            Action<CommanderSettingsBuilder> factory,
            ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var options = CommanderSettingsBuilderExtensions.Build(factory);
            builder.ServiceCollection
                .AddSingleton<ICommanderSettings, CommanderSettings>(a => options)
                .AddReader(lifetime) // add reader
                .AddOracle(lifetime) // add connector
                .AddDatabaseCommander(lifetime);

            return builder;
        }

    }
}

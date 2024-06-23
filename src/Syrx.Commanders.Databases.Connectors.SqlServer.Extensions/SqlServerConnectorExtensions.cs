// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (22:10)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================


namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions
{
    public static class SqlServerConnectorExtensions
    {
        public static SyrxBuilder UseSqlServer(
            this SyrxBuilder builder,
            Action<CommanderSettingsBuilder> factory,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var options = CommanderSettingsBuilderExtensions.Build(factory);
            builder.ServiceCollection
                .AddTransient<ICommanderSettings, CommanderSettings>(a => options)
                .AddReader(lifetime) // add reader
                .AddSqlServer(lifetime) // add connector
                .AddDatabaseCommander(lifetime);
            
            return builder;
        }
        
    }
}
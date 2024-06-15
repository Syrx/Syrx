// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (22:10)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions
{
    public static class SqlServerOptionsBuilderExtensions
    {
        public static SyrxOptionsBuilder UseSqlServer(
            this SyrxOptionsBuilder builder,
            IDatabaseCommanderSettings settings,
            ServiceLifetime lifetime = ServiceLifetime.Transient
            )
        {
            builder.ServiceCollection
                .AddSqlServer(lifetime)
                .AddDatabaseCommander(settings, lifetime);
            return builder;
        }

        public static SyrxOptionsBuilder UseSqlServer(
            this SyrxOptionsBuilder builder,
            Action<DatabaseCommanderSettingsOptions> factory,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder.Build(factory);
            return builder.UseSqlServer(settings, lifetime);
        }
    }
}
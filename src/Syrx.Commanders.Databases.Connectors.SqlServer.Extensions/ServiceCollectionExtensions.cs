// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (21:30)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSqlServer(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseConnector),
                typeof(SqlServerDatabaseConnector),
                lifetime);
        }
    }
}
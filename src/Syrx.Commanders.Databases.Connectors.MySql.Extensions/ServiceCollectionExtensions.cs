namespace Syrx.Commanders.Databases.Connectors.MySql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddMySql(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseConnector),
                typeof(MySqlDatabaseConnector),
                lifetime);
        }
    }
}

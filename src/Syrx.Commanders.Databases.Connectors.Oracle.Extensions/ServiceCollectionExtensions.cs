namespace Syrx.Commanders.Databases.Connectors.Oracle.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddOracle(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseConnector),
                typeof(OracleDatabaseConnector),
                lifetime);
        }
    }
}

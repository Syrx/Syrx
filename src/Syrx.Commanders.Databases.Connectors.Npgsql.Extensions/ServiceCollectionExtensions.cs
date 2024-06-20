namespace Syrx.Commanders.Databases.Connectors.Npgsql.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddPostgres(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseConnector),
                typeof(NpgsqlDatabaseConnector),
                lifetime);
        }
    }
}

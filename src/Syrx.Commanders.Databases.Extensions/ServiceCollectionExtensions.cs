// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (20:51)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseCommander(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services
                .TryAddToServiceCollection(typeof(ICommander<>), typeof(DatabaseCommander<>), lifetime);

        }
    }
}
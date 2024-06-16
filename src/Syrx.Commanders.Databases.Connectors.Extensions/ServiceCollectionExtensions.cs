using Microsoft.Extensions.DependencyInjection;
using Syrx.Extensions;
using System.Data.Common;

namespace Syrx.Commanders.Databases.Connectors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /*
        public static IServiceCollection AddProvider(
            this IServiceCollection services,
            Func<DbProviderFactory> providerFactory)
        {
            return services.TryAddToServiceCollection(
                typeof(DbProviderFactory),
                providerFactory);
        }

        public static IServiceCollection AddDatabaseConnector<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient
            ) where TService : IDatabaseConnector where TImplementation : class, IDatabaseConnector
        {
            return services.TryAddToServiceCollection(
                typeof(TService),
                typeof(TImplementation),
                lifetime);
        }
        */
    }
}

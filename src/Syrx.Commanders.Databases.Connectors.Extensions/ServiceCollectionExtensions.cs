using Microsoft.Extensions.DependencyInjection;
using Syrx.Extensions;
using System.Data.Common;
using static Syrx.Validation.Contract;

namespace Syrx.Commanders.Databases.Connectors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // these exist tpo provide support for 
        // providers and connectors that have 
        // yet to be written... 

        public static IServiceCollection AddProvider(
            this IServiceCollection services,
            Func<DbProviderFactory> providerFactory
            )
        {
            Throw(providerFactory != null,
                () => new ArgumentNullException(nameof(providerFactory),
                $"The {nameof(DbProviderFactory)} delegate cannot be null."));

            return services.TryAddToServiceCollection(
                typeof(Func<DbProviderFactory>), 
                providerFactory!);
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
        
    }
}

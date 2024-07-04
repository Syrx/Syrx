using Microsoft.Extensions.DependencyInjection;
using Syrx.Extensions;

namespace Syrx.Commanders.Databases.Settings.Readers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReader(
           this IServiceCollection services,
           ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseCommandReader),
                typeof(DatabaseCommandReader),
                lifetime);
        }
    }
}

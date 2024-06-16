using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Extensions.Configuration;
using Syrx.Extensions;

namespace Syrx.Commanders.Databases.Settings.Readers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /*
        public static IServiceCollection AddReader(
            this IServiceCollection services,
            CommanderOptions options,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.AddSingleton(options);

            return services.TryAddToServiceCollection(
                typeof(IDatabaseCommandReader),
                typeof(DatabaseCommandReader),
                lifetime);
        }
        */
    }
}

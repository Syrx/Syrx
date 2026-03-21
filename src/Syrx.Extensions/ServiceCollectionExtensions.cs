using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Syrx.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers Syrx services in the <see cref="IServiceCollection"/> using the provided builder configuration.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register Syrx services into.</param>
        /// <param name="factory">A delegate used to configure the <see cref="SyrxBuilder"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="factory"/> is null.</exception>
        public static IServiceCollection UseSyrx(
            this IServiceCollection services,
            Action<SyrxBuilder> factory
        )
        {
            // validation.... 

            var builder = new SyrxBuilder(services);
            factory(builder);
            return services;
        }

        private static IServiceCollection TryAddToServiceCollection(
            this IServiceCollection services,
            ServiceDescriptor descriptor)
        {
            services.TryAdd(descriptor);
            return services;
        }

        /// <summary>
        /// Attempts to add a service descriptor to the <see cref="IServiceCollection"/> if the service type has not already been registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the service into.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type to use for the service.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> for the service. Defaults to <see cref="ServiceLifetime.Transient"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection TryAddToServiceCollection(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                new ServiceDescriptor(
                    serviceType,
                    implementationType,
                    lifetime));
        }

        /// <summary>
        /// Attempts to register a singleton service instance in the <see cref="IServiceCollection"/> if the service type has not already been registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register the service into.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="instance">The singleton instance to register.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection TryAddToServiceCollection(
            this IServiceCollection services,
            Type serviceType,
            object instance)
        {
            return services.TryAddToServiceCollection(new ServiceDescriptor(serviceType, instance));
        }

    }
}

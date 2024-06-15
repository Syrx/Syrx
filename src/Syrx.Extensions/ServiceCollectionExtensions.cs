using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Syrx.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseSyrx(
            this IServiceCollection services,
            Action<SyrxOptionsBuilder> builder = null
        )
        {
            var optionsBuilder = new SyrxOptionsBuilder(services);
            builder?.Invoke(optionsBuilder);
            return services;
        }

        private static IServiceCollection TryAddToServiceCollection(
            this IServiceCollection services,
            ServiceDescriptor descriptor)
        {
            services.TryAdd(descriptor);
            return services;
        }

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

        public static IServiceCollection TryAddToServiceCollection(
            this IServiceCollection services,
            Type serviceType,
            object instance)
        {
            return services.TryAddToServiceCollection(new ServiceDescriptor(serviceType, instance));
        }

    }
}


namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSettings(
         this IServiceCollection services,
         IDatabaseCommanderSettings settings)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseCommanderSettings),
                settings);
        }

    }
}

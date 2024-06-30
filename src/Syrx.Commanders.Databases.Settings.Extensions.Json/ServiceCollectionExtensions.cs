using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static Syrx.Validation.Contract;

namespace Syrx.Commanders.Databases.Settings.Extensions.Json
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSyrxJsonFile(
            this IServiceCollection services,
            IConfigurationBuilder builder,
            string fileName)
        {
            Throw<ArgumentNullException>(builder != null, $"ConfigurationBuilder is null! Check bootstrap.");
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(fileName), nameof(fileName));

            builder?.AddJsonFile(fileName);
            return services;
        }
    }
}

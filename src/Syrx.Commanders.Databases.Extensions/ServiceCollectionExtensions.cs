﻿// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (20:51)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

using Syrx.Commanders.Databases.Extensions.Configuration;

namespace Syrx.Commanders.Databases.Extensions
{
    public static class ServiceCollectionExtensions
    {

        /*
        public static IServiceCollection AddDatabaseCommander(
            this IServiceCollection services,
            IDatabaseCommanderSettings settings,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services
                .AddSettings(settings)
                .AddReader(lifetime)
                .TryAddToServiceCollection(typeof(ICommander<>), typeof(DatabaseCommander<>), lifetime);

        }
        */

        /*
        public static IServiceCollection AddDatabaseCommander(
            this IServiceCollection services,
            CommanderOptions options,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services
                .AddSingleton(options)
                .AddReader(options, lifetime)
                .TryAddToServiceCollection(typeof(ICommander<>), typeof(DatabaseCommander<>), lifetime);

        }
        */
    }
}
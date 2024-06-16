// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (20:01)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

using Syrx.Commanders.Databases.Extensions.Configuration;

namespace Syrx.Commanders.Databases.Extensions
{
    public static class DatabaseCommanderOptionsBuilderExtensions
    {
        /*public static SyrxOptionsBuilder UseDatabaseCommander(
            this SyrxOptionsBuilder options,
            CommanderOptions settings)
        {
            options.ServiceCollection
                .AddDatabaseCommander(settings);
            return options;
        }*/


        /*
        public static SyrxOptionsBuilder UseDatabaseCommander(
            this SyrxOptionsBuilder options,
            IDatabaseCommanderSettings settings)
        {
            options.ServiceCollection
                .AddDatabaseCommander(settings);
            return options;
        }

        public static SyrxOptionsBuilder UseDatabaseCommander(
            this SyrxOptionsBuilder options,
            Func<IDatabaseCommanderSettings> factory)
        {
            var settings = factory();
            return options.UseDatabaseCommander(settings);
        }

        public static SyrxOptionsBuilder UseDatabaseCommander(
            this SyrxOptionsBuilder options,
            Func<IDatabaseCommanderSettings> factory,
            Func<DbProviderFactory> providerFactory)
        {
            return options.UseDatabaseCommander(factory);
        }
        */

    }
}
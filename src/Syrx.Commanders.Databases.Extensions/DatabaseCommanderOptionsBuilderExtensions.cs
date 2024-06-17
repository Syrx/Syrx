// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (20:01)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Extensions
{
    public static class DatabaseCommanderOptionsBuilderExtensions
    {

        public static SyrxOptionsBuilder UseDatabaseCommander(
            this SyrxOptionsBuilder options
            )
        {
            options.ServiceCollection
                .AddDatabaseCommander();
            return options;
        }
    }
}
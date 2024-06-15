// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (17:15)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Options
{
    public static class DatabaseCommanderSettingsOptionsBuilder
    {
        public static DatabaseCommanderSettings Build(Action<DatabaseCommanderSettingsOptions> builder)
        {
            var result = new DatabaseCommanderSettingsOptions();
            builder?.Invoke(result);
            return result.Build();
        }

    }
}
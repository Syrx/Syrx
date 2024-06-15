// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (17:15)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Options
{
    public static class DatabaseCommandSettingOptionsBuilderExtensions
    {
        public static DatabaseCommandSettingOptionsBuilder AddCommand(Action<DatabaseCommandSettingOptionsBuilder> builder)
        {
            var optionsBuilder = new DatabaseCommandSettingOptionsBuilder();
            builder?.Invoke(optionsBuilder);
            optionsBuilder.Build();
            return optionsBuilder;
        }
    }
}
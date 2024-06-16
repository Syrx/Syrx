
using Syrx.Commanders.Databases.Extensions.Configuration;

namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class OptionsConverters
    {
        // temporary type conversion while we transition

        public static DatabaseCommanderSettings ToSettings(this CommanderOptions options)
        {
            return new DatabaseCommanderSettings(
                options.Namespaces.Select(x => x.ToNamespaceSetting()),
                options?.Connections.Select(x => x.ToConnectionStringSetting()));
        }

        public static DatabaseCommandNamespaceSetting ToNamespaceSetting(this NamespaceSettingOptions options)
        {
            return new DatabaseCommandNamespaceSetting(options.Namespace, options.Types.Select(x => x.ToTypeSetting()));
        }

        public static DatabaseCommandTypeSetting ToTypeSetting(this TypeSettingOptions options)
        {
            return new DatabaseCommandTypeSetting(
                options.Name,
                options.Commands.ToDictionary(x => x.Key, x => x.Value.ToCommandSettings()));
        }

        public static DatabaseCommandSetting ToCommandSettings(this CommandSettingOptions options)
        {
            return new DatabaseCommandSetting(options.ConnectionAlias,
                options.CommandText,
                options.CommandType,
                options.CommandTimeout,
                options.Split,
                options.Flags.ToFlagSetting(),
                options.IsolationLevel);
        }

        public static Settings.CommandFlagSetting ToFlagSetting(this Syrx.Commanders.Databases.Extensions.Configuration.CommandFlagSetting setting)
        {
            var value = (int) setting;
            return (Settings.CommandFlagSetting) value;

        }

        public static Settings.ConnectionStringSetting ToConnectionStringSetting(this ConnectionStringSettingOptions options)
        {
            return new ConnectionStringSetting(options.Alias, options.ConnectionString);
        }
    }
}

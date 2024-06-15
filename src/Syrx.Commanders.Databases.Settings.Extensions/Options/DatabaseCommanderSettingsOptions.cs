// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (17:15)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Options
{
    public class DatabaseCommanderSettingsOptions
    {
        private readonly List<ConnectionStringSetting> _connectionStringSettings;
        private readonly List<DatabaseCommandSettingOptionsBuilder> _options;
        private readonly Dictionary<string, DatabaseCommanderSettings> _fileImports;

        public DatabaseCommanderSettingsOptions()
        {
            _connectionStringSettings = new List<ConnectionStringSetting>();
            _options = new List<DatabaseCommandSettingOptionsBuilder>();
            _fileImports = new Dictionary<string, DatabaseCommanderSettings>();
        }

        public DatabaseCommanderSettingsOptions AddConnectionString(string alias, string connectionString)
        {
            var setting = new ConnectionStringSetting(alias, connectionString);
            _connectionStringSettings.Add(setting);
            return this;
        }

        public DatabaseCommanderSettingsOptions AddCommand(Action<DatabaseCommandSettingOptionsBuilder> builder)
        {
            var options = DatabaseCommandSettingOptionsBuilderExtensions.AddCommand(builder);
            _options.Add(options);

            return this;
        }

        internal DatabaseCommanderSettings Build()
        {
            // this isn't at all fugly. /s
            var namespaceSettings = _options
                .GroupBy(a => a.Type.Namespace)
                .Select(b => new DatabaseCommandNamespaceSetting(b.Key,
                    _options.Where(f => f.Type.Namespace == b.Key)
                        .GroupBy(t => t.Type.FullName)
                        .Select(y => new DatabaseCommandTypeSetting(y.Key,
                            _options.Where(x => x.Type.FullName == y.Key)
                                .ToDictionary(a => a.Name, a => a.Build())
                        ))));

            var importedNamespaces = _fileImports.Values.SelectMany(x => x.Namespaces);
            var importedConnectionStrings = _fileImports.Values.SelectMany(x => x.Connections);

            var ns = namespaceSettings.Union(importedNamespaces).Cast<DatabaseCommandNamespaceSetting>();
            var connectionSrings = _connectionStringSettings.Union(importedConnectionStrings);

            return new DatabaseCommanderSettings(ns, connectionSrings);

        }
    }


}
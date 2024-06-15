//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Settings
{
    public record DatabaseCommanderSettings : IDatabaseCommanderSettings
    {
        public IEnumerable<INamespaceSetting<DatabaseCommandSetting>> Namespaces { get; init; }
        public IEnumerable<ConnectionStringSetting> Connections { get; init; }

        public DatabaseCommanderSettings(
            IEnumerable<DatabaseCommandNamespaceSetting> namespaces,
            IEnumerable<ConnectionStringSetting> connections = null)
        {
            Throw<ArgumentNullException>(namespaces != null, nameof(namespaces));
            var namespaceSettings = namespaces as DatabaseCommandNamespaceSetting[] ?? namespaces.ToArray();
            Throw<ArgumentException>(namespaceSettings.Any(), Messages.EmptyNamespaceSettingsList);
            Namespaces = (IEnumerable<INamespaceSetting<DatabaseCommandSetting>>?) namespaceSettings;

            if (connections != null)
            {
                var connectionStringSettings = connections as ConnectionStringSetting[] ?? connections.ToArray();
                Throw<ArgumentException>(connectionStringSettings.Any(), Messages.EmptyConnectionStringSettingsList);
                Connections = connectionStringSettings;
            }
        }

        private static class Messages
        {
            internal const string EmptyConnectionStringSettingsList =
                "At least 1 ConnectionStringSetting was expected.";

            internal const string EmptyNamespaceSettingsList =
                    "At least 1 DatabaseCommandNamespaceSetting was expected to be passed to the DatabaseCommanderSettings constructor."
                ;
        }
    }
}
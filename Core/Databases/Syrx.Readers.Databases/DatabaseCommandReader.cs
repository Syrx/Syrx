//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using System;
using System.Linq;
using Syrx.Settings;
using Syrx.Settings.Databases;
using static Syrx.Validation.Contract;
// ReSharper disable PossibleNullReferenceException

namespace Syrx.Readers.Databases
{
    public class DatabaseCommandReader : IDatabaseCommandReader
    {
        private readonly IDatabaseCommanderSettings _settings;

        public DatabaseCommandReader(IDatabaseCommanderSettings settings)
        {
            Require<ArgumentNullException>(settings != null, "{0}. No settings were passed to DatabaseCommandReader.",
                nameof(settings));
            _settings = settings;
        }

        public DatabaseCommandSetting GetCommand(Type type, string key)
        {
            Require<ArgumentNullException>(type != null, nameof(type));
            Require<ArgumentNullException>(!string.IsNullOrWhiteSpace(key), nameof(key));

            var namespaceSetting = GetNamespaceSetting(type);
            Require<NullReferenceException>(
                namespaceSetting != null,
                ErrorMessages.NoNamespaceSettingForType,
                type.FullName);

            var typeSetting = GetTypeSetting(namespaceSetting, type);
            Require<NullReferenceException>(typeSetting != null,
                ErrorMessages.NoTypeSettingAndNoPathInNamespace,
                type.FullName,
                namespaceSetting.Namespace);

            var commandSetting = GetCommandSetting(typeSetting, key);
            Require<NullReferenceException>(commandSetting != null,
                ErrorMessages.NoCommandSetting,
                key,
                typeSetting.Name);

            return commandSetting;
        }

        private INamespaceSetting<DatabaseCommandSetting> GetNamespaceSetting(Type type)
        {
            //return _settings.Namespaces.SingleOrDefault(x => x.Namespace == type.Namespace);
            var result = _settings.Namespaces.SingleOrDefault(x => x.Namespace == type.Namespace);

            if (result != null)
            {
                return result;
            }

            // loop backwards till we find a match
            foreach (var setting in _settings.Namespaces)
            {
                var typeNamespace = type.Namespace;
                var periods = typeNamespace.Count(x => x == '.');

                for (var i = periods; i > 0; i--)
                {
                    if (setting.Namespace == typeNamespace)
                    {
                        return setting;
                    }
                    typeNamespace = typeNamespace.Substring(0, typeNamespace.LastIndexOf('.'));
                }
            }

            // if we reach here, we've got nothing.
            return null;
        }

        private ITypeSetting<DatabaseCommandSetting> GetTypeSetting(
            INamespaceSetting<DatabaseCommandSetting> namespaceSetting,
            Type type)
        {
            // check if it has a type setting.
            return namespaceSetting.Types?.SingleOrDefault(x => x.Name == type.FullName);
        }

        private DatabaseCommandSetting GetCommandSetting(            
            ITypeSetting<DatabaseCommandSetting> typeSetting,
            string key)
        {
            return typeSetting.Commands?.SingleOrDefault(x => x.Key == key).Value;            
        }


        private static class ErrorMessages
        {
            internal const string NoNamespaceSettingForType =
                @"'{0}' does not belong to any NamespaceSetting.
Please check settings.";

            internal const string NoTypeSettingAndNoPathInNamespace =
                    @"The type '{0}' has no entry in the type settings of namespace '{1}'. Please add a type setting entry to the namespace setting."
                ;

            internal const string NoCommandSetting =
                    @"The command setting '{0}' has no entry for the type setting '{1}'. Please add a command setting entry to the type setting."
                ;
        }
    }
}
//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Microsoft.Extensions.Options;
using Syrx.Commanders.Databases.Extensions.Configuration;

namespace Syrx.Commanders.Databases.Settings.Readers
{
    public class DatabaseCommandReader : IDatabaseCommandReader
    {
        private readonly IDatabaseCommanderSettings _settings;
        private readonly ICommanderOptions _options;

        //public DatabaseCommandReader(
        //    IOptions<CommanderOptions> options
        //    )
        //{
        //    Throw<ArgumentNullException>(options != null, "{0}. No settings were passed to DatabaseCommandReader.", nameof(options));
        //    _options = options.Value;
            
        //}

        public DatabaseCommandReader(ICommanderOptions options)
        {
            Throw<ArgumentNullException>(options != null, "{0}. No settings were passed to DatabaseCommandReader.", nameof(options));
            _options = options;
        }

        //public virtual DatabaseCommandSetting GetCommand(Type type, string key)
        //{
        //    Throw<ArgumentNullException>(type != null, nameof(type));
        //    Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(key), nameof(key));

        //    /*
        //    // returns what I want, but without the validation 
        //    return _settings.Namespaces
        //       .SelectMany(x => x.Types.Where(y => y.Name == type.FullName))
        //       .SelectMany(a => a.Commands)
        //       .SingleOrDefault(b => b.Key == key).Value;
        //    */

        //    var namespaceSetting = GetNamespaceSetting(type);
        //    Throw<NullReferenceException>(
        //        namespaceSetting != null,
        //        ErrorMessages.NoNamespaceSettingForType,
        //        type.FullName);

        //    var typeSetting = GetTypeSetting(namespaceSetting, type);
        //    Throw<NullReferenceException>(typeSetting != null,
        //        ErrorMessages.NoTypeSetting,
        //        type.FullName,
        //        namespaceSetting.Namespace);

        //    var commandSetting = GetCommandSetting(typeSetting, key);
        //    Throw<NullReferenceException>(commandSetting != null,
        //        ErrorMessages.NoCommandSetting,
        //        key,
        //        typeSetting.Name);

        //    return commandSetting;
        //}

        //private INamespaceSetting<DatabaseCommandSetting> GetNamespaceSetting(Type type)
        //{
        //    var result = _settings.Namespaces.SingleOrDefault(x => x.Namespace == type.Namespace);

        //    if (result != null)
        //    {
        //        return result;
        //    }


        //    // loop backwards till we find a match
        //    foreach (var setting in _settings.Namespaces)
        //    {
        //        var typeNamespace = type.Namespace;
        //        var periods = type.FullName.Count(x => x == '.');

        //        for (var i = periods; i > 0; i--)
        //        {
        //            if (setting.Namespace == typeNamespace)
        //            {
        //                return setting;
        //            }

        //            if (i != 1)
        //            {
        //                typeNamespace = typeNamespace.Substring(0, typeNamespace.LastIndexOf('.'));
        //            }
        //        }
        //    }

        //    // if we reach here, we've got nothing.
        //    return null;
        //}

        //private static ITypeSetting<DatabaseCommandSetting>? GetTypeSetting(
        //    INamespaceSetting<DatabaseCommandSetting> namespaceSetting,
        //    Type type) =>
        //    // check if it has a type setting.
        //    namespaceSetting.Types?.SingleOrDefault(x => x.Name == type.FullName);

        //private DatabaseCommandSetting GetCommandSetting(
        //    ITypeSetting<DatabaseCommandSetting> typeSetting,
        //    string key)
        //{
        //    return typeSetting.Commands?.SingleOrDefault(x => x.Key == key).Value;
        //}

        public CommandSettingOptions GetCommand(Type type, string key)
        {            
            var result = _options.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == type.FullName))
                .SelectMany(z => z.Commands)
                .SingleOrDefault(f => f.Key == key).Value;

            Throw<NullReferenceException>(result != null,
                ErrorMessages.NoCommandSetting, key, type.Name);

            return result;
        }

        private static class ErrorMessages
        {
            internal const string NoNamespaceSettingForType =
                @"'{0}' does not belong to any NamespaceSetting.
Please check settings.";

            internal const string NoTypeSetting =
                    @"The type '{0}' has no entry in the type settings of namespace '{1}'. Please add a type setting entry to the namespace setting.";

            internal const string NoCommandSetting =
                    @"The command setting '{0}' has no entry for the type setting '{1}'. Please add a command setting entry to the type setting.";
        }
    }
}
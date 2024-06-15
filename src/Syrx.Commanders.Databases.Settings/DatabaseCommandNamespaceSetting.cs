//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Settings
{
    public record DatabaseCommandNamespaceSetting : IDatabaseCommandNamespaceSetting
    {
        public string Namespace { get; init; }
        public IEnumerable<ITypeSetting<DatabaseCommandSetting>> Types { get; init; }

        public DatabaseCommandNamespaceSetting(string @namespace,
            IEnumerable<DatabaseCommandTypeSetting> types)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(@namespace), Messages.NullName,
                nameof(@namespace));
            Throw<ArgumentNullException>(types != null, Messages.NullCommandsPassed, nameof(types),
                @namespace);
            var typeSettings = types as DatabaseCommandTypeSetting[] ?? types.ToArray();
            Throw<ArgumentException>(typeSettings.Any(), Messages.EmptyListPassed, @namespace);
            Namespace = @namespace;
            Types = (IEnumerable<ITypeSetting<DatabaseCommandSetting>>?) typeSettings;
        }

        private static class Messages
        {
            internal const string NullName =
                    "{0}. All namespace entries must have a name to be valid. Please check settings for an empty namespace namespace."
                ;

            internal const string NullCommandsPassed
                = "{0}. The types collection for '{1}' is null. Please check settings.";

            internal const string EmptyListPassed
                = "'{0}' must have at least 1 Type setting to be valid. Please check settings.";
        }
    }
}
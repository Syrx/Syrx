//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Settings
{
    public record DatabaseCommandTypeSetting : IDatabaseCommandTypeSetting
    {
        public string Name { get; init; }

        public IDictionary<string, DatabaseCommandSetting> Commands { get; init; }

        public DatabaseCommandTypeSetting(string name, IDictionary<string, DatabaseCommandSetting> commands)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), Messages.NullEmptyWhitespaceName, nameof(name));
            Throw<ArgumentNullException>(commands != null, Messages.NullCommandsPassed, nameof(commands), name);
            Throw<ArgumentException>(commands.Any(), Messages.EmptyDictionaryPassed, name);

            Name = name;
            Commands = commands;
        }

        private static class Messages
        {
            internal const string NullEmptyWhitespaceName
                = @"{0}. All type entries must have a name to be valid. Please check settings.";

            internal const string NullCommandsPassed
                = "{0}. The commands collection for '{1}' is null. Please check settings.";

            internal const string EmptyDictionaryPassed
                = "'{0}' must have at least 1 CommandSetting to be valid. Please check settings.";
        }
    }
}
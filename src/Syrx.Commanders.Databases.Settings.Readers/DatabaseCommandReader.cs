//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Readers
{
    public class DatabaseCommandReader : IDatabaseCommandReader
    {
        private readonly ICommanderSettings _settings;
                
        public DatabaseCommandReader(ICommanderSettings settings)
        {
            Throw<ArgumentNullException>(settings != null, "{0}. No settings were passed to DatabaseCommandReader.", nameof(settings));
            _settings = settings!;
        }
                
        public CommandSetting GetCommand(Type type, string key)
        {
            Throw<ArgumentNullException>(type != null, nameof(type));
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(key), nameof(key));

            var result = _settings.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == type!.FullName))
                .SelectMany(z => z.Commands)
                .SingleOrDefault(f => f.Key == key).Value;

            Throw<NullReferenceException>(result != null,
                ErrorMessages.NoCommandSetting, key, type!.FullName);

            return result!;
        }

        private static class ErrorMessages
        {
            internal const string NoCommandSetting =
                    @"The command setting '{0}' has no entry for the type setting '{1}'. Please add a command setting entry to the type setting.";
        }
    }
}
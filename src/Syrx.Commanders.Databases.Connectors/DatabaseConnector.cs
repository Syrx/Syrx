//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Connectors
{
    public class DatabaseConnector : IDatabaseConnector
    {
        private readonly Func<DbProviderFactory> _providerPredicate;
        private readonly ICommanderSettings _settings;

        public DatabaseConnector(
            ICommanderSettings settings,
            Func<DbProviderFactory> providerPredicate
        )
        {
            Throw<ArgumentNullException>(settings != null, nameof(settings));
            Throw<ArgumentNullException>(providerPredicate != null, nameof(providerPredicate));

            _settings = settings!;
            _providerPredicate = providerPredicate!;
        }

        public IDbConnection CreateConnection(CommandSetting setting)
        {
            Throw<ArgumentNullException>(setting != null, nameof(setting));

            var connectionStringSetting = _settings?.Connections?.SingleOrDefault(x => x.Alias == setting?.ConnectionAlias);
            Throw<NullReferenceException>(connectionStringSetting != null, Messages.NoAliasedConnection, setting?.ConnectionAlias);

            var connection = _providerPredicate.Invoke().CreateConnection();
            Throw<NullReferenceException>(connection != null, Messages.NoConnectionCreated, setting?.ConnectionAlias);

            // assign the connection and return
            connection!.ConnectionString = connectionStringSetting?.ConnectionString;
            return connection;
        }
        
        private static class Messages
        {
            internal const string NoAliasedConnection =
                "There is no connection with the alias '{0}' in the settings. Please check settings.";

            internal const string NoConnectionCreated =
                "The provider predicate did not return a connection for the aliased connection '{0}'.";
        }
    }



}
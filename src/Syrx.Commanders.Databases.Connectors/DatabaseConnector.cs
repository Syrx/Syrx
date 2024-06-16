//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Commanders.Databases.Extensions.Configuration;
using Syrx.Settings;

namespace Syrx.Commanders.Databases.Connectors
{
    public class DatabaseConnector : IDatabaseConnector
    {
        private readonly Func<DbProviderFactory> _providerPredicate;
        //private readonly IDatabaseCommanderSettings _settings;
        private readonly ICommanderOptions _options;

        public DatabaseConnector(
            //IDatabaseCommanderSettings settings,
            ICommanderOptions settings,
            Func<DbProviderFactory> providerPredicate
        )
        {
            //Throw<ArgumentNullException>(settings != null, nameof(settings));
            Throw<ArgumentNullException>(settings != null, nameof(settings));
            Throw<ArgumentNullException>(providerPredicate != null, nameof(providerPredicate));

            //_settings = settings;
            _options = settings;
            _providerPredicate = providerPredicate;
        }

        public IDbConnection CreateConnection(CommandSettingOptions options)
        {
            Throw<ArgumentNullException>(options != null, nameof(options));

            var connectionStringSetting = _options.Connections.SingleOrDefault(x => x.Alias == options.ConnectionAlias);
            Throw<NullReferenceException>(connectionStringSetting != null, Messages.NoAliasedConnection, options.ConnectionAlias);

            var connection = _providerPredicate.Invoke().CreateConnection();
            Throw<NullReferenceException>(connection != null, Messages.NoConnectionCreated, options.ConnectionAlias);

            // assign the connection and return
            connection.ConnectionString = connectionStringSetting.ConnectionString;
            return connection;
        }

        /*
        public virtual IDbConnection CreateConnection(DatabaseCommandSetting commandSetting)
        {
            // pre-conditions. 
            Throw<ArgumentNullException>(commandSetting != null, nameof(commandSetting));

            // get the connection string setting from the root connections. 
            var connectionStringSetting =
                _settings.Connections.SingleOrDefault(x => x.Alias == commandSetting.ConnectionAlias);
            Throw<NullReferenceException>(connectionStringSetting != null, Messages.NoAliasedConnection,
                commandSetting.ConnectionAlias);

            // invoke the provider predicate to return a connection. 
            var connection = _providerPredicate.Invoke().CreateConnection();
            Throw<NullReferenceException>(connection != null, Messages.NoConnectionCreated,
                commandSetting.ConnectionAlias);

            // assign the connection and return
            connection.ConnectionString = connectionStringSetting.ConnectionString;
            return connection;
        }
        */

        private static class Messages
        {
            internal const string NoAliasedConnection =
                "There is no connection with the alias '{0}' in the settings. Please check settings.";

            internal const string NoConnectionCreated =
                "The provider predicate did not return a connection for the aliased connection '{0}'.";
        }
    }



}
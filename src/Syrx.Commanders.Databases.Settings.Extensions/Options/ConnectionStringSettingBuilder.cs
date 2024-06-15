// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.22 (18:22)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Options
{
    public class ConnectionStringSettingBuilder
    {
        private readonly List<ConnectionStringSetting> _connectionStringSettings;
        public IEnumerable<ConnectionStringSetting> ConnectionStringSettings => _connectionStringSettings;

        public ConnectionStringSettingBuilder(IEnumerable<ConnectionStringSetting> connectionStringSettings = null)
        {
            _connectionStringSettings = new List<ConnectionStringSetting>();
            if (connectionStringSettings != null)
            {
                // use the AddConnectionString method to validate the contents on the way in. 
                connectionStringSettings.
                    ToList().
                    ForEach(x => AddConnectionString(x.Alias, x.ConnectionString));
            }
        }

        public ConnectionStringSettingBuilder AddConnectionString(string alias, string connectionString)
        {
            // first check if alias already exists
            //  if so, check connection string matches
            //    if so return
            //    else set warning
            //  else add new connection string            
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));

            var exists = _connectionStringSettings.SingleOrDefault(x => x.Alias == alias);

            if (exists == null)
            {
                _connectionStringSettings.Add(new ConnectionStringSetting(alias, connectionString));
             
            }

            if (exists != null)
            {
                // seems counter-intuitive. 
                Throw<ArgumentException>(
                    exists.ConnectionString == connectionString,
                    @$"The alias '{alias}' is already assigned to a different connection string. 
Current connection string: {exists.ConnectionString}
New connection string: {connectionString}");
                                
            }

            return this;

        }

        public static ConnectionStringSettingBuilder AddConnectionString<TType>(string connectionString)
        {
            var alias = typeof(TType).FullName;
            var builder = new ConnectionStringSettingBuilder();
            return builder.AddConnectionString(alias, connectionString);
        }

    }
}
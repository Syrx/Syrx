namespace Syrx.Commanders.Databases.Extensions.Configuration.Builders
{
    public class ConnectionStringOptionsBuilder
    {
        private string _alias;
        private string _connectionString;
        public ConnectionStringOptionsBuilder()
        {
            _alias = string.Empty;
            _connectionString = string.Empty;
        }

        public ConnectionStringOptionsBuilder UseAlias(string alias)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));
            _alias = alias;
            return this;
        }

        public ConnectionStringOptionsBuilder UseConnectionString(string connectionString)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionString), nameof(connectionString));
            _connectionString += connectionString;
            return this;
        }

        public ConnectionStringOptionsBuilder UseConnectionString<TType>(string connectionString)
        {
            var alias = typeof(TType).FullName;            
            return UseConnectionString(alias, connectionString);
        }

        public ConnectionStringOptionsBuilder UseConnectionString(string alias, string connectionString)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionString), nameof(connectionString));

            _alias = alias!;
            _connectionString = connectionString;
            return this;
        }
    
        public ConnectionStringSettingOptions Build()
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_alias), nameof(_alias));
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_connectionString), nameof(_connectionString));

            return new ConnectionStringSettingOptions { Alias = _alias, ConnectionString = _connectionString };
        }
    }

}
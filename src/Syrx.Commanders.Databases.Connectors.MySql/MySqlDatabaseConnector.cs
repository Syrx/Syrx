using MySqlConnector;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Connectors.MySql
{
    public class MySqlDatabaseConnector : DatabaseConnector
    {
        public MySqlDatabaseConnector(ICommanderSettings settings) 
            : base(settings, () => MySqlConnectorFactory.Instance)
        {
        }    
    }
}

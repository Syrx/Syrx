using MySqlConnector;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Connectors.MySql
{
    public class MySqlDatabaseConnector(ICommanderSettings settings) : DatabaseConnector(settings, () => MySqlConnectorFactory.Instance)
    {
    }
}

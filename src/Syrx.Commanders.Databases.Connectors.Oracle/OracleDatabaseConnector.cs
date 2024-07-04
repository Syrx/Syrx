using Oracle.ManagedDataAccess.Client;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Connectors.Oracle
{
    public class OracleDatabaseConnector(ICommanderSettings settings) : DatabaseConnector(settings, () => OracleClientFactory.Instance)
    {
    }
}

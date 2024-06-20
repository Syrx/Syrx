using Oracle.ManagedDataAccess.Client;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Connectors.Oracle
{
    public class OracleDatabaseConnector : DatabaseConnector
    {
        public OracleDatabaseConnector(ICommanderSettings settings)
            : base(settings, () => OracleClientFactory.Instance)
        {
        }
    }
}

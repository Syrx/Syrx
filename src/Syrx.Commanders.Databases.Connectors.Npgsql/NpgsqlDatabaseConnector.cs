using Npgsql;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Connectors.Npgsql
{
    public class NpgsqlDatabaseConnector : DatabaseConnector
    {
        public NpgsqlDatabaseConnector(ICommanderSettings settings)
            : base(settings, () => NpgsqlFactory.Instance)
        {
        }
    }
}

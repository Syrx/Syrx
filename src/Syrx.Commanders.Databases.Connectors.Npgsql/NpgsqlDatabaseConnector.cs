using Npgsql;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Connectors.Npgsql
{
    public class NpgsqlDatabaseConnector(ICommanderSettings settings) : DatabaseConnector(settings, () => NpgsqlFactory.Instance)
    {
    }
}

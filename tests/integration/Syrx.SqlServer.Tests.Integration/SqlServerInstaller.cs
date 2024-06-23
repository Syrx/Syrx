using Microsoft.Extensions.DependencyInjection;
using Syrx.Extensions;
using Syrx.SqlServer.Tests.Integration.Setup;

namespace Syrx.SqlServer.Tests.Integration
{
    public class SqlServerInstaller 
    {
        public SyrxBuilder SyrxBuilder { get; }
        public IServiceProvider Provider { get; }

        public SqlServerInstaller(string connectionString)
        {
            var services = new ServiceCollection();
            var builder = new SyrxBuilder(services);
            SyrxBuilder = builder.SetupSqlServer(connectionString);
            
            Provider = services.BuildServiceProvider();
            var commander = Provider.GetService<ICommander<DatabaseBuilder>>();
            var database = new DatabaseBuilder(commander);
            database.Build();
        }
    }
}

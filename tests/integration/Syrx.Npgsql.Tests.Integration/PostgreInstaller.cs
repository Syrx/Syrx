namespace Syrx.Npgsql.Tests.Integration
{
    public class PostgreInstaller
    {
        public SyrxBuilder SyrxBuilder { get; }
        public IServiceProvider Provider { get; }

        public PostgreInstaller(string connectionString)
        {
            var services = new ServiceCollection();
            var builder = new SyrxBuilder(services);
            SyrxBuilder = builder.SetupPostgres(connectionString);

            Provider = services.BuildServiceProvider();
            var commander = Provider.GetService<ICommander<DatabaseBuilder>>();
            var database = new DatabaseBuilder(commander);
            database.Build();
        }
    }
}

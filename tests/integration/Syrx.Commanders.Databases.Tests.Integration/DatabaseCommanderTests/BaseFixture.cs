using Syrx.Commanders.Databases.Tests.Integration.Setup;
using Testcontainers.MsSql;

namespace Syrx.Commanders.Databases.Tests.Integration.DatabaseCommanderTests
{

    public class BaseFixture : IAsyncLifetime
    {
        public const string QueryFixtureCollectionDefinition = "FixtureCollectionDefinition";
        public const string ExecuteFixtureCollectionDefinition = "ExecuteFixtureCollectionDefinition";
        private readonly IServiceProvider _services;
        private readonly MsSqlContainer _container = new MsSqlBuilder().Build();
        public string ConnectionString => _container.GetConnectionString();

        public BaseFixture()
        {
            var installer = Installer.Install();
            _services = installer;
            //Setup();

            //_container = new MsSqlBuilder().Build();
        }

        public async Task DisposeAsync()
        {
            await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>();
        }

        /*
        internal protected void Setup(string name = "Syrx")
        {
            Console.WriteLine($"Running environment setup checks against databse '{name}'.");
            var commander = GetCommander<DatabaseBuilder>();

            var builder = DatabaseBuilder
                .Initialize(commander)
                .CreateDatabase(name)
                .DropTableCreatorProcedure()
                .CreateTableCreatorProcedure()
                .CreateTable("poco")
                .CreateTable("identity_tester")
                .CreateTable("bulk_insert")
                .CreateTable("distributed_transaction")
                .DropIdentityTesterProcedure()
                .CreateIdentityTesterProcedure()
                .DropBulkInsertProcedure()
                .CreateBulkInsertProcedure()
                .DropBulkInsertAndReturnProcedure()
                .CreateBulkInsertAndReturnProcedure()
                .DropTableClearingProcedure()
                .CreateTableClearingProcedure()
                .ClearTable()
                .Populate();

            Console.WriteLine("Finished setting database up. Let the games begin!");
        }
        */

    }

    [CollectionDefinition(BaseFixture.QueryFixtureCollectionDefinition)]
    public class QueryFixtureCollectionDefinition : ICollectionFixture<QueryFixture> { }

    [CollectionDefinition(BaseFixture.ExecuteFixtureCollectionDefinition)]
    public class ExecuteFixtureCollectionDefinition : ICollectionFixture<ExecuteFixture> { }
}

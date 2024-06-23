
namespace Syrx.Commanders.Databases.Tests.Integration.DatabaseCommanderTests
{

    public interface IFixture<TDatabaseProvider>
    {
        IServiceProvider ServiceProvider { get; }
        IContrainerWrapper Contrainer { get; }
    }

    

    public interface IContrainerWrapper
    {
        Task StartAsync();
        Task StopAsync();
        string ConnectionString { get; }
    }

    public class BaseFixture: IAsyncLifetime
    {

        public IServiceProvider ServiceProvider { get; }
        public IContrainerWrapper Contrainer { get; }


        public BaseFixture(Func<IServiceProvider> providerFactory, Func<IContrainerWrapper> containerFactory)
        {
            ServiceProvider = providerFactory();
            Contrainer = containerFactory();
        }

        public ICommander<TRepository> GetCommander<TRepository>() => ServiceProvider.GetService<ICommander<TRepository>>();

        public async Task InitializeAsync()
        {
            await Contrainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await Contrainer.StopAsync();
        }
    }

    //public class BaseFixture : IAsyncLifetime
    //{

    //    public IServiceProvider ServiceProvider { get; }

    //    //public BaseFixture(IServiceProvider services)
    //    //{
    //    //    Services = services;
    //    //}

    //    public BaseFixture(Func<IServiceProvider> providerFactory)
    //    {
    //        ServiceProvider = providerFactory();
    //    }

    //    public ICommander<TRepository> GetCommander<TRepository>() => ServiceProvider.GetService<ICommander<TRepository>>();

    //    //private IServiceProvider _services;



    //    //private readonly MsSqlContainer _container = new MsSqlBuilder()
    //    //    .WithName("syrx-sqlserver")             
    //    //    .WithReuse(true)
    //    //    .Build();

    //    //public async Task DisposeAsync()
    //    //{
    //    //    //await _container.StopAsync();
    //    //}

    //    //public async Task InitializeAsync()
    //    //{
    //    //    if (_container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
    //    //    {
    //    //        await _container.StartAsync();
    //    //        var connectionString = _container.GetConnectionString();
    //    //        var installer = new Installer();

    //    //        _services = installer.Install(new ServiceCollection(), connectionString);

    //    //        Setup();
    //    //    }
    //    //}

    //    //internal protected ICommander<TRepository> GetCommander<TRepository>() => _services.GetService<ICommander<TRepository>>();

    //    /*
    //    internal protected void Setup(string name = "Syrx")
    //    {
    //        Console.WriteLine($"Running environment setup checks against databse '{name}'.");
    //        var commander = GetCommander<DatabaseBuilder>();

    //        var builder = DatabaseBuilder
    //            .Initialize(commander)
    //            .CreateDatabase(name)
    //            .DropTableCreatorProcedure()
    //            .CreateTableCreatorProcedure()
    //            .CreateTable("poco")
    //            .CreateTable("identity_tester")
    //            .CreateTable("bulk_insert")
    //            .CreateTable("distributed_transaction")
    //            .DropIdentityTesterProcedure()
    //            .CreateIdentityTesterProcedure()
    //            .DropBulkInsertProcedure()
    //            .CreateBulkInsertProcedure()
    //            .DropBulkInsertAndReturnProcedure()
    //            .CreateBulkInsertAndReturnProcedure()
    //            .DropTableClearingProcedure()
    //            .CreateTableClearingProcedure()
    //            .ClearTable()
    //            .Populate();

    //        Console.WriteLine("Finished setting database up. Let the games begin!");
    //    }
    //    */


    //}
}

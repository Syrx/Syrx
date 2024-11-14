using DotNet.Testcontainers.Builders;

namespace Syrx.Npgsql.Tests.Integration
{

    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            //.WithName("syrx-postgresql")
            .WithReuse(true)
            .Build();

        public async Task DisposeAsync()
        {
            await Task.Run(() => Console.WriteLine("Done"));
        }

        public async Task InitializeAsync()
        {
            if (_container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
            {
                await _container.StartAsync();
            }

            // line up
            var connectionString = _container.GetConnectionString();
            var installer = new PostgreInstaller(connectionString);
            _services = installer.Provider;

        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>()!;
        }

    }

}

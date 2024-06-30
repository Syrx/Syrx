namespace Syrx.Npgsql.Tests.Integration
{

    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
            .Build();

        public async Task DisposeAsync()
        {
            await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

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

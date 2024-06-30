namespace Syrx.MySql.Tests.Integration
{

    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly MySqlContainer _container = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .Build();

        public async Task DisposeAsync()
        {
           await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            // line up
            var connectionString = _container.GetConnectionString() + ";Allow User Variables=true";
            var installer = new MySqlInstaller(connectionString);
            _services = installer.Provider;

        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>()!;
        }

    }

}

using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Logging;

namespace Syrx.MySql.Tests.Integration
{

    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly MySqlContainer _container = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
            .WithName("syrx-mysql")
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

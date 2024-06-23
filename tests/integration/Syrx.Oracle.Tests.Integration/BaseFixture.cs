using Microsoft.Extensions.Logging;
using Serilog;

namespace Syrx.Oracle.Tests.Integration
{
    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private Microsoft.Extensions.Logging.ILogger _logger;
        private readonly OracleContainer _container;

        public BaseFixture()
        {
            //_logger = CreateLogger();
            _container = new OracleBuilder()
                .WithName("syrx-oracle")
                .WithReuse(true)
                //.WithLogger(_logger)
                .Build();
        }

        public async Task DisposeAsync()
        {
            //await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            if (_container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
            {
                await _container.StartAsync();
            }

            // line up
            var connectionString = _container.GetConnectionString();
            var installer = new OracleInstaller(connectionString);
            _services = installer.Provider;

        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>();
        }

    }
}

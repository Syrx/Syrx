using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Logging;

namespace Syrx.Oracle.Tests.Integration
{
    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly OracleContainer _container;

        public BaseFixture()
        {
            var strategy = Wait.ForWindowsContainer()
                .UntilMessageIsLogged("Completed: ALTER DATABASE OPEN", x =>
                {
                    // oracle container takes eternity to load. 
                    // 4 minutes is way too long. 
                    var interval = TimeSpan.FromMinutes(1);
                    var timeout = TimeSpan.FromMinutes(5);
                    x.WithInterval(interval)
                    .WithTimeout(timeout);

                });

            _container = new OracleBuilder()
                .WithWaitStrategy(strategy)
                .WithLogger(LoggerFactory.Create(x => x.AddConsole()).CreateLogger<BaseFixture>())
                .Build();

        }

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
            var installer = new OracleInstaller(connectionString);
            _services = installer.Provider;

        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>()!;
        }

    }

}

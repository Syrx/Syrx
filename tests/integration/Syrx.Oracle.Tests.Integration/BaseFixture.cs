using DotNet.Testcontainers.Builders;

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
                    var interval = TimeSpan.FromSeconds(10);
                    ushort retries = 3;
                    var timeout = TimeSpan.FromSeconds(90);
                    Console.WriteLine($"Starting wait with interval: { interval}, retries :{ retries }, timeout : {timeout}..."); 
                    x.WithInterval(interval)
                    .WithRetries(retries)
                    .WithTimeout(timeout);

                });
            
            _container = new OracleBuilder()
                .WithWaitStrategy(strategy)
                .Build();

        }

        public async Task DisposeAsync()
        {
            await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
         
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

using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Logging;

namespace Syrx.Oracle.Tests.Integration
{
    public class BaseFixture : IAsyncLifetime
    {
         private IServiceProvider _services;
        // private readonly OracleContainer _container;

        public BaseFixture()
        {
         
        }

        public async Task DisposeAsync()
        {
            await Task.Run(() => Console.WriteLine("Done"));
        }

        public async Task InitializeAsync()
        {
            //if (_container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
            //{
            //    await _container.StartAsync();
            //}

            //// line up
            //var connectionString = _container.GetConnectionString();
            //var installer = new OracleInstaller(connectionString);
            //_services = installer.Provider;

            await Task.CompletedTask;
        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>()!;
        }

    }

}

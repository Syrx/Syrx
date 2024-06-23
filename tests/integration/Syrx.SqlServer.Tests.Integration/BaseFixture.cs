﻿

namespace Syrx.SqlServer.Tests.Integration
{

    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly MsSqlContainer _container = new MsSqlBuilder()
            .WithName("syrx-sqlserver")
            .WithReuse(true)
            .Build();
        
        public async Task DisposeAsync()
        {
            await _container.StopAsync();
        }

        public async Task InitializeAsync()
        {
            if(_container.State != DotNet.Testcontainers.Containers.TestcontainersStates.Running)
            {
                await _container.StartAsync();
            }

            // line up
            var connectionString = _container.GetConnectionString();
            var installer = new SqlServerInstaller(connectionString);
            _services = installer.Provider;
            
        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>();
        }

    }
}
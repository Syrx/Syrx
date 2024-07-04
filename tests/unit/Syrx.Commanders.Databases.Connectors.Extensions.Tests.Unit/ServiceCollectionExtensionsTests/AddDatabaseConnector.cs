using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syrx.Commanders.Databases.Settings;
using Syrx.Tests.Extensions;
using System.Data;
using System.Data.Common;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.Extensions.Tests.Unit.ServiceCollectionExtensionsTests
{
    public class AddDatabaseConnector
    {
        private readonly IServiceCollection _services;

        public AddDatabaseConnector()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void RegistersCustomConnector()
        {
            _services.AddDatabaseConnector<IDatabaseConnector, AddDatabaseConnectorTestConnector>();
            var provider = _services.BuildServiceProvider();
            var result = provider.GetService<IDatabaseConnector>();
            NotNull(result);           
        }


        private class AddDatabaseConnectorTestConnector : IDatabaseConnector
        {
            public IDbConnection CreateConnection(CommandSetting options) 
                => throw new NotImplementedException("stub implementation for test only.");
        }

    }

    public class AddProvider
    {
        private readonly IServiceCollection _services;

        public AddProvider()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void NullProviderThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => _services.AddProvider(null));
            result.HasMessage("The DbProviderFactory delegate cannot be null. (Parameter 'providerFactory')");
        }

        [Fact]
        public void Successfully()
        {
            var connection = new Mock<DbConnection>();
            var dbProviderFactory = new Mock<DbProviderFactory>();
            dbProviderFactory.Setup(x => x.CreateConnection()).Returns(connection.Object);

            Func<DbProviderFactory> factory = () => dbProviderFactory.Object;
            var result = _services.AddProvider(factory);

            var provider = _services.BuildServiceProvider();
            var resolved = provider.GetService<Func<DbProviderFactory>>();
            NotNull(resolved);
            Same(factory, resolved);
        }
    }    
}

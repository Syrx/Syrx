using Microsoft.Extensions.DependencyInjection;
using Moq;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Extensions;
using Syrx.Tests.Extensions;
using System.Data.Common;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Connectors.Extensions.Tests.Unit.DatabaseConnectorExtensionsTests
{
    public class UseConnector : IClassFixture<DatabaseConnectorExtensionsFixture>
    {
        private readonly IServiceCollection _services;
        private DatabaseConnectorExtensionsFixture _fixture;

        public UseConnector(DatabaseConnectorExtensionsFixture fixture)
        {
            _services = new ServiceCollection();
            _fixture = fixture;
        }

        [Fact]
        public void WithFullSettings()
        {
            var connection = new Mock<DbConnection>();
            var dbProviderFactory = new Mock<DbProviderFactory>();
            dbProviderFactory.Setup(x => x.CreateConnection()).Returns(connection.Object);

            Func<DbProviderFactory> factory = () => dbProviderFactory.Object;

            var settings = _services.UseSyrx(
                a => a.UseConnector(factory,
                b => b.AddCommand(
                    c => c.ForType<UseConnector>(
                        d => d.ForMethod(
                            nameof(WithFullSettings), e => e
                            .UseConnectionAlias("test-alias")
                            .UseCommandText("test-command"))))));

            var provider = _services.BuildServiceProvider();
            var connector = provider.GetService<IDatabaseConnector>();
            NotNull(connector);
            var resolvedFactory = provider.GetService<Func<DbProviderFactory>>();
            NotNull(resolvedFactory);
            Same(factory, resolvedFactory);
        }


        [Fact]
        public void RegistersCustomProvider()
        {
            var connection = new Mock<DbConnection>();
            var dbProviderFactory = new Mock<DbProviderFactory>();
            dbProviderFactory.Setup(x => x.CreateConnection()).Returns(connection.Object);

            Func<DbProviderFactory> factory = () => dbProviderFactory.Object;

            var builder = new SyrxBuilder(_services);
            var settings = _fixture.SettingsDelegate;
            var result = builder.UseConnector(factory, settings);

            var provider = builder.ServiceCollection.BuildServiceProvider();
            var connector = provider.GetService<IDatabaseConnector>();
            NotNull(connector);
            var resolved = provider.GetService<Func<DbProviderFactory>>();
            NotNull(resolved);
            Same(factory, resolved);
        }
        
        [Fact]
        public void NullProviderFactoryThrowsArgumentNullException()
        {
            var builder = new SyrxBuilder(_services);
            var settings = _fixture.SettingsDelegate;
            var result = Throws<ArgumentNullException>(() => builder.UseConnector(null, settings));
            result.HasMessage("The DbProviderFactory delegate cannot be null. (Parameter 'providerFactory')");
        }

        [Fact]
        public void NullSettingsDelegateThrowsArgumentNullException()
        {
            var connection = new Mock<DbConnection>();
            var dbProviderFactory = new Mock<DbProviderFactory>();
            dbProviderFactory.Setup(x => x.CreateConnection()).Returns(connection.Object);

            Func<DbProviderFactory> factory = () => dbProviderFactory.Object;

            var builder = new SyrxBuilder(_services);
            var result = Throws<ArgumentNullException>(() => builder.UseConnector(factory, null));
            result.HasMessage("The CommanderSettings delegate cannot be null. (Parameter 'settingsFactory')");
        }


    }


    public class DatabaseConnectorExtensionsFixture
    {
        public Action<CommanderSettingsBuilder> SettingsDelegate {
            get {
                Action<CommanderSettingsBuilder> result = (a) => a.AddCommand(
                    b => b.ForType<CommanderSettingsBuilder>(
                        c => c.ForMethod("TestMethod",
                        d => d.UseConnectionAlias("test-alias")
                              .UseCommandText("test-command"))));
                return result;
            }
        }
    }
}

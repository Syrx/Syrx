using Syrx.Extensions;

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions.Tests.Unit.DependencyInjection.SqlServerOptionsBuilderExtensionsTests
{
    public class UseSqlServer
    {
        [Fact]
        public void Successfully()
        {
            var services = new ServiceCollection();
            services.UseSyrx(
                builder => builder
                .UseSqlServer(
                    factory => factory
                        .AddConnectionString("test-alias", "test-connection-string")
                        .AddCommand(f => f
                            .ForRepositoryType<UseSqlServer>()
                            .ForMethodNamed(nameof(Successfully))
                            .UseCommandText("test-command-text")
                            .AgainstConnectionAlias("test-alias")
                        )));
        }

        [Fact]
        public void ConfigureFromJsonFile()
        {
        }
    }
}

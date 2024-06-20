using Syrx.Commanders.Databases.Connectors.Npgsql;
using Syrx.Commanders.Databases.Connectors.Npgsql.Extensions;

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions.Tests.Unit.SqlServerConnectorExtensionsTests
{
    public class UseSqlServer
    {
        private IServiceCollection _services;

        public UseSqlServer()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void Successful()
        {
            _services.UseSyrx(a => a
                .UsePostgres(b => b
                    .AddCommand(c => c
                        .ForType<UseSqlServer>(d => d
                            .ForMethod(nameof(Successful), e => e.UseCommandText("test-command").UseConnectionAlias("test-aliase"))))));

            var provider = _services.BuildServiceProvider();
            var connector = provider.GetService<IDatabaseConnector>();
            IsType<NpgsqlDatabaseConnector>(connector);
        }
    }
}

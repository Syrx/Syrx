
using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Connectors.Npgsql.Extensions;
using Syrx.Extensions;

namespace Syrx.Npgsql.Tests.Integration
{
    public class UnitTest1
    {
        [Fact(Skip = "inital connection... ")]
        public void Test1()
        {
            var builder = new  SyrxBuilder(new ServiceCollection());

            var settings = builder
                .ServiceCollection
                .UseSyrx(a => a
                .UsePostgres(
                    b => b.AddConnectionString("Syrx.Postgres", "Host=localhost;Port=5432;Database=syrx;Username=postgres;Password=syrxforpostgres;")
                    .AddCommand(c => c.ForType<UnitTest1>(
                        d => d.ForMethod(
                            nameof(Test1),
                            e => e.UseConnectionAlias("Syrx.Postgres")
                            .UseCommandText("select 1/0;"))))));

            var provider = builder.ServiceCollection.BuildServiceProvider();
            var commander = provider.GetService<ICommander<UnitTest1>>();

            var result = commander.Query<bool>();

        }
    }
}
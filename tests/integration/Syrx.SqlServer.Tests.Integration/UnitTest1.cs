using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Connectors.SqlServer.Extensions;
using Syrx.Commanders.Databases.Settings;
using Syrx.Extensions;
using Syrx.Tests.Extensions;

namespace Syrx.SqlServer.Tests.Integration
{
    public class UnitTest1
    {
        [Fact]
        public void Configuation()
        {
            var services = new ServiceCollection();

            services.UseSyrx(x =>
                x.UseSqlServer( 
                    a => a.AddConnectionString("test-name", "test-string")                
                .AddCommand(b => b
                    .ForRepositoryType<UnitTest1>()
                    .ForMethodNamed(nameof(Configuation))
                    .AgainstConnectionAlias("no")
                    .UseCommandText("test-command"))
                .AddCommand(b => b
                    .ForRepositoryType<UnitTest1>()
                    .ForMethodNamed($"Retrieve")
                    .AgainstConnectionAlias("test")
                    .UseCommandText("second-test-command"))
                ));

            var provider = services.BuildServiceProvider();
            var settings = provider.GetRequiredService<IDatabaseCommanderSettings>();
            settings.PrintAsJson();

        }
    }
}
using Syrx.Commanders.Databases.Connectors.SqlServer.Extensions;
using Syrx.Extensions;
using Microsoft.Extensions.Configuration;
using Syrx.Commanders.Databases.Extensions.Configuration;

namespace Syrx.Commanders.Databases.Tests.Integration.Setup
{
    public class Installer
    {
        public IServiceProvider Install(IServiceCollection services = null)
        {
            services ??= new ServiceCollection();

            /*
            services.UseSyrx(x => x
                            .UseSqlServer(f => f
                                .AddConnectionStrings()
                                .AddSetupBuilderCommands()
                                .AddMultimapCommands()
                                .AddMultipleCommands()
                                .AddExecuteCommands()
                                .AddDisposeCommands()
                                
                                ));
            */

            services.UseSyrx(a => a
                        .UseSqlServer(
                            b => b
                            .AddConnectionStrings()
                            .AddSetupBuilderOptions()
                            .AddQueryMultimap()
                            .AddQueryMultiple()
                            .AddExecute()
                            ));

            var result = services.BuildServiceProvider();

            var settings = result.GetRequiredService<ICommanderOptions>();

            return result;
        }

        public static IServiceProvider Install()
        {
            var installer = new Installer();
            return installer.Install(new ServiceCollection());
        }

    }
}

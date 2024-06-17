using Syrx.Commanders.Databases.Connectors.SqlServer.Extensions;
using Syrx.Extensions;
using Microsoft.Extensions.Configuration;
using Syrx.Commanders.Databases.Settings;

namespace Syrx.Commanders.Databases.Tests.Integration.Setup
{
    public class Installer
    {
        public IServiceProvider Install(IServiceCollection services = null)
        {
            services ??= new ServiceCollection();

            services.UseSyrx(a => a
                        .UseSqlServer(
                            b => b
                            .AddConnectionStrings()
                            .AddSetupBuilderOptions()
                            .AddQueryMultimap()
                            .AddQueryMultiple()
                            .AddExecute()
                            .AddDisposeCommands()
                            ));

            var result = services.BuildServiceProvider();
                        
            return result;
        }

        public static IServiceProvider Install()
        {
            var installer = new Installer();
            return installer.Install(new ServiceCollection());
        }

    }
}

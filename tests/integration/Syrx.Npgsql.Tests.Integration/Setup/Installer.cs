using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Connectors.Npgsql.Extensions;
using Syrx.Extensions;

namespace Syrx.Npgsql.Tests.Integration.Setup
{
    public class Installer
    {
        public IServiceProvider Install(IServiceCollection services = null)
        {
            services ??= new ServiceCollection();

            services.UseSyrx(a => a.SetupPostgres());

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

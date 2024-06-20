using Syrx.Commanders.Databases.Connectors.SqlServer.Extensions;
using Syrx.Extensions;

namespace Syrx.Commanders.Databases.Tests.Integration.Setup
{
    public class Installer
    {
        public IServiceProvider Install(IServiceCollection services = null)
        {
            services ??= new ServiceCollection();

            services.UseSyrx(a => a.SetupSqlServer());

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

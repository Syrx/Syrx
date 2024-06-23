
namespace Syrx.Commanders.Databases.Tests.Integration.Setup
{
    public class Installer
    {
        public IServiceProvider Install(IServiceCollection services)
        {
            //services ??= new ServiceCollection();

            return services.BuildServiceProvider();
                        
        }

    }
}

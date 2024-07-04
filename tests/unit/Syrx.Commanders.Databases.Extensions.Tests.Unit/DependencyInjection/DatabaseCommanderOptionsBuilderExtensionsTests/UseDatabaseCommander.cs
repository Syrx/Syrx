namespace Syrx.Commanders.Databases.Extensions.Tests.Unit.DependencyInjection.DatabaseCommanderOptionsBuilderExtensionsTests
{
    public class UseDatabaseCommander
    {
        [Fact]
        public void Successfully()
        {
            var services = new ServiceCollection();
            //var options = new SyrxBuilder(collection);            
            //services.AddSyrx(a => a.UseCommander<UseDatabaseCommander>(x => { return new DatabaseCommander<UseDatabaseCommander>(new Settings.Readers.DatabaseCommandReader(null), new Connectors.DatabaseConnector (null)) } ))

            //services.AddDatabaseCommander()
        }
    }
}

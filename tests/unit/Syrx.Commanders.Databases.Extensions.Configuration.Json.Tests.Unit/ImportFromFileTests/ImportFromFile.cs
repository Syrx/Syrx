using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Json.Tests.Unit.ImportFromFileTests
{
    public class ImportFromFile(JsonFileFixture fixture) : IClassFixture<JsonFileFixture>
    {
        
        [Fact]
        public void ConfigureFromFile()
        {
            // wire up
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder();
                        
            var options = fixture.GetTestOptions<ImportFromFile>();            
            var path = fixture.WriteToFile(options);

            // act
            builder.AddJsonFile(fixture.FileName);

            // finish up the wire up
            services.Configure<CommanderOptions>(builder.Build());

            var provider = services.BuildServiceProvider();
            var resolved = provider.GetService<IOptions<CommanderOptions>>();

            // assertions
            NotNull(resolved);
                       
            Equal(options.Connections, resolved.Value.Connections);
            Single(resolved.Value.Namespaces);
            Equal(options.Namespaces.Single().Namespace, resolved.Value.Namespaces.Single().Namespace);
            Single(resolved.Value.Namespaces.Single().Types);
            Equal(options.Namespaces.Single().Types.Single().Name, resolved.Value.Namespaces.Single().Types.Single().Name);
            Equal(2, resolved.Value.Namespaces.Single().Types.Single().Commands.Count);

        }
    
    
    }
}
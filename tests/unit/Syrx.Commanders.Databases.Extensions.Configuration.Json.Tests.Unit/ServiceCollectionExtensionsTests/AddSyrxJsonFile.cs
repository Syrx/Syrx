using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Syrx.Commanders.Databases.Settings;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Json.Tests.Unit.ServiceCollectionExtensionsTests
{
    public class AddSyrxJsonFile(JsonFileFixture fixture) : IClassFixture<JsonFileFixture>
    {
        [Fact]
        public void Successfully()
        {
            var services = fixture.Services;
            var builder = fixture.ConfigurationBuilder;
            
            // write file
            var options = fixture.GetTestOptions<AddSyrxJsonFile>();            
            var filename = fixture.WriteToFile(options);
                        
            // act
            services.AddSyrxJsonFile(builder, filename);
            services.Configure<CommanderSettings>(builder.Build());

            // finalze build
            var provider = services.BuildServiceProvider();
            var resolved = provider.GetService<IOptions<CommanderSettings>>();

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

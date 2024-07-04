using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Extensions.Json.Tests.Unit.ServiceCollectionExtensionsTests
{
    public class AddSyrxJsonFile(JsonFileFixture fixture) : IClassFixture<JsonFileFixture>
    {
        [Fact]
        public void Successfully()
        {
            var services = fixture.Services;
            var builder = fixture.ConfigurationBuilder;

            // write file
            var settings = fixture.GetTestOptions<AddSyrxJsonFile>();
            var filename = fixture.WriteToFile(settings);

            // act
            services.AddSyrxJsonFile(builder, filename);
            

            // finalze build
            var configuration = builder.Build();
            var provider = services.BuildServiceProvider();
            var resolved = configuration.Get<CommanderSettings>();

            // assertions
            NotNull(resolved);
            Equivalent(settings, resolved, true);
            
            Equal(settings.Connections, resolved.Connections);
            Single(resolved.Namespaces);
            Equal(settings.Namespaces.Single().Namespace, resolved.Namespaces.Single().Namespace);
            Single(resolved.Namespaces.Single().Types);
            Equal(settings.Namespaces.Single().Types.Single().Name, resolved.Namespaces.Single().Types.Single().Name);
            Equal(2, resolved.Namespaces.Single().Types.Single().Commands.Count);
            
        }
    }
}

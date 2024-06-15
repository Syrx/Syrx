using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Syrx.Tests.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Xml.Tests.Unit.ServiceCollectionExtensionsTests
{
    public class AddSyrxXmlFile(XmlFileFixture fixture) : IClassFixture<XmlFileFixture>
    {
        [Fact(Skip ="Needs to be revisited.")]
        public void Successfully()
        {
            var services = fixture.Services;
            var builder = fixture.ConfigurationBuilder;

            // write file
            var options = fixture.GetTestOptions<AddSyrxXmlFile>();

            var filename = fixture.WriteToFile(options);
            filename.PrintAsJson();

            // act
            services.AddSyrxXmlFile(builder, filename);
            services.Configure<CommanderOptions>(builder.Build());

            // finalze build
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

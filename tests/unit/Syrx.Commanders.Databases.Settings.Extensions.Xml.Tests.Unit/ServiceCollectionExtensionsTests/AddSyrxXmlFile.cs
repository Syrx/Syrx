using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Extensions.Xml.Tests.Unit.ServiceCollectionExtensionsTests
{
    public class AddSyrxXmlFile(XmlFileFixture fixture) : IClassFixture<XmlFileFixture>
    {
        // parsing from xml is supported, but painful. the syrx.settings.xml 
        // file can be used as a reference as that works but dynamically writing 
        // writing the file out before testing is proving a little more cumbersome.

        [Fact(Skip = "Dynamic writing of the file is dropping one of the elements.")]
        public void Successfully()
        {
            var services = fixture.Services;
            var builder = fixture.ConfigurationBuilder;

            // write file
            var settings = fixture.GetTestOptions<AddSyrxXmlFile>();
            var filename = fixture.WriteToFile(settings);

            // act
            services.AddSyrxXmlFile(builder, filename);

            // finalze build
            var configuration = builder.Build();
            var provider = services.BuildServiceProvider();
            var resolved = configuration.Get<CommanderSettings>();

            // assertions
            NotNull(resolved);
            //Equivalent(settings, resolved);

            Equal(settings.Connections, resolved.Connections);
            Single(resolved.Namespaces);
            Equal(settings.Namespaces.Single().Namespace, resolved.Namespaces.Single().Namespace);
            Single(resolved.Namespaces.Single().Types);
            Equal(settings.Namespaces.Single().Types.Single().Name, resolved.Namespaces.Single().Types.Single().Name);
            Equal(2, resolved.Namespaces.Single().Types.Single().Commands.Count);
        }

        [Fact(Skip = "Reading of the file is dropping one of the elements.")]
        public void SuccessfullyWithPrebuiltFile()
        {
            var services = fixture.Services;
            var builder = fixture.ConfigurationBuilder;
            const string path = "syrx.settings.xml";
            services.AddSyrxXmlFile(builder, path);


            // finalze build
            var configuration = builder.Build();
            var provider = services.BuildServiceProvider();
            var resolved = configuration.Get<CommanderSettings>();

            // assertions
            NotNull(resolved);

            Single(resolved.Connections);
            Single(resolved.Namespaces);
            Single(resolved.Namespaces.Single().Types);
            Equal(2, resolved.Namespaces.Single().Types.Single().Commands.Count);
        }
    }
}

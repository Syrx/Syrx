using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Syrx.Commanders.Databases.Settings;
using Syrx.Commanders.Databases.Settings.Extensions;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Xml.Tests.Unit
{
    public class XmlFileFixture
    {
        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";
        private const string CommandText = "test-command-text";

        public string FileName => $"syrx.settings.{DateTime.UtcNow.ToString("yyMMddHH")}.xml";
        public IServiceCollection Services { get; }
        public IConfigurationBuilder ConfigurationBuilder { get; }
        public XmlFileFixture()
        {
            Services = new ServiceCollection();
            ConfigurationBuilder = new ConfigurationBuilder();
        }
                
        public string WriteToFile(CommanderSettings options)
        {
            var path = FileName;
            File.WriteAllText(path, SerializeToXml(options));

            return path;
        }

        public CommanderSettings GetTestOptions<TType>()
        {
            return
            CommanderOptionsBuilderExtensions.Build(
                a => a.AddConnectionString(b => b.UseAlias(Alias).UseConnectionString(ConnectionString))
                       .AddCommand(c => c.ForType<TType>(d => d
                                .ForMethod("Method1", e => e.UseCommandText(CommandText).UseConnectionAlias(Alias))
                                .ForMethod("Method2", e => e.UseCommandText(CommandText).UseConnectionAlias(Alias)))));

        }
                
        public static string SerializeToXml<T>(T obj)
        {
            // Convert the object to JSON
            string json = JsonConvert.SerializeObject(obj);

            // Parse the JSON string into a JObject
            var parsedJson = JsonConvert.DeserializeObject(json);

            // Convert the JObject to XML
            var xml = JsonConvert.DeserializeXNode(parsedJson.ToString(), typeof(T).Name);

            // Return the XML string
            return xml.ToString();
        }

    }


}

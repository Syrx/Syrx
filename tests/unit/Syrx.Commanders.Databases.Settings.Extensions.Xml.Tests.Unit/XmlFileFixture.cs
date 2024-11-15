using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Xml;

namespace Syrx.Commanders.Databases.Settings.Extensions.Xml.Tests.Unit
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
            WriteXml(path, options);

            return path;
        }

        public CommanderSettings GetTestOptions<TType>()
        {
            return
            CommanderSettingsBuilderExtensions.Build(
                a => a.AddConnectionString(b => b.UseAlias(Alias).UseConnectionString(ConnectionString))
                       .AddCommand(c => c.ForType<TType>(d => d
                                .ForMethod("Method1", e => e.UseCommandText(CommandText).UseConnectionAlias(Alias))
                                .ForMethod("Method2", e => e.UseCommandText(CommandText).UseConnectionAlias(Alias)))));

        }


        public void WriteXml(string filename, CommanderSettings root)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true; // Makes the XML easier to read

            using (var writer = XmlWriter.Create(filename, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("CommanderSettings");

                writer.WriteStartElement("Namespaces");
                foreach (var ns in root.Namespaces)
                {
                    writer.WriteStartElement("Namespace");
                    writer.WriteElementString("Namespace", ns.Namespace);

                    writer.WriteStartElement("Types");
                    foreach (var type in ns.Types)
                    {
                        writer.WriteStartElement("Type");
                        writer.WriteElementString("Name", type.Name);

                        writer.WriteStartElement("Commands");
                        foreach (var command in type.Commands)
                        {
                            writer.WriteStartElement(command.Key);
                            writer.WriteElementString("Split", command.Value.Split);
                            writer.WriteElementString("CommandText", command.Value.CommandText);
                            writer.WriteElementString("CommandTimeout", command.Value.CommandTimeout.ToString());
                            writer.WriteElementString("Flags", command.Value.Flags.ToString());
                            writer.WriteElementString("ConnectionAlias", command.Value.ConnectionAlias);
                            writer.WriteEndElement(); // End Command
                        }
                        writer.WriteEndElement(); // End Commands

                        writer.WriteEndElement(); // End Type
                    }
                    writer.WriteEndElement(); // End Types

                    writer.WriteEndElement(); // End Namespace
                }
                writer.WriteEndElement(); // End Namespaces

                writer.WriteStartElement("Connections");
                foreach (var connection in root.Connections)
                {
                    writer.WriteStartElement("Connection");
                    writer.WriteElementString("Alias", connection.Alias);
                    writer.WriteElementString("ConnectionString", connection.ConnectionString);
                    writer.WriteEndElement(); // End Connection
                }
                writer.WriteEndElement(); // End Connections

                writer.WriteEndElement(); // End Root
                writer.WriteEndDocument();
            }
        }

    }


}

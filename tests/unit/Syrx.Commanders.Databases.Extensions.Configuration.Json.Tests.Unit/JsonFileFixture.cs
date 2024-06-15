using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Extensions.Configuration.Builders;
using Syrx.Tests.Extensions;

namespace Syrx.Commanders.Databases.Extensions.Configuration.Json.Tests.Unit
{
    public class JsonFileFixture
    {
        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";
        private const string CommandText = "test-command-text";

        public string FileName => $"syrx.settings.{DateTime.UtcNow.ToString("yyMMddHH")}.json";
        public IServiceCollection Services { get; }
        public IConfigurationBuilder ConfigurationBuilder { get; }
        public JsonFileFixture()
        {
            Services = new ServiceCollection();
            ConfigurationBuilder = new ConfigurationBuilder();
        }

        public string WriteToFile(CommanderOptions options)
        {
            var path = FileName;
            File.WriteAllText(path, options.Serialize());
            return path;
        }
        
        public CommanderOptions GetTestOptions<TType>()
        {
            return
            CommanderOptionsBuilderExtensions.Build(
                a => a.AddConnectionString(b => b.UseAlias(Alias).UseConnectionString(ConnectionString))
                       .AddCommand(c => c.ForType<TType>(d => d
                                .ForMethod("Method1", e => e.UseCommandText(CommandText).UseConnectionAlias(Alias))
                                .ForMethod("Method2", e => e.UseCommandText(CommandText).UseConnectionAlias(Alias)))));

        }

    }
}
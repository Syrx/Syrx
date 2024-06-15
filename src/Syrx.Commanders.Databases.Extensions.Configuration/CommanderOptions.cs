
namespace Syrx.Commanders.Databases.Extensions.Configuration
{
    public sealed record CommanderOptions
    {
        public required List<NamespaceSettingOptions> Namespaces { get; init; }
        
        public List<ConnectionStringSettingOptions>? Connections { get; init; }
    }
}

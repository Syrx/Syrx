namespace Syrx.Commanders.Databases.Extensions.Configuration
{
    public interface ICommanderOptions
    {
        List<ConnectionStringSettingOptions>? Connections { get; init; }
        List<NamespaceSettingOptions> Namespaces { get; init; }
    }

    public sealed record CommanderOptions : ICommanderOptions
    {
        public required List<NamespaceSettingOptions> Namespaces { get; init; }

        public List<ConnectionStringSettingOptions>? Connections { get; init; }
    }
}

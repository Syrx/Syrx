
namespace Syrx.Commanders.Databases.Extensions.Configuration
{
    public sealed record NamespaceSettingOptions
    {
        public string Namespace { get; init; }
        public List<TypeSettingOptions> Types { get; init; }
    }
}

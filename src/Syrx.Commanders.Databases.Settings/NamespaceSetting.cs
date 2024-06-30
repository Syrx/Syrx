namespace Syrx.Commanders.Databases.Settings
{
    public sealed record NamespaceSetting
    {
        public required string Namespace { get; init; }
        public required List<TypeSetting> Types { get; init; }
    }
}

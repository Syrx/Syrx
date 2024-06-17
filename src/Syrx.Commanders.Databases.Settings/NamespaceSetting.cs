namespace Syrx.Commanders.Databases.Settings
{
    public sealed record NamespaceSetting
    {
        public string Namespace { get; init; }
        public List<TypeSetting> Types { get; init; }
    }
}

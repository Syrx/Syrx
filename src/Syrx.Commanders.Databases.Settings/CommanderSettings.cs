namespace Syrx.Commanders.Databases.Settings
{

    public sealed record CommanderSettings : ICommanderSettings
    {
        public required List<NamespaceSetting> Namespaces { get; init; }

        public List<ConnectionStringSetting>? Connections { get; init; }
    }
}

namespace Syrx.Commanders.Databases.Settings
{
    public interface ICommanderSettings
    {
        List<ConnectionStringSetting>? Connections { get; init; }
        List<NamespaceSetting> Namespaces { get; init; }
    }
}

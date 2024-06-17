namespace Syrx.Commanders.Databases.Settings
{
    public sealed record TypeSetting
    {
        public string Name { get; init; }
        public Dictionary<string, CommandSetting> Commands { get; init; }
    }
}

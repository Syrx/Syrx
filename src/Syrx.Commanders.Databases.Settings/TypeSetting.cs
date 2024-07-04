namespace Syrx.Commanders.Databases.Settings
{
    public sealed record TypeSetting
    {
        public required string Name { get; init; }
        public required Dictionary<string, CommandSetting> Commands { get; init; }
    }
}


namespace Syrx.Commanders.Databases.Extensions.Configuration
{
    public sealed record TypeSettingOptions
    {
        public string Name { get; init; }
        public Dictionary<string, CommandSettingOptions> Commands { get; init; }
    }
}

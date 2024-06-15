
namespace Syrx.Commanders.Databases.Extensions.Configuration
{
    public sealed record ConnectionStringSettingOptions
    {
        public required string ConnectionString { get; init; }
        public required string Alias { get; init; }
    }
}

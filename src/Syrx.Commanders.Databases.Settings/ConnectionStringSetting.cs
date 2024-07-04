namespace Syrx.Commanders.Databases.Settings
{
    public sealed record ConnectionStringSetting
    {
        public required string ConnectionString { get; init; }
        public required string Alias { get; init; }
    }
}

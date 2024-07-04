namespace Syrx.Commanders.Databases.Settings
{
    public sealed record CommandSetting
    {
        public string Split { get; init; } = "Id";
        public required string CommandText { get; init; }
        public int CommandTimeout { get; init; } = 30;
        public CommandType CommandType { get; init; } = CommandType.Text;
        public CommandFlagSetting Flags { get; init; } = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache;
        public IsolationLevel IsolationLevel { get; init; } = IsolationLevel.Serializable;
        public required string ConnectionAlias { get; init; }
    }
}

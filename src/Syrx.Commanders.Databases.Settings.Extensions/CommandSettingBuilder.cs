namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public class CommandSettingBuilder
    {
        private string _split = "id"; // default
        private string _commandText;
        private string _alias;
        private int _commandTimeout = 30;
        private CommandType _commandType = CommandType.Text;
        private CommandFlagSetting _commandFlagSetting = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache;
        private IsolationLevel _isolationLevel = IsolationLevel.Serializable;

        public CommandSettingBuilder()
        {
            _commandText = string.Empty;
            _alias = string.Empty;
        }
        public CommandSettingBuilder SplitOn(string split = "id")
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(split), nameof(split));
            _split = split;
            return this;
        }

        public CommandSettingBuilder UseCommandText(string commandText)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(commandText), nameof(commandText));
            _commandText = commandText;
            return this;
        }

        public CommandSettingBuilder SetCommandTimeout(int commandTimeout = 30)
        {
            Throw<ArgumentException>(commandTimeout > 1, $"CommandTimeout cannot be less than 1. The value '{commandTimeout}' is not valid.");
            _commandTimeout = commandTimeout;
            return this;
        }

        public CommandSettingBuilder SetCommandType(CommandType commandType = CommandType.Text)
        {
            _commandType = commandType;
            return this;
        }

        public CommandSettingBuilder SetFlags(CommandFlagSetting commandFlagSetting = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache)
        {
            _commandFlagSetting = commandFlagSetting;
            return this;
        }

        public CommandSettingBuilder SetIsolationLevel(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            _isolationLevel = isolationLevel;
            return this;
        }

        public CommandSettingBuilder UseConnectionAlias(string alias)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(alias), nameof(alias));
            _alias = alias;
            return this;
        }

        protected internal CommandSetting Build()
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_alias), $"The connection string alias must be set. Use the '{nameof(UseConnectionAlias)}' method to set.");
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_commandText), $"The command text cannot be null, empty or blank. Use the '{nameof(UseCommandText)}' method to set.");

            return new CommandSetting
            {
                CommandText = _commandText,
                CommandTimeout = _commandTimeout,
                CommandType = _commandType,
                ConnectionAlias = _alias,
                Flags = _commandFlagSetting,
                IsolationLevel = _isolationLevel,
                Split = _split
            };
        }

    }

}
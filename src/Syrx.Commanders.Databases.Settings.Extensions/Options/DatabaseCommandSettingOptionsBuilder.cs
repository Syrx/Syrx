// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (17:15)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Options
{
    public class DatabaseCommandSettingOptionsBuilder
    {
        internal protected Type Type { get; private set; }
        internal protected string _alias;
        internal protected string _commandText;
        internal protected CommandType _commandType = CommandType.Text;
        internal protected int _commandTimeout = 30;
        internal protected string _split;
        internal protected CommandFlagSetting _commandFlagSetting = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache;
        internal protected IsolationLevel _isolationLevel = IsolationLevel.Serializable;
        internal protected string Name { get; private set; }
        public DatabaseCommandSetting CommandSetting { get; private set; }

        public DatabaseCommandSettingOptionsBuilder ForRepositoryType(Type type)
        {
            Throw<ArgumentNullException>(type != null, nameof(type));
            Type = type;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder ForRepositoryType<TType>()
        {
            Type = typeof(TType);
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder ForMethodNamed(string method)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(method), nameof(method));
            Name = method;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder AgainstConnectionAlias(string connectionAlias = null)
        {
            if (string.IsNullOrWhiteSpace(connectionAlias))
            {
                connectionAlias = Type?.FullName;
            }

            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionAlias), nameof(connectionAlias));
            _alias = connectionAlias;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder UseCommandText(string commandText)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(commandText), nameof(commandText));
            _commandText = commandText;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder UsingCommandType(CommandType commandType = CommandType.Text)
        {
            _commandType = commandType;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder WithCommandTimeout(int timeout = 30)
        {
            _commandTimeout = timeout;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder SplitResultsOn(string split = null)
        {
            _split = split;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder WithCommandFlags(CommandFlagSetting commandFlag = CommandFlagSetting.Buffered | CommandFlagSetting.NoCache)
        {
            _commandFlagSetting = commandFlag;
            return this;
        }

        public DatabaseCommandSettingOptionsBuilder WithIsolationLevel(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            _isolationLevel = isolationLevel;
            return this;
        }

        /// <summary>
        /// This is deliberately marked as protected internal - callers should not be able to access directly. 
        /// </summary>
        /// <returns></returns>
        protected internal DatabaseCommandSetting Build()
        {
            Throw<InvalidOperationException>(!string.IsNullOrWhiteSpace(Name), $"Please set the command name using the '{nameof(ForMethodNamed)}()' method.");
            Throw<InvalidOperationException>(Type != null, $"Please map the command to the calling type using the '{nameof(ForRepositoryType)}<TType>()' method.");

            if (string.IsNullOrWhiteSpace(_alias))
            {
                _alias ??= Type.FullName;
            }

            // todo: validation tests on this builder. 
            var setting = new DatabaseCommandSetting(
                _alias,
                _commandText,
                _commandType,
                _commandTimeout,
                _split,
                _commandFlagSetting,
                _isolationLevel
            );

            CommandSetting = setting;
            return setting;
        }
    }
}
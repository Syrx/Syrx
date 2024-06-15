//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:58)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Settings
{
    /// <summary>
    /// This class exists to support the separation of a
    /// command from a commander.
    /// </summary>
    public record DatabaseCommandSetting : ICommandSetting
    {
        /// <summary>
        /// Used for Dapper multimap splits. 
        /// </summary>
        public string Split { get; init; }

        /// <summary>
        /// The command text to be executed against the SQL database.
        /// </summary>
        public string CommandText { get; init; }

        /// <summary>
        /// The <see cref="System.Data.IDbCommand.CommandTimeout"/>. Defaults to 30.        
        /// </summary>
        public int CommandTimeout { get; init; }

        /// <summary>
        /// The <see cref="System.Data.CommandType"/>. Defaults to Text.
        /// </summary>
        public CommandType CommandType { get; init; }

        /// <summary>
        /// The <see cref="CommandFlagSetting"/>. Defaults to <see cref="CommandFlagSetting.None"/>
        /// </summary>
        public CommandFlagSetting Flags { get; init; }

        /// <summary>
        /// Gets or sets the isolation level used by a transaction. 
        /// Defaults to <see><cref>IsolationLevel.Serializable</cref></see>
        /// </summary>        
        public IsolationLevel IsolationLevel { get; init; }

        /// <summary>
        /// Used for aliasing connections
        /// </summary>
        public string ConnectionAlias { get; init; }

        public DatabaseCommandSetting(
            string connectionAlias,
            string commandText,
            CommandType commandType = CommandType.Text,
            int commandTimeout = 30,
            string split = "Id",
            CommandFlagSetting flags = CommandFlagSetting.None,
            IsolationLevel isolationLevel = IsolationLevel.Serializable
        )
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(connectionAlias), nameof(connectionAlias));
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(commandText), nameof(commandText));

            ConnectionAlias = connectionAlias;
            CommandText = commandText;
            CommandType = commandType;
            CommandTimeout = commandTimeout;
            Split = split;
            Flags = flags;
            IsolationLevel = isolationLevel == 0 ? IsolationLevel.Serializable : isolationLevel;
        }
    }
}
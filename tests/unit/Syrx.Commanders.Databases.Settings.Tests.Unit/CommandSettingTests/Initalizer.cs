using System.Data;
using static Xunit.Assert;
using static Syrx.Commanders.Databases.Settings.Tests.Unit.TestsConstants;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.CommandSettingTests
{
    public class Initalizer
    {
        [Fact]
        public void Successfully()
        {
            var result = new CommandSetting
            {
                CommandText = CommandSettings.CommandText,
                ConnectionAlias = CommandSettings.ConnectionAlias,
                CommandTimeout = CommandSettings.CommandTimeout,
                CommandType = CommandSettings.CommandType,
                Flags = CommandSettings.Flags,
                IsolationLevel = CommandSettings.IsolationLevel,
                Split = CommandSettings.Split
            };

            Equal(CommandSettings.CommandText, result.CommandText);
            Equal(CommandSettings.ConnectionAlias, result.ConnectionAlias);
            Equal(CommandSettings.CommandTimeout, result.CommandTimeout);
            Equal(CommandSettings.CommandType, result.CommandType);
            Equal(CommandSettings.Flags, result.Flags);
            Equal(CommandSettings.IsolationLevel, result.IsolationLevel);
            Equal(CommandSettings.Split, result.Split);
        }

        [Fact]
        public void SuccessfullyWithDefaults()
        {
            var result = new CommandSetting
            {
                // these two are required. 
                CommandText = CommandSettings.CommandText,
                ConnectionAlias = CommandSettings.ConnectionAlias
            };

            Equal(CommandSettings.CommandText, result.CommandText);
            Equal(CommandSettings.ConnectionAlias, result.ConnectionAlias);
            Equal(30, result.CommandTimeout);
            Equal(CommandType.Text, result.CommandType);
            Equal(CommandFlagSetting.Buffered|CommandFlagSetting.NoCache, result.Flags);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
            Equal("Id", result.Split);
        }
    }
}

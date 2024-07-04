using System.Data;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit
{
    public class SettingsFixture
    {

        public NamespaceSetting TestNamespaceSetting(List<TypeSetting> typeSettings, string @namespace = "test-namespace")
        {
            return new NamespaceSetting
            {
                Namespace = @namespace,
                Types = typeSettings
            };
        }

        public TypeSetting TestTypeSetting(Dictionary<string, CommandSetting> commands, string name = "test-type-setting")
        {
            return new TypeSetting
            {
                Name = name,
                Commands = commands
            };
        }

        public List<TypeSetting> GetEmptyTypeSettingList()
        {
            return new List<TypeSetting>();
        }

        public List<TypeSetting> GetTypeSettingList()
        {
            return new List<TypeSetting>
            {
                new TypeSetting
                {
                    Name = TestsConstants.TypeSettings.Name,
                    Commands = new Dictionary<string, CommandSetting>
                    {
                        ["name"] = TestCommandSetting()
                    }
                }
            };            
        }

        public CommandSetting TestCommandSetting(
            string commandText = TestsConstants.CommandSettings.CommandText,
            string connectionAlias = TestsConstants.CommandSettings.ConnectionAlias,
            int commandTimeout = TestsConstants.CommandSettings.CommandTimeout,
            CommandType commandType = CommandType.Text,
            CommandFlagSetting flags = CommandFlagSetting.None,
            IsolationLevel isolationLevel = IsolationLevel.Serializable,
            string split = TestsConstants.CommandSettings.Split)
        {
            return new CommandSetting
            {
                CommandText = commandText,
                CommandTimeout = commandTimeout,
                CommandType = commandType,
                ConnectionAlias = connectionAlias,
                Flags = flags,
                IsolationLevel = isolationLevel,
                Split = split
            };
        }
    }
}


using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.TypeSettingTests
{
    public class Initializer
    {
        const string _name = "test-type-setting";
        
        [Fact]
        public void Successfully()
        {
            var result = new TypeSetting
            {
                Name = _name,
                Commands = new Dictionary<string, CommandSetting>
                {
                    ["name"] = new CommandSetting 
                    { 
                        CommandText = TestsConstants.CommandSettings.CommandText, 
                        ConnectionAlias = TestsConstants.CommandSettings.ConnectionAlias
                    }
                }
            };

            NotNull(result);
            Equal(_name, result.Name);
            Equal(TestsConstants.CommandSettings.CommandText, result.Commands.First().Value.CommandText);
            Equal(TestsConstants.CommandSettings.ConnectionAlias, result.Commands.First().Value.ConnectionAlias);
        }
    }
}
    
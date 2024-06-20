using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.TypeSettingTests
{
    public class Initializer
    {

        [Fact]
        public void Successfully()
        {
            var result = new TypeSetting
            {
                Name = "test-type-setting",
                Commands = new Dictionary<string, CommandSetting>
                {
                    ["name"] = new CommandSetting { 
                        CommandText = "test-command-text", 
                        ConnectionAlias = "test-alias"}
                }
            };

            NotNull(result);

        }
    }
}
    
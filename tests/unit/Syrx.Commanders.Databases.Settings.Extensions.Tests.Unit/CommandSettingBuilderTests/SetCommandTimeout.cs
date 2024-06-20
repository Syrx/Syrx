namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests
{
    public class SetCommandTimeout(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        // NOTE:
        // as the build method is protected internal, we 
        // can't assert the happy paths in these tests. 
        // tehy can only be tested by the extention method. 
        // this is because the purpose of the builders/extentions
        // is to enforce the happy path. 
        private CommandSettingBuilder _builder = fixture.Builder;

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CannotBeLessThanOne(int commandTimeout)
        {
            var result = Throws<ArgumentException>(() => _builder.SetCommandTimeout(commandTimeout));
            result.HasMessage($"CommandTimeout cannot be less than 1. The value '{commandTimeout}' is not valid.");
        }
    }
}

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingBuilderTests
{
    public class UseCommandTest(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        // NOTE:
        // as the build method is protected internal, we 
        // can't assert the happy paths in these tests. 
        // tehy can only be tested by the extention method. 
        // this is because the purpose of the builders/extentions
        // is to enforce the happy path. 
        private CommandSettingBuilder _builder = fixture.Builder;

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSplitThrowsArgumentException(string commandText)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseCommandText(commandText));
            result.ArgumentNull(nameof(commandText));
        }
    }


}

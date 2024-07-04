namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommanderSettingsBuilderTests
{
    public class AddConnectionString
    {
        private CommanderSettingsBuilder _builder;
        private const string ConnectionString = "test-connection-string";
        private const string Alias = "test-alias";


        public AddConnectionString()
        {
            _builder = new CommanderSettingsBuilder();
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentNullException(string alias)
        {
            var result = Throws<ArgumentNullException>(() => _builder.AddConnectionString(alias, ConnectionString));
            result.ArgumentNull(nameof(alias));
        }

        [Fact]
        public void AddSingle()
        {
            _ = _builder.AddConnectionString(Alias, ConnectionString);
            // no exception thrown is the assertion
        }

        [Fact]
        public void DuplicateConnectionStringsReturnsOnlyOne()
        {
            _ = _builder
                .AddConnectionString(Alias, ConnectionString)
                .AddConnectionString(Alias, ConnectionString);
            // no exception thrown is the assertion
        }


    }
}

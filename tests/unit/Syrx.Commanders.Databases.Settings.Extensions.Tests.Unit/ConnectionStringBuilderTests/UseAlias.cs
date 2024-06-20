namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.ConnectionStringBuilderTests
{
    public class UseAlias
    {
        private const string Alias = "test-alias";
        private ConnectionStringSettingsBuilder _builder;


        public UseAlias()
        {
            _builder = new ConnectionStringSettingsBuilder();
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentException(string alias)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseAlias(alias));
            result.ArgumentNull(nameof(alias));
        }

        [Fact]
        public void Successfully()
        {
            _ = _builder.UseAlias(Alias);
            // no exception thrown is the assertion. 
        }
    }
}

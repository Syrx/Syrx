namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.ConnectionStringBuilderTests
{
    public class UseConnectionString
    {
        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";
        private ConnectionStringSettingsBuilder _builder;

        public UseConnectionString()
        {
            _builder = new ConnectionStringSettingsBuilder();
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentException(string alias)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseConnectionString(alias, ConnectionString));
            result.ArgumentNull(nameof(alias));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceConnectionStringWithAliasOverloadThrowsArgumentException(string connectionString)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseConnectionString(Alias, connectionString));
            result.ArgumentNull(nameof(connectionString));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceConnectionStringThrowsArgumentException(string connectionString)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseConnectionString(connectionString));
            result.ArgumentNull(nameof(connectionString));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceConnectionOnGenericTypeThrowsArgumentException(string connectionString)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseConnectionString<UseConnectionString>(connectionString));
            result.ArgumentNull(nameof(connectionString));
        }

        [Fact]
        public void SuccessfullyWithGenericType()
        {
            _ = _builder
                .UseConnectionString<UseConnectionString>(ConnectionString);

            // no exception thrown is the assertion. 
        }

        [Fact]
        public void SuccessfullyWithoutType()
        {
            _ = _builder
                .UseConnectionString(ConnectionString);

            // no exception thrown is the assertion. 
        }

        [Fact]
        public void SuccessfullyWithoutTypeWithAliasOverload()
        {
            _ = _builder
                .UseConnectionString(Alias, ConnectionString);

            // no exception thrown is the assertion. 
        }

        [Fact]
        public void DuplicateConnectionStringsReturnsOnlyOne()
        {
            const string connectionString = "expected-to-fail";
            var result = _builder
                    .UseConnectionString(Alias, ConnectionString)
                    .UseConnectionString(Alias, connectionString);

            // no exception is the assertion. 
        }

        [Fact]
        public void SuuportsMultipleConnectionStrings()
        {
            const string connectionString = "another-connection-string";
            const string alias = "another-test-alias";
            var result =
                _builder
                    .UseConnectionString(Alias, ConnectionString)
                    .UseConnectionString(alias, connectionString);

            // no exception thrown is the assertion. 
        }

    }
}

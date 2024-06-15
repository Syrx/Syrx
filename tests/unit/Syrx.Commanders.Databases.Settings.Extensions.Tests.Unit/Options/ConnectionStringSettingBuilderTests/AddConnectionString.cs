namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.ConnectionStringSettingBuilderTests
{
    public class AddConnectionString
    {
        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentException(string alias)
        {
            var result = Throws<ArgumentNullException>(() =>
                new ConnectionStringSettingBuilder().AddConnectionString(alias, ConnectionString)
                );

            result.ArgumentNull(nameof(alias));
        }

        [Fact]
        public void SuccessfullyWithGenericType()
        {
            var result = ConnectionStringSettingBuilder
                .AddConnectionString<Assert>(ConnectionString)
                .ConnectionStringSettings.Single();

            Equal(typeof(Assert).FullName, result.Alias);
            Equal(ConnectionString, result.ConnectionString); 
        }

        [Fact]
        public void SuccessfullyWithoutType()
        {
            var result = new ConnectionStringSettingBuilder()
                .AddConnectionString(Alias, ConnectionString)
                .ConnectionStringSettings.Single();

            Equal(Alias, result.Alias);
            Equal(ConnectionString, result.ConnectionString);
        }

        [Fact]
        public void ThrowsArgumentExceptionOnConnectionStringConflict()
        {
            const string newConnectionString = "expected-to-fail";
            var result = Throws<ArgumentException>(() =>
                new ConnectionStringSettingBuilder()
                    .AddConnectionString(Alias, ConnectionString)
                    .AddConnectionString(Alias, newConnectionString)
            );

            result.HasMessage(@$"The alias '{Alias}' is already assigned to a different connection string. 
Current connection string: {ConnectionString}
New connection string: {newConnectionString}");
        }
    }
}

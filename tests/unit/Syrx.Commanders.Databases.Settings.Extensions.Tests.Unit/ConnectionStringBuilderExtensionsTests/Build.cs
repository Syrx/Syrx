using B = Syrx.Commanders.Databases.Settings.Extensions.ConnectionStringBuilderExtensions;


namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.ConnectionStringBuilderExtensionsTests
{
    public class Build
    {

        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";

        [Fact]
        public void SuccessfullyWithGenericType()
        {
            var result = B.Build(x => x.UseConnectionString<Build>(ConnectionString));

            Equal(typeof(Build).FullName, result.Alias);
            Equal(ConnectionString, result.ConnectionString);
        }


        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentException(string alias)
        {
            var result = Throws<ArgumentNullException>(() => B.Build(x => x.UseConnectionString(alias, ConnectionString)));
            result.ArgumentNull(nameof(alias));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceConnectionThrowsArgumentException(string connectionString)
        {
            var result = Throws<ArgumentNullException>(() => B.Build(x => x.UseConnectionString(Alias, connectionString)));
            result.ArgumentNull(nameof(connectionString));
        }

        [Fact]
        public void AcceptsLastEntryFromBuilder()
        {
            const string connectionString = "another-connection-string";
            var result =
                B.Build(x => x
                    .UseConnectionString(Alias, ConnectionString)
                    .UseConnectionString(Alias, connectionString));

            Equal(Alias, result.Alias);
            Equal(connectionString, result.ConnectionString);
        }

    }
}

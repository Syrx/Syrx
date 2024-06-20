namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.ConnectionStringSettingTests
{
    public class Initalizer
    {
        private const string ConnectionString = "test-connection-string";
        private const string Alias = "test-alias";

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhiteSpaceConnectionString(string connectionString)
        {
            var result = new ConnectionStringSetting
            {
                Alias = Alias,
                ConnectionString = connectionString
            };

            // this isn't what I wasnted but I think it'll do. 
            // the builder will handle validation from that perspective
            // but we'll need to build validation in for the file configuration. 
            True(string.IsNullOrWhiteSpace(result.ConnectionString));
            NotNull(result.Alias);
        }


        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhiteSpaceAlias(string alias)
        {
            var result = new ConnectionStringSetting
            {
                Alias = alias,
                ConnectionString = ConnectionString
            };

            // this isn't what I wasnted but I think it'll do. 
            // the builder will handle validation from that perspective
            // but we'll need to build validation in for the file configuration. 
            True(string.IsNullOrWhiteSpace(result.Alias));
            NotNull(result.ConnectionString);
        }

        [Fact]
        public void Successfully()
        {
            var result = new ConnectionStringSetting
            {
                Alias = Alias,
                ConnectionString = ConnectionString
            };

            Equal(Alias, result.Alias);
            Equal(ConnectionString, result.ConnectionString);
        }

    }
}

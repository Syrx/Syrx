namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.ConnectionStringSettingBuilderTests
{
    public class Constructor
    {
        [Fact]
        public void SupportsNull()
        {
            var result = new ConnectionStringSettingBuilder(null);
            NotNull(result);
            NotNull(result.ConnectionStringSettings);
            Empty(result.ConnectionStringSettings);
        }

        [Fact]
        public void SupportsNullEnumerable()
        {
            List<ConnectionStringSetting> input = null;
            var result = new ConnectionStringSettingBuilder(input);
            NotNull(result);
            NotNull(result.ConnectionStringSettings);
            Empty(result.ConnectionStringSettings);
        }

        [Fact]
        public void WillNotAllowDifferentConnectionStringsOnTheSameAlias()
        {
            var range = new List<ConnectionStringSetting>
            {
                new("test-alias", "test-connection-string"),
                new("test-alias", "test-connection-string-1"),
                new("test-alias-1", "test-connection-string"),
            };

            var expect = @$"The alias 'test-alias' is already assigned to a different connection string. 
Current connection string: test-connection-string
New connection string: test-connection-string-1";

            var result = Throws<ArgumentException>(() => new ConnectionStringSettingBuilder(range));
            result.HasMessage(expect);

        }

        [Fact]
        public void AllowsDifferentAliasesOnTheSameConnectionString()
        {
            var range = new List<ConnectionStringSetting>
            {
                new("test-alias", "test-connection-string"),
                new("test-alias-1", "test-connection-string"),
                new("test-alias-2", "test-connection-string"),
            };

            var result = new ConnectionStringSettingBuilder(range);
            NotNull(result);
            NotNull(result.ConnectionStringSettings);
            NotEmpty(result.ConnectionStringSettings);
            Equal(3, result.ConnectionStringSettings.Count());
        }


        [Fact]
        public void AddRangeOfConnections()
        {
            var range = new List<ConnectionStringSetting>
            {
                new("test-alias", "test-connection-string"),
                new("test-alias-1", "test-connection-string-1"),
                new("test-alias-2", "test-connection-string-2"),
            };

            var result = new ConnectionStringSettingBuilder(range);
            NotNull(result);
            NotNull(result.ConnectionStringSettings);
            NotEmpty(result.ConnectionStringSettings);
            Equal(3, result.ConnectionStringSettings.Count());
        }
    }
}

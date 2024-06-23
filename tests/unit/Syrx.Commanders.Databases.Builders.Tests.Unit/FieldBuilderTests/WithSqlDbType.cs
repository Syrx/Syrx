namespace Syrx.Commanders.Databases.Builders.Tests.Unit.FieldBuilderTests
{
    public class WithSqlDbType
    {
        private const string Name = "test_field";

        [Theory]
        [MemberData(nameof(Generators.SqlDbTypes), MemberType = typeof(Generators))]
        public void Successfully(SqlDbType type)
        {
            var result = AddField(x => x.WithName(Name).WithDataType(type));
            Equal(Name, result.Name);
            Equal(type, result.Type);
        }
    }
}

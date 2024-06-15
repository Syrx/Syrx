namespace Syrx.Commanders.Databases.Builders.Tests.Unit.FieldBuilderTests
{
    public class IsNullable
    {

        private const string Name = "test_field";
        private const SqlDbType Type = SqlDbType.Int;

        [Theory]
        [MemberData(nameof(Generators.BoolValues), MemberType = typeof(Generators))]
        public void Successfully(bool isNullable)
        {
            var result = AddField(x => x.WithName(Name).WithDataType(Type).IsNullable(isNullable));
            Equal(isNullable, result.IsNullable);
            Equal(Name, result.Name);
            Equal(Type, result.Type);
        }
    }
}

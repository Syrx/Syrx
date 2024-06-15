namespace Syrx.Commanders.Databases.Builders.Tests.Unit.TableOptionsTests
{
    public class WithSchema : IClassFixture<TableOptionsFixture>
    {
        private readonly IEnumerable<Field> _fields;
        private readonly Field _field;
        private readonly string _name;
        private readonly string _schema;


        public WithSchema(TableOptionsFixture fixture)
        {
            _fields = fixture.Fields;
            _field = fixture.TestField;
            _name = fixture.Name;
            _schema = fixture.Schema;
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSchemaDefaultsToDbo(string schema)
        {
            var result = TableOptionsBuilderExtensions.Build(
                a => a
                    .WithName(_name)
                    .WithSchema(schema)
                    .AddField(_field));
            Equal(_name, result.Name);
            Equal("dbo", result.Schema);
            Single(_fields);
        }


        [Fact]
        public void SuccessfullyWithOptionalSchema()
        {
            var result = TableOptionsBuilderExtensions.Build(
                a => a
                    .WithName(_name)
                    .AddField(_field));
            Equal(_name, result.Name);
            Equal("dbo", result.Schema);
            Single(_fields);
        }

        [Fact]
        public void SuccessfullyWithSpecifiedSchema()
        {
            var result = TableOptionsBuilderExtensions.Build(
                a => a
                    .WithName(_name)
                    .WithSchema(_schema)
                    .AddField(_field));

            Equal(_name, result.Name);
            Equal(_schema, result.Schema);
            Single(_fields);
        }
    }
}

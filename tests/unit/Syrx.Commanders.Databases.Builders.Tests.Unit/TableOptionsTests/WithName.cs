namespace Syrx.Commanders.Databases.Builders.Tests.Unit.TableOptionsTests
{
    public class WithName : IClassFixture<TableOptionsFixture>
    {
        private readonly IEnumerable<Field> _fields;
        private readonly Field _field;
        private readonly string _name;

        public WithName(TableOptionsFixture fixture)
        {
            _fields = fixture.Fields;
            _field = fixture.TestField;
            _name = fixture.Name;
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceNameThrowsArgumentNullException(string name)
        {
            var result = Throws<ArgumentNullException>(() => TableOptionsBuilderExtensions.Build(a => a.WithName(name).AddField(_field)));
            result.ArgumentNull(nameof(name));
        }

        [Fact]
        public void Successfully()
        {
            var result = TableOptionsBuilderExtensions.Build(a => a.WithName(_name).AddField(_field));
            Equal(_name, result.Name);
            Single(_fields);
        }
    }
}

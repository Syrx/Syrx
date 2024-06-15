namespace Syrx.Commanders.Databases.Builders.Tests.Unit.TableTests
{
    public class Constructor
    {
        private const string Name = "test_table";
        private const string Schema = "test_schema";
        private readonly IEnumerable<Field> _fields;

        public Constructor()
        {
            _fields =
            [
                // use builder extensions. 
                AddField(x=> x.WithName("test_field").WithDataType(SqlDbType.Bit))
            ];
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceNameThrowsArgumentNullException(string name)
        {
            var result = Throws<ArgumentNullException>(() => new Table(name, _fields));
            result.ArgumentNull(nameof(name));
        }


        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSchemaDefaultsToDbo(string schema)
        {
            var result = new Table(Name, _fields, schema);
            Equal(Name, result.Name);
            Equal("dbo", result.Schema);
            Single(_fields);
        }

        [Fact]
        public void NullFieldsCollectionThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => new Table(Name, null));
            result.ArgumentNull("fields");
        }

        [Fact]
        public void EmptyFieldsCollectionThrowsArgumentOutOfRangeException()
        {
            var fields = new List<Field>();
            var result = Throws<ArgumentOutOfRangeException>(() => new Table(Name, fields));
            result.ArgumentOutOfRange(nameof(fields));
        }

        [Fact]
        public void SuccessfullyWithOptionalSchema()
        {
            var result = new Table(Name, _fields);
            Equal(Name, result.Name);
            Equal("dbo", result.Schema);
            Single(_fields);
        }

        [Fact]
        public void SuccessfullyWithSpecifiedSchema()
        {
            var result = new Table(Name, _fields, Schema);
            Equal(Name, result.Name);
            Equal(Schema, result.Schema);
            Single(_fields);
        }
    }
}

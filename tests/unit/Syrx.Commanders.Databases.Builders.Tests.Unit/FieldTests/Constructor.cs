namespace Syrx.Commanders.Databases.Builders.Tests.Unit.FieldTests
{
    public class Constructor
    {
        private const string Name = "test_field";
        private const SqlDbType Type = SqlDbType.Int;

        [Fact]
        public void Successfully()
        {
            var result = new Field(Name, Type);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceNameThrowsArgumentNullException(string name)
        {
            var result = Throws<ArgumentNullException>(() => new Field(name, Type));
            result.ArgumentNull(nameof(name));
        }
    }
}

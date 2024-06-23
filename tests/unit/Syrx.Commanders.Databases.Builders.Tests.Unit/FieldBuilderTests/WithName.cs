namespace Syrx.Commanders.Databases.Builders.Tests.Unit.FieldBuilderTests
{
    public class WithName
    {
        private const string Name = "test_field";

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceKeyThrowsArgumentNullException(string name)
        {
            var result = Throws<ArgumentNullException>(() => AddField(x => x.WithName(name)));
            result.ArgumentNull(nameof(name));
        }

        [Fact]
        public void Successfully()
        {
            var result = AddField(x => x.WithName(Name));
            Equal(Name, result.Name);
        }
    }
}

namespace Syrx.Commanders.Databases.Builders.Tests.Unit.TableOptionsTests
{
    public class AddField : IClassFixture<TableOptionsFixture>
    {
        private readonly IEnumerable<Field> _fields;
        private readonly Field _field;
        private readonly string _name;


        public AddField(TableOptionsFixture fixture)
        {
            _fields = fixture.Fields;
            _field = fixture.TestField;
            _name = fixture.Name;
        }

        [Fact]
        public void NullFieldThrowsArgumentNullException()
        {
            var field = AddField(x => x.WithName("test_null_field").WithDataType(SqlDbType.Bit));
            field = null;
#pragma warning disable CS8604 // Possible null reference argument.
            var result = Throws<ArgumentNullException>(() => TableOptionsBuilderExtensions.Build(a => a.WithName(_name).AddField(field)));
#pragma warning restore CS8604 // Possible null reference argument.
            result.ArgumentNull(nameof(field));
        }

        [Fact]
        public void DuplicateFieldThrowsArgumentException()
        {
            var first = AddField(x => x.WithName(_name).WithDataType(SqlDbType.Bit));
            var second = AddField(x => x.WithName(_name).WithDataType(SqlDbType.Bit));

            var result = Throws<ArgumentException>(() => TableOptionsBuilderExtensions.Build(a => a.WithName(_name).AddField(first).AddField(second)));
            result.DuplicateKey(_name);
        }

        [Fact]
        public void SuccessfullyWithSuppliedField()
        {
            var result = TableOptionsBuilderExtensions.Build(a => a.WithName(_name).AddField(_field));
            NotNull(result);
            Single(result.Fields);
            Same(_field, result.Fields.Single());
        }

        [Fact]
        public void SuccessfullyWithBuilder()
        {
            var result = TableOptionsBuilderExtensions.Build(
                a => a
                    .WithName(_name)
                    .AddField(c => c.WithName("test_field_1").WithDataType(SqlDbType.Int))
                    .AddField(c => c.WithName("test_field_2").WithDataType(SqlDbType.VarChar).HasWidth(50))
                    .AddField(c => c.WithName("test_field_3").WithDataType(SqlDbType.DateTime)));

            NotNull(result);
            Equal(3, result.Fields.Count());
        }
    }
}

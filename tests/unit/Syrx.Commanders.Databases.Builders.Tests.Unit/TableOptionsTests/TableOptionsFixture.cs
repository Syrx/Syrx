namespace Syrx.Commanders.Databases.Builders.Tests.Unit.TableOptionsTests
{
    public class TableOptionsFixture
    {
        public string Name { get; } = "test_table";
        public string Schema { get; } = "test_schema";
        public IEnumerable<Field> Fields { get; }
        public Field TestField { get; }

        public TableOptionsFixture()
        {
            TestField = AddField(x => x.WithName("test_field").WithDataType(SqlDbType.Bit));
            Fields = new List<Field> { TestField };
        }
    }
}

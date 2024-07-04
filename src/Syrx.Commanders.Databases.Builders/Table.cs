namespace Syrx.Commanders.Databases.Builders
{
    public class Table
    {
        public string Schema { get; }
        public string Name { get; }
        public IEnumerable<Field> Fields { get; }

        public Table(string name, IEnumerable<Field> fields, string schema = "dbo")
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));
            Throw<ArgumentNullException>(fields != null, nameof(fields));
            Throw<ArgumentOutOfRangeException>(fields!.Any(), nameof(fields));

            Name = name;
            Fields = fields!;
            Schema = string.IsNullOrWhiteSpace(schema) ? "dbo" : schema;
        }
    }
}

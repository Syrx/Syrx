namespace Syrx.Commanders.Databases.Builders
{
    public class Field
    {
        public string Name { get; }
        public SqlDbType Type { get; }
        public bool IsNullable { get; }
        public int? Width { get; }
        public Field(string name, SqlDbType type, int? width = null, bool isNullable = true)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));

            Name = name;
            Type = type;
            Width = width;
            IsNullable = isNullable;
        }
    }
}

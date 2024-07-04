namespace Syrx.Commanders.Databases.Builders
{
    public class FieldOptions
    {
        private string? _name;
        private SqlDbType _type;
        private int? _width;
        private bool _nullable;
        public FieldOptions WithName(string name)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));
            _name = name;
            return this;
        }

        public FieldOptions WithDataType(SqlDbType type)
        {
            _type = type;
            return this;
        }

        public FieldOptions HasWidth(int? width)
        {

            _width = width;
            return this;
        }

        public FieldOptions IsNullable(bool nullable = true)
        {
            _nullable = nullable;
            return this;
        }

        internal protected Field Build()
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_name), nameof(_name));
            return new Field(_name!, _type, _width, _nullable);
        }
    }
}

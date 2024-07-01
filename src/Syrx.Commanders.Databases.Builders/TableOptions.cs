namespace Syrx.Commanders.Databases.Builders
{
    public class TableOptions
    {
        private string _name;
        private string _schema;
        private IDictionary<string, Field> _fields;

        public TableOptions()
        {
            _fields = new Dictionary<string, Field>();
            _name = string.Empty;
            _schema = string.Empty;
        }

        public TableOptions WithName(string name)
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));
            _name = name;
            return this;
        }

        public TableOptions WithSchema(string schema = "dbo")
        {
            _schema = schema;
            return this;
        }

        public TableOptions AddField(Action<FieldOptions> builder)
        {
            var field = FieldOptionsBuilderExtensions.AddField(builder);
            return AddField(field);
        }

        public TableOptions AddField(Field field)
        {
            Throw<ArgumentNullException>(field != null, nameof(field));
            _fields.Add(field!.Name, field);
            return this;
        }

        internal protected Table Build()
        {
            // validate before returning. 
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(_name), nameof(Table.Name));
            Throw<ArgumentNullException>(_fields != null, nameof(Table.Fields));
            Throw<ArgumentOutOfRangeException>(_fields!.Any(), nameof(Table.Fields));

            return new Table(_name, _fields!.Values, _schema);
        }
    }
}

using Syrx.Commanders.Databases.Tests.Integration.Models.Immutable;

namespace Syrx.Commanders.Databases.Tests.Integration.Models
{
    public partial class ModelGenerators
    {
        public class ImmutableTypeOptions
        {
            private int _id;
            private string _name;
            private decimal _value = 1;
            private DateTime _modified = DateTime.Today;

            public ImmutableTypeOptions WithId(int id = 1)
            {
                _id = id;
                return this;
            }

            public ImmutableTypeOptions WithName(string name = "entry")
            {
                _name = name;
                return this;
            }

            public ImmutableTypeOptions WithValue(decimal value = 10)
            {
                _value = value;
                return this;
            }

            public ImmutableTypeOptions WithDate(DateTime? modified = null)
            {
                _modified = modified ?? DateTime.Today;
                return this;
            }

            protected internal ImmutableType Build()
            {
                return new ImmutableType(
                    _id,
                    _name ?? $"entry {_id}",
                    _value == 0 ? _value : (_id * 10),
                    _modified
                    );
            }
        }
    }
}


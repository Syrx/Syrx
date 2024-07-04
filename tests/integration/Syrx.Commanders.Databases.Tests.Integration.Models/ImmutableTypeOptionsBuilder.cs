using Syrx.Commanders.Databases.Tests.Integration.Models.Immutable;

namespace Syrx.Commanders.Databases.Tests.Integration.Models
{
    public partial class ModelGenerators
    {
        public class ImmutableTypeOptionsBuilder
        {
            public static ImmutableType Build(Action<ImmutableTypeOptions> builder)
            {
                var options = new ImmutableTypeOptions();
                builder(options);
                return options.Build();
            }

            public static IEnumerable<ImmutableType> Build(int instances = 1) => Enumerable.Range(1, instances).Select(x => Build(y => y.WithId(x)));


        }
    }
}


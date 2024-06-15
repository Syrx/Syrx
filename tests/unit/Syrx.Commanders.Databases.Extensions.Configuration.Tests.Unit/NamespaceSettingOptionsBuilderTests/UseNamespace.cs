namespace Syrx.Commanders.Databases.Extensions.Configuration.Tests.Unit.NamespaceSettingOptionsBuilderTests
{
    public class UseNamespace
    {
        private NamespaceSettingOptionsBuilder _builder;
        private const string CommandText = "test-command-text";
        private const string Alias = "test-alias";


        public UseNamespace()
        {
            _builder = new NamespaceSettingOptionsBuilder();
        }


        [Fact]
        public void NullActionThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => _builder.ForType<UseNamespace>(null));
            result.ArgumentNull("builder");
        }

        [Fact]
        public void Successfully()
        {
            var result = _builder
                .ForType<UseNamespace>(x => x.
                    ForMethod(nameof(Successfully),
                        y => y.UseCommandText(CommandText)
                              .UseConnectionAlias(Alias)));
            NotNull(result);
        }
    }
}

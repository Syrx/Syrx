namespace Syrx.Commanders.Databases.Extensions.Configuration.Builders
{
    public static class NamespaceSettingOptionsBuilderExtensions
    {
        public static NamespaceSettingOptions Build(Action<NamespaceSettingOptionsBuilder> builder)
        {
            Throw<ArgumentNullException>(builder != null, nameof(builder));
            var options = new NamespaceSettingOptionsBuilder();
            builder!(options);
            return options.Build();
        }
    }
}

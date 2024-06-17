namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class NamespaceSettingOptionsBuilderExtensions
    {
        public static NamespaceSetting Build(Action<NamespaceSettingOptionsBuilder> builder)
        {
            Throw<ArgumentNullException>(builder != null, nameof(builder));
            var options = new NamespaceSettingOptionsBuilder();
            builder!(options);
            return options.Build();
        }
    }
}

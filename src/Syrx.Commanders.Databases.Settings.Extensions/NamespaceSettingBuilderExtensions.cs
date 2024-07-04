namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class NamespaceSettingBuilderExtensions
    {
        public static NamespaceSetting Build(Action<NamespaceSettingBuilder> factory)
        {
            Throw<ArgumentNullException>(factory != null, nameof(factory));
            var builder = new NamespaceSettingBuilder();
            factory!(builder);
            return builder.Build();
        }
    }
}

namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class TypeSettingBuilderExtensions
    {
        public static TypeSetting Build<TType>(Action<TypeSettingBuilder<TType>> factory)
        {
            Throw<ArgumentNullException>(factory != null, nameof(factory));
            var builder = new TypeSettingBuilder<TType>();
            factory!(builder);
            return builder.Build();
        }
    }
}

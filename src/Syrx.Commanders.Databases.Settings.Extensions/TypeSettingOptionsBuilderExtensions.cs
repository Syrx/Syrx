namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class TypeSettingOptionsBuilderExtensions
    {
        public static TypeSetting Build<TType>(Action<TypeSettingOptionsBuilder<TType>> builder)
        {
            Throw<ArgumentNullException>(builder != null, nameof(builder));
            var options = new TypeSettingOptionsBuilder<TType>();
            builder!(options);
            return options.Build();
        }
    }
}

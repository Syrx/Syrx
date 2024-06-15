namespace Syrx.Commanders.Databases.Extensions.Configuration.Builders
{
    public static class TypeSettingOptionsBuilderExtensions
    {
        public static TypeSettingOptions Build<TType>(Action<TypeSettingOptionsBuilder<TType>> builder)
        {
            Throw<ArgumentNullException>(builder != null, nameof(builder));
            var options = new TypeSettingOptionsBuilder<TType>();
            builder!(options);
            return options.Build();
        }
    }
}

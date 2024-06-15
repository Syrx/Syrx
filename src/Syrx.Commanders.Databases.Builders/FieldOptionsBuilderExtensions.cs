namespace Syrx.Commanders.Databases.Builders
{
    public static class FieldOptionsBuilderExtensions
    {
        public static Field AddField(Action<FieldOptions> builder)
        {
            var options = new FieldOptions();
            builder.Invoke(options);
            return options.Build();
        }
    }
}

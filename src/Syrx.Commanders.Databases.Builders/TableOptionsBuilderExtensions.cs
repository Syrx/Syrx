namespace Syrx.Commanders.Databases.Builders
{
    public static class TableOptionsBuilderExtensions
    {
        public static Table Build(Action<TableOptions> builder)
        {
            var options = new TableOptions();
            builder(options);
            return options.Build();
        }
    }
}

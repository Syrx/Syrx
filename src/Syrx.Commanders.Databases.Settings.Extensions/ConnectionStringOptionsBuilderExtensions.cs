namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class ConnectionStringOptionsBuilderExtensions
    {
        public static ConnectionStringSetting Build(Action<ConnectionStringOptionsBuilder> builder)
        {
            var result = new ConnectionStringOptionsBuilder();
            builder(result);
            return result.Build();
        }
    }

}
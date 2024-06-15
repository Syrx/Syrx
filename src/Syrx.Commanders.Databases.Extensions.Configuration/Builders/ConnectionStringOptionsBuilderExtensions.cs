namespace Syrx.Commanders.Databases.Extensions.Configuration.Builders
{
    public static class ConnectionStringOptionsBuilderExtensions
    {
        public static ConnectionStringSettingOptions Build(Action<ConnectionStringOptionsBuilder> builder)
        {
            var result = new ConnectionStringOptionsBuilder();
            builder(result);
            return result.Build();
        }
    }

}
namespace Syrx.Commanders.Databases.Settings.Extensions
{
    public static class ConnectionStringBuilderExtensions
    {
        public static ConnectionStringSetting Build(Action<ConnectionStringSettingsBuilder> factory)
        {
            var builder = new ConnectionStringSettingsBuilder();
            factory(builder);
            return builder.Build();
        }
    }

}
// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (22:10)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

using Syrx.Commanders.Databases.Extensions;
using Syrx.Commanders.Databases.Settings.Readers.Extensions;
using System.Data.Common;

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions
{
    public static class SqlServerBuilderExtensions
    {
        public static SyrxOptionsBuilder UseSqlServer(
            this SyrxOptionsBuilder builder,
            Action<CommanderOptionsBuilder> factory,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var options = CommanderOptionsBuilderExtensions.Build(factory);
            builder.ServiceCollection
                .AddTransient<ICommanderSettings, CommanderSettings>(a => options)
                .AddReader(lifetime) // add reader
                .AddSqlServer(lifetime) // add connector
                //.AddDatabaseConnector<IDatabaseConnector, SqlServerDatabaseConnector>(lifetime) // example of how to us. 
                .AddDatabaseCommander(lifetime);
            
            return builder;
        }
        
    }
}
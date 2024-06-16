// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (22:10)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

using Syrx.Commanders.Databases.Extensions.Configuration;
using Syrx.Commanders.Databases.Extensions.Configuration.Builders;
using Syrx.Commanders.Databases.Settings.Readers;
using System.Data.Common;

namespace Syrx.Commanders.Databases.Connectors.SqlServer.Extensions
{
    public static class SqlServerOptionsBuilderExtensions
    {
        /*
        public static SyrxOptionsBuilder UseSqlServer(
            this SyrxOptionsBuilder builder,
            IDatabaseCommanderSettings settings,
            ServiceLifetime lifetime = ServiceLifetime.Transient
            )
        {
            builder.ServiceCollection
                .AddSqlServer(lifetime)
                .AddDatabaseCommander(settings, lifetime);
            return builder;
        }

        public static SyrxOptionsBuilder UseSqlServer(
            this SyrxOptionsBuilder builder,
            Action<DatabaseCommanderSettingsOptions> factory,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder.Build(factory);
            return builder.UseSqlServer(settings, lifetime);
        }
        */

        
        public static SyrxOptionsBuilder UseSqlServer(
            this SyrxOptionsBuilder builder,
            Action<CommanderOptionsBuilder> factory,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var options = CommanderOptionsBuilderExtensions.Build(factory);
            //var commander = Build<SyrxOptionsBuilder>(options);
            builder.ServiceCollection
                //.AddTransient<IDatabaseConnector, DatabaseConnector>() // add connector
                //.AddTransient<IDatabaseCommandReader, DatabaseCommandReader>() // reader
                //.TryAddToServiceCollection(typeof(IDatabaseConnector), typeof(SqlServerDatabaseConnector), lifetime)
                //.TryAddToServiceCollection(typeof(ICommander<>), typeof(DatabaseCommander<>), lifetime)
                //.AddTransient()
                //.AddSqlServer(lifetime)
                //.AddDatabaseCommander(options, lifetime)
                //.AddSingleton(options)
                //.AddProvider()
                .AddTransient<ICommanderOptions, CommanderOptions>(a => options)
                .AddReader(lifetime)
                .AddSqlServer(lifetime)
                //.AddDatabaseCommander(lifetime);
                .AddTransient(typeof(ICommander<>), typeof(DatabaseCommander<>));
                //.AddTransient(a => Build<SyrxOptionsBuilder>(options));
                
                

            return builder;
        }
        /*
        private static ICommander<T> Build<T>(CommanderOptions options)
        {
            var reader = new DatabaseCommandReader(options);
            var connector = new SqlServerDatabaseConnector(options);
            var result = new DatabaseCommander<T>(reader, connector);
            return result;
        }
        */

        public static IServiceCollection AddProvider(
            this IServiceCollection services,
            Func<DbProviderFactory> providerFactory)
        {
            return services.TryAddToServiceCollection(
                typeof(DbProviderFactory),
                providerFactory);
        }

        public static IServiceCollection AddDatabaseConnector<TService, TImplementation>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient
            ) where TService : IDatabaseConnector where TImplementation : class, IDatabaseConnector
        {
            return services.TryAddToServiceCollection(
                typeof(TService),
                typeof(TImplementation),
                lifetime);
        }

        internal static IServiceCollection AddSqlServer(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services.TryAddToServiceCollection(
                typeof(IDatabaseConnector),
                typeof(SqlServerDatabaseConnector),
                lifetime);
        }

        public static SyrxOptionsBuilder UseDatabaseCommander(
            this SyrxOptionsBuilder options
            )
        {
            options.ServiceCollection
                .AddDatabaseCommander();
            return options;
        }

        public static IServiceCollection AddDatabaseCommander(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            return services
                .AddReader(lifetime)
                .TryAddToServiceCollection(typeof(ICommander<>), typeof(DatabaseCommander<>), lifetime);

        }

        public static IServiceCollection AddReader(
           this IServiceCollection services,
           ServiceLifetime lifetime = ServiceLifetime.Transient)
        {            
            return services.TryAddToServiceCollection(
                typeof(IDatabaseCommandReader),
                typeof(DatabaseCommandReader),
                lifetime);
        }

    }
}
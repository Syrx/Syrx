using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Extensions;
using Syrx.Commanders.Databases.Settings;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Commanders.Databases.Settings.Readers.Extensions;
using Syrx.Extensions;
using System.Data.Common;
using static Syrx.Validation.Contract;

namespace Syrx.Commanders.Databases.Connectors.Extensions
{
    public static class DatabaseConnectorExtensions
    {
        
        /// <summary>
        /// Registers a singleton Syrx instance using a 
        /// specific DbProviderFactory delegate for the 
        /// default DatabaseConnector. 
        /// </summary>
        /// <param name="builder">The builder used to configure Syrx.</param>
        /// <param name="providerFactory">The delegate which will retun an instance of DbProviderFactory</param>
        /// <param name="commandFactory">The delegate which connfigures commands.</param>
        /// <returns></returns>
        public static SyrxBuilder UseConnector(
            this SyrxBuilder builder,
            Func<DbProviderFactory> providerFactory,
            Action<CommanderSettingsBuilder> settingsFactory
            )
        {
            // installs a default connection with a given provider

            Throw(settingsFactory != null,
                () => new ArgumentNullException(nameof(settingsFactory),
                $"The {nameof(CommanderSettings)} delegate cannot be null."));

            var settings = CommanderSettingsBuilderExtensions.Build(settingsFactory!);
            builder.ServiceCollection
                .AddProvider(providerFactory)
                .AddSingleton<ICommanderSettings, CommanderSettings>(a => settings)
                .AddReader(ServiceLifetime.Singleton) 
                .AddDatabaseConnector<IDatabaseConnector, DatabaseConnector>(ServiceLifetime.Singleton)                 
                .AddDatabaseCommander(ServiceLifetime.Singleton);
            return builder;
        }

    }
}

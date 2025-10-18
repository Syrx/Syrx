// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (19:59)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

using Microsoft.Extensions.DependencyInjection;

namespace Syrx.Extensions
{
    /// <summary>
    /// Provides a fluent builder interface for configuring Syrx services and their dependencies
    /// within a dependency injection container. This class serves as the entry point for
    /// registering database providers, command settings, and other Syrx-related services.
    /// </summary>
    /// <remarks>
    /// The SyrxBuilder is typically used within the UseSyrx() extension method to configure
    /// the framework. It encapsulates the service collection and provides extension points
    /// for database-specific configuration through provider-specific builder extensions.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.UseSyrx(builder => 
    /// {
    ///     builder.UseSqlServer(sqlServer => 
    ///     {
    ///         sqlServer.AddConnectionString("Default", connectionString);
    ///         sqlServer.AddCommand(/* configuration */);
    ///     });
    /// });
    /// </code>
    /// </example>
    public class SyrxBuilder
    {
        /// <summary>
        /// Gets the service collection that will be used to register Syrx services and dependencies.
        /// This collection is passed to database provider extensions for service registration.
        /// </summary>
        /// <value>
        /// The <see cref="IServiceCollection"/> instance used for dependency injection configuration.
        /// </value>
        public IServiceCollection ServiceCollection { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyrxBuilder"/> class with the specified service collection.
        /// </summary>
        /// <param name="services">
        /// The service collection to use for registering Syrx services and dependencies.
        /// If null, a new <see cref="ServiceCollection"/> will be created.
        /// </param>
        /// <remarks>
        /// This constructor is typically called by the UseSyrx() extension method and should not
        /// be instantiated directly in application code.
        /// </remarks>
        public SyrxBuilder(IServiceCollection services)
        {
            ServiceCollection = services ?? new ServiceCollection();
        }
    }
}
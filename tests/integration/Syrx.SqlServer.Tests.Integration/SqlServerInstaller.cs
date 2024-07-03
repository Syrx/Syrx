using Microsoft.Extensions.DependencyInjection;
using Syrx.Extensions;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace Syrx.SqlServer.Tests.Integration
{
    public class SqlServerInstaller 
    {
        public SyrxBuilder SyrxBuilder { get; }
        public IServiceProvider Provider { get; }

        public SqlServerInstaller(string connectionString)
        {
            var services = new ServiceCollection();
            var builder = new SyrxBuilder(services);
            SyrxBuilder = builder.SetupSqlServer(connectionString);
                        
            Provider = services.BuildServiceProvider();
            var commander = Provider.GetService<ICommander<DatabaseBuilder>>();
            var database = new DatabaseBuilder(commander);
            database.Build();
        }
    }
}

namespace Syrx.Samples
{
    public class Country
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
    }

    public interface ICountryRepository
    {
        // return a list of countries from a database. 
        Task<IEnumerable<Country>> RetrieveAllAsync(CancellationToken cancellationToken = default);
    }

    public class CountryRepository(ICommander<CountryRepository> commander) : ICountryRepository
    {
        // instance of our ICommander passed in via dependency injection. 
        private readonly ICommander<CountryRepository> _commander = commander;

        public async Task<IEnumerable<Country>> RetrieveAllAsync(CancellationToken cancellationToken = default)
        {
            return await _commander.QueryAsync<Country>(cancellationToken: cancellationToken);
        }
    }

    public static class SyrxInstaller
    {
        public static  IServiceCollection Install(this IServiceCollection services)
        {   
            return services.UseSyrx(builder =>          // call the UseSyrx extension method on IServiceCollection. 
                builder.UseSqlServer(sqlServer =>       // add support for the relevant provider. in this case, SQL Server. 
                    sqlServer.AddConnectionString("Example", "Data Source=xxx;Inital Catalog") // add a connection string to the database. 
                        .AddCommand(types => types.ForType<CountryRepository>( // start adding commands per repository type. 
                                methods => methods.ForMethod(nameof(CountryRepository.RetrieveAllAsync),  // reference the method that will execute the command. 
                                command => command // start building up the command
                                        .UseConnectionAlias("Example") // reference the connection string by the alias you provided earlier. 
                                        .UseCommandText("select * from [dbo].[Country];") // supply the SQL that you want to be executed. 
                                        )))));
        }
    }
}

using Microsoft.Extensions.Logging;

namespace Syrx.MySql.Tests.Integration
{
    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly MySqlContainer _container;


        public BaseFixture()
        {
            var _logger = LoggerFactory.Create(b => b.AddConsole().AddSystemdConsole().AddSimpleConsole()).CreateLogger<BaseFixture>();

            _container = new MySqlBuilder()
                .WithImage("mysql:8.0")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
                .WithReuse(true)
                .WithLogger(_logger)
                .WithStartupCallback((container, token) => 
                {
                    var message = @$"{new string('=', 150)}
Syrx: {nameof(MySqlContainer)} startup callback. Container details:
{new string('=', 150)}
Name ............. : {container.Name}
Id ............... : {container.Id}
State ............ : {container.State}
Health ........... : {container.Health}
CreatedTime ...... : {container.CreatedTime}
StartedTime ...... : {container.StartedTime}
Hostname ......... : {container.Hostname}
Image.Digest ..... : {container.Image.Digest}
Image.FullName ... : {container.Image.FullName}
Image.Registry ... : {container.Image.Registry}
Image.Repository . : {container.Image.Repository}
Image.Tag ........ : {container.Image.Tag}
IpAddress ........ : {container.IpAddress}
MacAddress ....... : {container.MacAddress}
ConnectionString . : {container.GetConnectionString()}
{new string('=', 150)}
";
                container.Logger.LogInformation(message);
                return Task.CompletedTask;
            }).Build();

            _container.StartAsync().Wait();
        }

        public async Task DisposeAsync()
        {
            await Task.Run(() => Console.WriteLine("Done"));
        }

        public async Task InitializeAsync()
        {
            // line up
            var connectionString = $"{ _container.GetConnectionString() };Allow User Variables=true";
            var installer = new MySqlInstaller(connectionString);
            _services = installer.Provider;
            await Task.CompletedTask;

        }

        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>()!;
        }

    }

}

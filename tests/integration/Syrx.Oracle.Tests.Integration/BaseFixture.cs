namespace Syrx.Oracle.Tests.Integration
{
    public class BaseFixture : IAsyncLifetime
    {
        private IServiceProvider _services;
        private readonly OracleContainer _container;

        public BaseFixture()
        {
            var _logger = LoggerFactory.Create(b => b.AddConsole().AddSystemdConsole().AddSimpleConsole()).CreateLogger<BaseFixture>();

            _container = new OracleBuilder()
            .WithImage("gvenzl/oracle-xe:21.3.0-slim-faststart")
            .WithReuse(true)
            .WithLogger(_logger)
            .WithStartupCallback((container, token) =>
            {
                var message = @$"{new string('=', 150)}
Syrx: {nameof(OracleContainer)} startup callback. Container details:
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
            })
            .Build();

            // start it up... 
            _container.StartAsync().Wait();
        }
                
        internal protected ICommander<TRepository> GetCommander<TRepository>()
        {
            return _services.GetService<ICommander<TRepository>>()!;
        }

        public async Task InitializeAsync()
        {
            var connectionString = _container.GetConnectionString();
            var installer = new OracleInstaller(connectionString);
            _services = installer.Provider;

            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            await Task.CompletedTask;
        }
    }


}

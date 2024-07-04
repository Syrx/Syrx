using Microsoft.Extensions.DependencyInjection;
using Syrx.Commanders.Databases.Settings.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Readers.Extensions.Tests.Unit.ServiceCollectionExtensionsTests
{
    public class AddReader
    {
        private readonly IServiceCollection _services;
        public AddReader()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void Successfully()
        {
            // need settings to allow the reader to resolve
            // (obviously - it's the command reader).  
            var settings = CommanderSettingsBuilderExtensions.Build(
                a => a.AddCommand(
                    b => b.ForType<AddReader>(
                        c => c.ForMethod(
                            nameof(Successfully), d => d
                            .UseCommandText("test-command")
                            .UseConnectionAlias("alias")))));
            _services
                .AddSingleton<ICommanderSettings, CommanderSettings>(a => settings)
                .AddReader();

            var provider = _services.BuildServiceProvider();
            var resolved = provider.GetService<IDatabaseCommandReader>();
            NotNull(resolved);
            IsType<DatabaseCommandReader>(resolved);
        }
    }
}

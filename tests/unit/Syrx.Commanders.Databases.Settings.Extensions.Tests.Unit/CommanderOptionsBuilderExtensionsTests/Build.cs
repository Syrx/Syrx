using Z = Syrx.Commanders.Databases.Settings.Extensions.CommanderOptionsBuilderExtensions;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommanderOptionsBuilderExtensionsTests
{
    public class Build
    {
        private const string CommandText = "test-command-text";
        private const string ConnectionString = "test-connection-string";
        private const string Alias = "test-alias";

        [Fact]
        public void DuplicateConnectionStringsSupported()
        {
            var result = Z.Build(x => x
                .AddConnectionString(Alias, ConnectionString)
                .AddConnectionString(Alias, ConnectionString)
                .AddCommand(c => c
                        .ForType<Build>(
                            t => t.ForMethod(
                                nameof(DuplicateConnectionsWithBuilderSupported),
                                o => o.UseConnectionAlias(Alias).UseCommandText(CommandText)))));

            Single(result.Connections!);
        }

        [Fact]
        public void DuplicateConnectionsWithBuilderSupported()
        {
            var result = Z.Build(x => x
                    .AddConnectionString(y => y.UseAlias(Alias).UseConnectionString(ConnectionString))
                    .AddConnectionString(y => y.UseAlias(Alias).UseConnectionString(ConnectionString))
                    .AddCommand(c => c
                        .ForType<Build>(
                            t => t.ForMethod(
                                nameof(DuplicateConnectionsWithBuilderSupported),
                                o => o.UseConnectionAlias(Alias).UseCommandText(CommandText)))));


            Single(result.Connections!);
        }

        [Fact]
        public void ThrowsArgumentExceptionOnConnectionStringConflict()
        {
            const string DifferentConnectionString = "different-connection-string";

            var result = Throws<ArgumentException>(() => Z.Build(x => x
                .AddConnectionString(Alias, ConnectionString)
                .AddConnectionString(Alias, DifferentConnectionString)));

            result.HasMessage(@$"The alias '{Alias}' is already assigned to a different connection string. 
Current connection string: {ConnectionString}
New connection string: {DifferentConnectionString}");
        }

        [Fact]
        public void AddCommandSuccessully()
        {
            var result = Z.Build(
                a => a
                .AddConnectionString(b => b.UseAlias(Alias).UseConnectionString(ConnectionString))
                .AddCommand(
                    c => c.ForType<Build>(
                        d => d.ForMethod(
                            nameof(AddCommandSuccessully),
                            e => e
                                .UseCommandText(CommandText)
                                .UseConnectionAlias(Alias)))));

            Single(result.Namespaces);
            result.PrintAsJson();
        }

        [Fact]
        public void CommandsInTheSameNamespaceShouldGroupTogether()
        {
            var result = Z.Build(
                a => a
                .AddCommand(
                    c => c.ForType<Build>(
                        d => d.ForMethod(
                            nameof(AddCommandSuccessully),
                            e => e
                                .UseCommandText(CommandText)
                                .UseConnectionAlias(Alias))))
                .AddCommand(
                    c => c.ForType<Build>(
                        d => d.ForMethod(
                            nameof(CommandsInTheSameNamespaceShouldGroupTogether),
                            e => e
                                .UseCommandText(CommandText)
                                .UseConnectionAlias(Alias)))));

            result.PrintAsJson();
            Single(result.Namespaces);
            Single(result.Namespaces.Single().Types);
            Equal(2, result.Namespaces.Single().Types.Single().Commands.Count());
            result.PrintAsJson();
        }

        [Fact]
        public void AcceptsLastEntryFromBuilder()
        {
            var result = Z.Build(x => x
                    .AddCommand(c => c
                        .ForType<Build>(
                            t => t.ForMethod(
                                nameof(AcceptsLastEntryFromBuilder),
                                o => o.UseConnectionAlias(Alias).UseCommandText(CommandText))))
                    .AddCommand(c => c
                        .ForType<Build>(
                            t => t.ForMethod(
                                nameof(AcceptsLastEntryFromBuilder),
                                o => o.UseConnectionAlias(Alias).UseCommandText("test-2")))));

            result.PrintAsJson();
            Single(result.Namespaces!);
            Single(result.Namespaces.Single().Types);

        }

        [Fact]
        public void AcceptsChainedMethods()
        {
            var result = Z.Build(x => x
                    .AddCommand(c => c
                        .ForType<Build>(
                            t => t
                            .ForMethod("Method1", o => o.UseConnectionAlias(Alias).UseCommandText(CommandText))
                            .ForMethod("Method2", o => o.UseConnectionAlias(Alias).UseCommandText(CommandText))
                            .ForMethod("Method3", o => o.UseConnectionAlias(Alias).UseCommandText(CommandText))
                            .ForMethod("Method4", o => o.UseConnectionAlias(Alias).UseCommandText(CommandText))
                            )));

            result.PrintAsJson();
            Single(result.Namespaces);
            Single(result.Namespaces.Single().Types);
            Equal(4, result.Namespaces.Single().Types.Single().Commands.Count);
        }

        [Fact]
        public void WithoutBuilder()
        {
            var command = Z.Build(x => x
                .AddCommand(a => a
                    .ForType<Build>(b => b
                        .ForMethod(nameof(WithoutBuilder),
                            c => c.UseCommandText(CommandText)
                                  .UseConnectionAlias(Alias)))));

            var result = Z.Build(a => a.AddCommand(command.Namespaces.Single()));
        }
    }
}

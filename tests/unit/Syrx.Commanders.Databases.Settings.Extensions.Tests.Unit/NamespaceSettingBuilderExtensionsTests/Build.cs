using B = Syrx.Commanders.Databases.Settings.Extensions.NamespaceSettingBuilderExtensions;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.NamespaceSettingBuilderExtensionsTests
{
    public class Build
    {
        private const string CommandText = "test-command-text";
        private const string Alias = "test-alias";


        [Fact]
        public void NullBuilderThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => B.Build(null));
            result.ArgumentNull("factory");
        }

        [Fact]
        public void Successfully()
        {
            var result = B.Build(
                x => x.ForType<Build>(
                    y => y.ForMethod(nameof(Successfully),
                        z => z.UseCommandText(CommandText)
                              .UseConnectionAlias(Alias))));
            NotNull(result);
        }

        [Fact]
        public void SupportsChainedMethodsPerType()
        {
            var result = B.Build(
                x => x.ForType<Build>(
                    y => y.ForMethod(nameof(SupportsChainedMethodsPerType), z => z.UseCommandText(CommandText).UseConnectionAlias(Alias))
                          .ForMethod("AnotherTestMethod", z => z.UseConnectionAlias(Alias).UseCommandText("more-command-text"))));
            NotNull(result);
            Single(result.Types);
            Equal(2, result.Types.Single().Commands.Count);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceMethodNameThrowsArumentNullException(string method)
        {
            var result = Throws<ArgumentNullException>(() =>
                B.Build(
                    a => a.ForType<Build>(
                        b => b.ForMethod(method,
                        c => c.UseCommandText(CommandText)
                              .UseConnectionAlias(Alias)))));
            result.ArgumentNull(nameof(method));
        }

        [Fact]
        public void AcceptsLastEntryFromBuilder()
        {
            var result = B.Build(
                    a => a.ForType<Build>(
                        b => b.ForMethod(nameof(AcceptsLastEntryFromBuilder), c => c.UseCommandText(CommandText).UseConnectionAlias(Alias))
                              .ForMethod(nameof(AcceptsLastEntryFromBuilder), c => c.UseCommandText(CommandText).UseConnectionAlias(Alias))));

            Single(result.Types);
            Single(result.Types.Single().Commands);
        }


        [Fact]
        public void GroupsTypes()
        {
            var result = B.Build(
                x => x.ForType<Build>(
                    y => y.ForMethod(nameof(Successfully),
                        z => z.UseCommandText(CommandText)
                              .UseConnectionAlias(Alias)))
                .ForType<Build>(
                    y => y.ForMethod(nameof(GroupsTypes),
                        z => z.UseCommandText(CommandText)
                              .UseConnectionAlias(Alias))));
            NotNull(result);
            Single(result.Types);
        }
    }
}

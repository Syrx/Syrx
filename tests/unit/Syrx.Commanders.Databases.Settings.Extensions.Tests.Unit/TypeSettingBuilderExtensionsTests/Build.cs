using B = Syrx.Commanders.Databases.Settings.Extensions.TypeSettingBuilderExtensions;

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.TypeSettingBuilderExtensionsTests
{
    public class Build
    {
        private const string CommandText = "test-command-text";
        private const string Alias = "test-alias";


        [Fact]
        public void ForGenericType()
        {
            var result = B.Build<Build>(x => x
                .ForMethod(
                    nameof(ForGenericType),
                    x => x.UseCommandText(CommandText)
                          .UseConnectionAlias(Alias)));

            // that we've not thrown an exception is kinda the point. 
            NotNull(result);
        }

        [Fact]
        public void ForSpecifiedType()
        {
            var type = typeof(Build);
            var method = nameof(ForSpecifiedType);

            var result = B.Build<Build>(x => x.ForMethod(method, y => y.UseCommandText(CommandText).UseConnectionAlias(Alias)));
            NotNull(result);
        }


        [Fact]
        public void NullActionThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => B.Build<Build>(x => x.ForMethod(nameof(NullActionThrowsArgumentNullException), null)));
            result.ArgumentNull("builder");
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceMethodNameThrowsArgumentNullException(string method)
        {
            Action<CommandSettingBuilder> builder = (x) => x.UseConnectionAlias(Alias).UseCommandText(CommandText);

            var result = Throws<ArgumentNullException>(() => B.Build<Build>(x => x.ForMethod(method, builder)));
            result.ArgumentNull(nameof(method));
        }
    }
}

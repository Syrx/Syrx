namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.TypeSettingBuilderTests
{
    public class ForType
    {
        private TypeSettingBuilder<ForType> _builder;
        private const string CommandText = "test-command-text";
        private const string Alias = "test-alias";

        public ForType()
        {
            _builder = new TypeSettingBuilder<ForType>();
        }

        [Fact]
        public void ForGenericType()
        {
            var result = _builder
                .ForMethod(
                    nameof(ForGenericType),
                    x => x.UseCommandText(CommandText)
                          .UseConnectionAlias(Alias));

            // that we've not thrown an exception is kinda the point. 
            NotNull(result);
        }

        [Fact]
        public void ForSpecifiedType()
        {
            var type = typeof(ForType);
            var method = nameof(ForSpecifiedType);

            Action<CommandSettingBuilder> builder = (x) => x.UseCommandText(CommandText).UseConnectionAlias(Alias);

            var result = _builder.ForMethod(method, builder);
            NotNull(result);
        }


        [Fact]
        public void NullActionThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => _builder.ForMethod(nameof(NullActionThrowsArgumentNullException), null));
            result.ArgumentNull("builder");
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceMethodNameThrowsArgumentNullException(string method)
        {
            Action<CommandSettingBuilder> builder = (x) => x.UseConnectionAlias(Alias).UseCommandText(CommandText);

            var result = Throws<ArgumentNullException>(() => _builder.ForMethod(method, builder));
            result.ArgumentNull(nameof(method));
        }
    }
}

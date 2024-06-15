namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class ForRepositoryType
    {
        private const string CommandText = "test-command-text";

        [Fact]
        public void NullTypeThrowsArgumentNullException()
        {

            var result = Throws<ArgumentNullException>(() => DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType(null)
                    .ForMethodNamed(nameof(NullTypeThrowsArgumentNullException))
                    .UseCommandText(CommandText)));
            result.ArgumentNull("type");
        }

        [Fact]
        public void SuccessfullyWithSuppliedType()
        {
            var type = typeof(ForRepositoryType);
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType(type)
                    .ForMethodNamed(nameof(SuccessfullyWithSuppliedType))
                    .UseCommandText(CommandText));

            //Equal(type, result.CommandSetting.Type);
            //True(false);
        }

        [Fact]
        public void SuccessfullyWithGenericType()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<ForRepositoryType>()
                    .ForMethodNamed(nameof(SuccessfullyWithGenericType))
                    .UseCommandText(CommandText));
            //True(false);
        }
    }
}

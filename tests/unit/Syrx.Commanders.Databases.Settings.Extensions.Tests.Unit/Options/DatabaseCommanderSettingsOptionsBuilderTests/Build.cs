namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommanderSettingsOptionsBuilderTests
{
    public class Build
    {
        // NOTE:
        // DatabaseCommanderSettingsOptions can only be tested via the DatabaseCommanderSettingsOptionsBuilder. 
        // this is because the Build method on DatabaseCommanderSettingsOptions is internal (deliberately). 
        // as such, we'll cover the various scenarios here. 


        private readonly DatabaseCommanderSettingsOptions _options;

        public Build()
        {
            _options = new DatabaseCommanderSettingsOptions();
        }

        [Fact]
        public void EmptyActionThrowsArgumentException()
        {
            var result = Throws<ArgumentException>(() => DatabaseCommanderSettingsOptionsBuilder.Build(null));
            result.HasMessage("At least 1 DatabaseCommandNamespaceSetting was expected to be passed to the DatabaseCommanderSettings constructor.");
        }

        [Fact]
        public void Succesfully()
        {
            var result = DatabaseCommanderSettingsOptionsBuilder.Build(
                a => a.AddConnectionString("test-alias", "test-connection-string")
                .AddCommand(b => b
                    .ForRepositoryType<Build>()
                    .ForMethodNamed(nameof(Succesfully))
                    .UseCommandText("test-commandtext")
                    .UsingCommandType(CommandType.StoredProcedure)
                    .WithCommandFlags(CommandFlagSetting.Buffered | CommandFlagSetting.Pipelined)
                    .WithIsolationLevel(IsolationLevel.ReadUncommitted)
                    ));

            NotNull(result);
        }
    }
}

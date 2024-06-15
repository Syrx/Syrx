namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommanderSettingsOptionsTests
{
    public class AddCommand
    {
        private readonly DatabaseCommanderSettingsOptions _options;
        private const string CommandText = "select 1;";
        private const string Alias = "test.alias";
        private const string ConnectionString = "test-connection-string";

        public AddCommand()
        {
            _options = new DatabaseCommanderSettingsOptions();
        }

        // exceptions
        // no method call (defaults)
        // method call (with defaults)
        // method call (with supplied)


        #region / ForType / 

        [Fact]
        public void NullTypeThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => _options.AddCommand(x => x.ForRepositoryType(null)));
            result.ArgumentNull("type");
        }

        [Fact]
        public void NoTypeSetThrowsInvalidOperationException()
        {
            var result = Throws<InvalidOperationException>(() =>
                _options.AddCommand(x
                    => x.ForMethodNamed(nameof(NoTypeSetThrowsInvalidOperationException))
                        .UseCommandText(CommandText)));

            result.HasMessage($"Please map the command to the calling type using the '{nameof(DatabaseCommandSettingOptionsBuilder.ForRepositoryType)}<TType>()' method.");
        }

        [Fact]
        public void SuccessfullyWithSuppliedType()
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder
                .Build(builder => builder
                        .AddConnectionString(Alias, ConnectionString)
                        .AddCommand(x => x
                            .ForRepositoryType(typeof(AddCommand))
                            .ForMethodNamed(nameof(SuccessfullyWithSuppliedType))
                            .UseCommandText(CommandText)));

            var result = settings.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == typeof(AddCommand).FullName))
                .SelectMany(a => a.Commands)
                .Single(b => b.Key == nameof(SuccessfullyWithSuppliedType)).Value;

            Equal(typeof(AddCommand).FullName, result.ConnectionAlias);
            Equal(CommandText, result.CommandText);
            Equal(30, result.CommandTimeout);
            Equal(CommandType.Text, result.CommandType);
            Equal((CommandFlagSetting) 5, result.Flags);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
            Null(result.Split);
        }

        [Fact]
        public void SuccessfullyWithGenericType()
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder
                .Build(builder =>
                    builder
                        .AddConnectionString(Alias, ConnectionString)
                        .AddCommand(x => x
                            .ForRepositoryType<AddCommand>()
                            .ForMethodNamed(nameof(SuccessfullyWithGenericType))
                            .UseCommandText(CommandText)
                            .AgainstConnectionAlias(Alias))
                        );

            var result = settings.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == typeof(AddCommand).FullName))
                .SelectMany(a => a.Commands)
                .Single(b => b.Key == nameof(SuccessfullyWithGenericType)).Value;

            Equal(Alias, result.ConnectionAlias);
            Equal(CommandText, result.CommandText);
            Equal(30, result.CommandTimeout);
            Equal(CommandType.Text, result.CommandType);
            Equal((CommandFlagSetting) 5, result.Flags);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
            Null(result.Split);

        }

        [Fact]
        public void SuccessfullyWithGenericTypeAndOptionalDefaults()
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder
                .Build(builder =>
                    builder
                        .AddConnectionString(Alias, ConnectionString)
                        .AddCommand(x => x
                            .ForRepositoryType<AddCommand>()
                            .ForMethodNamed(nameof(SuccessfullyWithGenericType))
                            .AgainstConnectionAlias(Alias)
                            .UseCommandText(CommandText)
                            .UsingCommandType()
                            .WithCommandTimeout()
                            .SplitResultsOn()
                            .WithCommandFlags()
                            .WithIsolationLevel()));

            var result = settings.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == typeof(AddCommand).FullName))
                .SelectMany(a => a.Commands)
                .Single(b => b.Key == nameof(SuccessfullyWithGenericType)).Value;

            Equal(Alias, result.ConnectionAlias);
            Equal(CommandText, result.CommandText);
            Equal(30, result.CommandTimeout);
            Equal(CommandType.Text, result.CommandType);
            Equal((CommandFlagSetting) 5, result.Flags);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
            Null(result.Split);
        }


        [Fact]
        public void SuccessfullyWithGenericTypeAndSuppliedOptions()
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder
                .Build(builder =>
                    builder
                        .AddConnectionString(Alias, ConnectionString)
                        .AddCommand(x => x
                            .ForRepositoryType<AddCommand>()
                            .ForMethodNamed(nameof(SuccessfullyWithGenericType))
                            .AgainstConnectionAlias(Alias)
                            .UseCommandText(CommandText)
                            .UsingCommandType(CommandType.StoredProcedure)
                            .WithCommandTimeout(45)
                            .SplitResultsOn("ForType")
                            .WithCommandFlags(CommandFlagSetting.Pipelined)
                            .WithIsolationLevel(IsolationLevel.Chaos)));

            var result = settings.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == typeof(AddCommand).FullName))
                .SelectMany(a => a.Commands)
                .Single(b => b.Key == nameof(SuccessfullyWithGenericType)).Value;

            Equal(Alias, result.ConnectionAlias);
            Equal(CommandText, result.CommandText);
            Equal(45, result.CommandTimeout);
            Equal(CommandType.StoredProcedure, result.CommandType);
            Equal(CommandFlagSetting.Pipelined, result.Flags);
            Equal(IsolationLevel.Chaos, result.IsolationLevel);
            Equal("ForType", result.Split);
        }

        #endregion

        #region / Named / 

        [Fact]
        public void MissingNameThrowsInvalidOperationException()
        {
            var result = Throws<InvalidOperationException>(() =>
                _options.AddCommand(
                    x => x
                        .ForRepositoryType<AddCommand>()
                        .UseCommandText(CommandText)));

            result.HasMessage($"Please set the command name using the '{nameof(DatabaseCommandSettingOptionsBuilder.ForMethodNamed)}()' method.");
        }

        [Fact]
        public void SuccessfullyWithNameAndOptionalDefaults()
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder
                .Build(builder =>
                    builder
                        .AddConnectionString(Alias, ConnectionString)
                        .AddCommand(x => x
                            .ForRepositoryType<AddCommand>()
                            .ForMethodNamed(nameof(SuccessfullyWithNameAndOptionalDefaults))
                            .AgainstConnectionAlias(Alias)
                            .UseCommandText(CommandText)
                            .UsingCommandType()
                            .WithCommandTimeout()
                            .SplitResultsOn()
                            .WithCommandFlags()
                            .WithIsolationLevel()));

            var result = settings.Namespaces
                .SelectMany(x => x.Types.Where(y => y.Name == typeof(AddCommand).FullName))
                .SelectMany(a => a.Commands)
                .Single(b => b.Key == nameof(SuccessfullyWithNameAndOptionalDefaults)).Value;

            Equal(Alias, result.ConnectionAlias);
            Equal(CommandText, result.CommandText);
            Equal(30, result.CommandTimeout);
            Equal(CommandType.Text, result.CommandType);
            Equal((CommandFlagSetting) 5, result.Flags);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
            Null(result.Split);
        }

        #endregion

        #region / AgainstConnectionAlias /



        #endregion

        #region / UseCommandText /



        #endregion

        #region / UsingCommandType /



        #endregion

        #region  / WithCommandTimeout /



        #endregion

        #region / SplitResultsOn /



        #endregion

        #region / WithCommandFlags /

        #endregion

        #region / WithIsolationLevel /

        #endregion

        [Fact]
        public void Massive()
        {
            var settings = DatabaseCommanderSettingsOptionsBuilder
                .Build(builder => builder.AddConnectionString(Alias, ConnectionString)
                    .AddCommand(x => x
                        .ForRepositoryType<AddCommand>()
                        .ForMethodNamed(nameof(Massive))
                        .UseCommandText(CommandText)
                        .AgainstConnectionAlias(Alias)
                        .SplitResultsOn()
                        .WithCommandFlags()
                        .WithCommandTimeout()
                        .UsingCommandType()
                        .WithIsolationLevel()
                    ));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceCommandTextThrowsArgumentNullException(string commandText)
        {
            var result = Throws<ArgumentNullException>(() => _options.AddCommand(x => x.UseCommandText(commandText)));
            result.ArgumentNull(nameof(commandText));
        }


        [Fact]
        public void OptionalAilasWithoutTypeThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(()
                => _options.AddCommand(x => x.AgainstConnectionAlias().UseCommandText(CommandText)));
            result.ArgumentNull("connectionAlias");
        }


    }
}

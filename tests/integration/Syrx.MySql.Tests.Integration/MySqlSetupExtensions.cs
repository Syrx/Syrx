namespace Syrx.MySql.Tests.Integration
{
    public static class MySqlSetupExtensions
    {

        public static SyrxBuilder SetupMySql(this SyrxBuilder builder, string connectionString)
        {
            return builder.UseMySql(
                            b => b
                            .AddConnectionStrings(connectionString)
                            .AddSetupBuilderOptions()
                            .AddQueryMultimap()
                            .AddQueryMultiple()
                            .AddExecute()
                            .AddDisposeCommands()
                            );
        }

        public static CommanderSettingsBuilder AddConnectionStrings(this CommanderSettingsBuilder builder, string connectionString)
        {
            return builder
                .AddConnectionString(a => a
                    .UseAlias(MySqlCommandStrings.Alias)
                    .UseConnectionString(connectionString));
        }

        public static CommanderSettingsBuilder AddSetupBuilderOptions(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<DatabaseBuilder>(
                    b => b
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateDatabase), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateDatabase))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableCreatorProcedure), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.DropTableCreatorProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableCreatorProcedure), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateTableCreatorProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTable), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .SetCommandType(CommandType.StoredProcedure)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateIdentityTesterProcedure), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertProcedure), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateBulkInsertAndReturnProcedure))                    
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableClearingProcedure), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.CreateTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.ClearTable), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .SetCommandType(CommandType.StoredProcedure)
                        .UseCommandText(MySqlCommandStrings.Setup.ClearTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.Populate), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Setup.Populate))
                    ));
        }

        public static CommanderSettingsBuilder AddQueryMultimap(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                    b => b.ForType<Query>(
                        c => c
                        .ForMethod(
                            nameof(Query.ExceptionsAreReturnedToCaller), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.ExceptionsAreReturnedToCaller))
                        .ForMethod(
                            nameof(Query.SingleType), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.SingleType))
                        .ForMethod(
                            nameof(Query.SingleTypeWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.SingleTypeWithParameters))
                        .ForMethod(
                            nameof(Query.TwoTypes), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.TwoTypes))
                        .ForMethod(
                            nameof(Query.TwoTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.TwoTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThreeTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.ThreeTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.FourTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FiveTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.FiveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.SixTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SevenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.SevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.EightTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.EightTypesWithParameters))
                        .ForMethod(
                            nameof(Query.NineTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.NineTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.TenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ElevenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.ElevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TwelveTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.TwelveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThirteenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.ThirteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourteenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.FourteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FifteenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.FifteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixteenTypesWithParameters), d => d
                            .UseConnectionAlias(MySqlCommandStrings.Alias)
                            .UseCommandText(MySqlCommandStrings.Query.Multimap.SixteenTypesWithParameters))));

        }

        public static CommanderSettingsBuilder AddQueryMultiple(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Query>(c => c
                .ForMethod(
                    nameof(Query.OneTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.OneTypeMultiple))
                .ForMethod(
                    nameof(Query.TwoTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.TwoTypeMultiple))
                .ForMethod(
                    nameof(Query.ThreeTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.ThreeTypeMultiple))
                .ForMethod(
                    nameof(Query.FourTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.FourTypeMultiple))
                .ForMethod(
                    nameof(Query.FiveTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.FiveTypeMultiple))
                .ForMethod(
                    nameof(Query.SixTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.SixTypeMultiple))
                .ForMethod(
                    nameof(Query.SevenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.SevenTypeMultiple))
                .ForMethod(
                    nameof(Query.EightTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.EightTypeMultiple))
                .ForMethod(
                    nameof(Query.NineTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.NineTypeMultiple))
                .ForMethod(
                    nameof(Query.TenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.TenTypeMultiple))
                .ForMethod(
                    nameof(Query.ElevenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.ElevenTypeMultiple))
                .ForMethod(
                    nameof(Query.TwelveTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.TwelveTypeMultiple))
                .ForMethod(
                    nameof(Query.ThirteenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.ThirteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FourteenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.FourteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FifteenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.FifteenTypeMultiple))
                .ForMethod(
                    nameof(Query.SixteenTypeMultiple), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Query.Multiple.SixteenTypeMultiple))));
        }

        public static CommanderSettingsBuilder AddExecute(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Execute>(c => c
                .ForMethod(
                    nameof(Execute.ExceptionsAreReturnedToCaller), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.ExceptionsAreReturnedToCaller))
                .ForMethod(
                    nameof(Execute.SupportParameterlessCalls), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportParameterlessCalls))
                .ForMethod(
                    $"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count", d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportsRollbackOnParameterlessCallsCount))
                .ForMethod(
                    nameof(Execute.SupportsRollbackOnParameterlessCalls), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportsRollbackOnParameterlessCalls))
                .ForMethod(
                    nameof(Execute.SupportsSuppressedDistributedTransactions), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportsSuppressedDistributedTransactions))
                .ForMethod(
                    $"{nameof(Execute.SupportsTransactionRollback)}.Count", d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportsTransactionRollbackCount))
                .ForMethod(
                    nameof(Execute.SupportsTransactionRollback), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportsTransactionRollback))
                .ForMethod(
                    nameof(Execute.SupportsEnlistingInAmbientTransactions), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SupportsEnlistingInAmbientTransactions))
                .ForMethod(
                    nameof(Execute.SuccessfullyWithResponse), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SuccessfullyWithResponse))
                .ForMethod(
                    $"{nameof(Execute.SuccessfullyWithResponse)}.Response", d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SuccessfullyWithResponseResponse))
                .ForMethod(
                    nameof(Execute.Successful), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.Successful))
                .ForMethod(
                    nameof(Execute.SingleType), d => d
                    .UseConnectionAlias(MySqlCommandStrings.Alias)
                    .UseCommandText(MySqlCommandStrings.Execute.SingleType))
                ));
        }

        public static CommanderSettingsBuilder AddDisposeCommands(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<Dispose>(b => b
                    .ForMethod(
                        nameof(Dispose.Successfully), c => c
                        .UseConnectionAlias(MySqlCommandStrings.Alias)
                        .UseCommandText(MySqlCommandStrings.Dispose.Successfully))));
        }
    }
}

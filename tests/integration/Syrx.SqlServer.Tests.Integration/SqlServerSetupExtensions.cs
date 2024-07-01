namespace Syrx.SqlServer.Tests.Integration
{
    public static class SqlServerSetupExtensions
    {
        public static SyrxBuilder SetupSqlServer(this SyrxBuilder builder, string connectionString = null)
        {
            return builder.UseSqlServer(
                            b => b
                            .AddConnectionStrings(connectionString)
                            .AddSetupBuilderOptions()
                            .AddQueryMultimap()
                            .AddQueryMultiple()
                            .AddExecute()
                            .AddDisposeCommands()
                            );
        }

        public static CommanderSettingsBuilder AddConnectionStrings(this CommanderSettingsBuilder builder, string connectionString = null)
        {
            return builder
                .AddConnectionString(a => a
                    .UseAlias(SqlServerCommandStrings.Instance)
                    .UseConnectionString(connectionString))
                .AddConnectionString(a => a
                    .UseAlias(SqlServerCommandStrings.Alias)
                    .UseConnectionString(connectionString));
        }

        public static CommanderSettingsBuilder AddSetupBuilderOptions(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<DatabaseBuilder>(
                    b => b
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateDatabase), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Instance)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateDatabase))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableCreatorProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.DropTableCreatorProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableCreatorProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateTableCreatorProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTable), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .SetCommandType(CommandType.StoredProcedure)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropIdentityTesterProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.DropIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateIdentityTesterProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.DropBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.DropBulkInsertAndReturnProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateBulkInsertAndReturnProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableClearingProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.DropTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableClearingProcedure), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.CreateTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.ClearTable), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .SetCommandType(CommandType.StoredProcedure)
                        .UseCommandText(SqlServerCommandStrings.Setup.ClearTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.Populate), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Setup.Populate))
                    ));
        }

        public static CommanderSettingsBuilder AddQueryMultimap(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                    b => b.ForType<Query>(
                        c => c
                        .ForMethod(
                            nameof(Query.ExceptionsAreReturnedToCaller), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.ExceptionsAreReturnedToCaller))
                        .ForMethod(
                            nameof(Query.SingleType), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.SingleType))
                        .ForMethod(
                            nameof(Query.SingleTypeWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.SingleTypeWithParameters))
                        .ForMethod(
                            nameof(Query.TwoTypes), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.TwoTypes))
                        .ForMethod(
                            nameof(Query.TwoTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.TwoTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThreeTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.ThreeTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.FourTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FiveTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.FiveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.SixTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SevenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.SevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.EightTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.EightTypesWithParameters))
                        .ForMethod(
                            nameof(Query.NineTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.NineTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.TenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ElevenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.ElevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TwelveTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.TwelveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThirteenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.ThirteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourteenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.FourteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FifteenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.FifteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixteenTypesWithParameters), d => d
                            .UseConnectionAlias(SqlServerCommandStrings.Alias)
                            .UseCommandText(SqlServerCommandStrings.Query.Multimap.SixteenTypesWithParameters))));

        }

        public static CommanderSettingsBuilder AddQueryMultiple(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Query>(c => c
                .ForMethod(
                    nameof(Query.OneTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.OneTypeMultiple))
                .ForMethod(
                    nameof(Query.TwoTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.TwoTypeMultiple))
                .ForMethod(
                    nameof(Query.ThreeTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.ThreeTypeMultiple))
                .ForMethod(
                    nameof(Query.FourTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.FourTypeMultiple))
                .ForMethod(
                    nameof(Query.FiveTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.FiveTypeMultiple))
                .ForMethod(
                    nameof(Query.SixTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.SixTypeMultiple))
                .ForMethod(
                    nameof(Query.SevenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.SevenTypeMultiple))
                .ForMethod(
                    nameof(Query.EightTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.EightTypeMultiple))
                .ForMethod(
                    nameof(Query.NineTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.NineTypeMultiple))
                .ForMethod(
                    nameof(Query.TenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.TenTypeMultiple))
                .ForMethod(
                    nameof(Query.ElevenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.ElevenTypeMultiple))
                .ForMethod(
                    nameof(Query.TwelveTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.TwelveTypeMultiple))
                .ForMethod(
                    nameof(Query.ThirteenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.ThirteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FourteenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.FourteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FifteenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.FifteenTypeMultiple))
                .ForMethod(
                    nameof(Query.SixteenTypeMultiple), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Query.Multiple.SixteenTypeMultiple))));
        }

        public static CommanderSettingsBuilder AddExecute(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Execute>(c => c
                .ForMethod(
                    nameof(Execute.ExceptionsAreReturnedToCaller), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.ExceptionsAreReturnedToCaller))
                .ForMethod(
                    nameof(Execute.SupportParameterlessCalls), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportParameterlessCalls))
                .ForMethod(
                    $"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count", d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportsRollbackOnParameterlessCallsCount))
                .ForMethod(
                    nameof(Execute.SupportsRollbackOnParameterlessCalls), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportsRollbackOnParameterlessCalls))
                .ForMethod(
                    nameof(Execute.SupportsSuppressedDistributedTransactions), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportsSuppressedDistributedTransactions))
                .ForMethod(
                    $"{nameof(Execute.SupportsTransactionRollback)}.Count", d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportsTransactionRollbackCount))
                .ForMethod(
                    nameof(Execute.SupportsTransactionRollback), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportsTransactionRollback))
                .ForMethod(
                    nameof(Execute.SupportsEnlistingInAmbientTransactions), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SupportsEnlistingInAmbientTransactions))
                .ForMethod(
                    nameof(Execute.SuccessfullyWithResponse), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SuccessfullyWithResponse))
                .ForMethod(
                    $"{nameof(Execute.SuccessfullyWithResponse)}.Response", d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SuccessfullyWithResponseResponse))
                .ForMethod(
                    nameof(Execute.Successful), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.Successful))
                .ForMethod(
                    nameof(Execute.SingleType), d => d
                    .UseConnectionAlias(SqlServerCommandStrings.Alias)
                    .UseCommandText(SqlServerCommandStrings.Execute.SingleType))
                ));
        }

        public static CommanderSettingsBuilder AddDisposeCommands(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<Dispose>(b => b
                    .ForMethod(
                        nameof(Dispose.Successfully), c => c
                        .UseConnectionAlias(SqlServerCommandStrings.Alias)
                        .UseCommandText(SqlServerCommandStrings.Dispose.Successfully))));
        }
    }

}

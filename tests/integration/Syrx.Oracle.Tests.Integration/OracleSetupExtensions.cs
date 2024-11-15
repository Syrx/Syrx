namespace Syrx.Oracle.Tests.Integration
{

    public static class OracleSetupExtensions
    {
        public static SyrxBuilder SetupOracle(this SyrxBuilder builder, string connectionString = null)
        {
            return builder.UseOracle(
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
                    .UseAlias(OracleCommandStrings.Alias)
                    .UseConnectionString(connectionString));
        }

        public static CommanderSettingsBuilder AddSetupBuilderOptions(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<DatabaseBuilder>(
                    b => b                
                    .ForMethod(
                        nameof(DatabaseBuilder.CreatePocoTable), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreatePocoTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateWritesTable), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateWritesTable))
                    .ForMethod(
                        "DropPocoTable", c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropPocoTable))
                    .ForMethod(
                        "DropWritesTable", c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropWritesTable))
                    .ForMethod(
                        "DropIdentityInsertTable", c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropIdentityInsertTable))
                    .ForMethod(
                        "DropBulkInsertTable", c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropBulkInsertTable))
                    .ForMethod(
                        "DropDistributeedTransactionTable", c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropDistributeedTransactionTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateIdentityTesterTable), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateIdentityTesterTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertTable), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateBulkInsertTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateDistributedTransactionTable), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateDistributedTransactionTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.TableChecker), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.TableChecker))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropIdentityTesterProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateIdentityTesterProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropBulkInsertAndReturnProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateBulkInsertAndReturnProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableClearingProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.DropTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableClearingProcedure), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.CreateTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.ClearTable), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.ClearTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.Populate), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Setup.Populate))
                    ));
        }

        public static CommanderSettingsBuilder AddQueryMultimap(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                    b => b.ForType<Query>(
                        c => c
                        .ForMethod(
                            nameof(Query.ExceptionsAreReturnedToCaller), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.ExceptionsAreReturnedToCaller))
                        .ForMethod(
                            nameof(Query.SingleType), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.SingleType))
                        .ForMethod(
                            nameof(Query.SingleTypeWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.SingleTypeWithParameters))
                        .ForMethod(
                            nameof(Query.TwoTypes), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.TwoTypes))
                        .ForMethod(
                            nameof(Query.TwoTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.TwoTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThreeTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.ThreeTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.FourTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FiveTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.FiveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.SixTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SevenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.SevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.EightTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.EightTypesWithParameters))
                        .ForMethod(
                            nameof(Query.NineTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.NineTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.TenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ElevenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.ElevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TwelveTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.TwelveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThirteenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.ThirteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourteenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.FourteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FifteenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.FifteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixteenTypesWithParameters), d => d
                            .UseConnectionAlias(OracleCommandStrings.Alias)
                            .UseCommandText(OracleCommandStrings.Query.Multimap.SixteenTypesWithParameters))));

        }

        public static CommanderSettingsBuilder AddQueryMultiple(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Query>(c => c
                .ForMethod(
                    nameof(Query.OneTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.OneTypeMultiple))
                .ForMethod(
                    nameof(Query.TwoTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.TwoTypeMultiple))
                .ForMethod(
                    nameof(Query.ThreeTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.ThreeTypeMultiple))
                .ForMethod(
                    nameof(Query.FourTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.FourTypeMultiple))
                .ForMethod(
                    nameof(Query.FiveTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.FiveTypeMultiple))
                .ForMethod(
                    nameof(Query.SixTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.SixTypeMultiple))
                .ForMethod(
                    nameof(Query.SevenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.SevenTypeMultiple))
                .ForMethod(
                    nameof(Query.EightTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.EightTypeMultiple))
                .ForMethod(
                    nameof(Query.NineTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.NineTypeMultiple))
                .ForMethod(
                    nameof(Query.TenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.TenTypeMultiple))
                .ForMethod(
                    nameof(Query.ElevenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.ElevenTypeMultiple))
                .ForMethod(
                    nameof(Query.TwelveTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.TwelveTypeMultiple))
                .ForMethod(
                    nameof(Query.ThirteenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.ThirteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FourteenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.FourteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FifteenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.FifteenTypeMultiple))
                .ForMethod(
                    nameof(Query.SixteenTypeMultiple), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Query.Multiple.SixteenTypeMultiple))));
        }

        public static CommanderSettingsBuilder AddExecute(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Execute>(c => c
                .ForMethod(
                    nameof(Execute.ExceptionsAreReturnedToCaller), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.ExceptionsAreReturnedToCaller))
                .ForMethod(
                    nameof(Execute.SupportParameterlessCalls), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportParameterlessCalls))
                .ForMethod(
                    $"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count", d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportsRollbackOnParameterlessCallsCount))
                .ForMethod(
                    nameof(Execute.SupportsRollbackOnParameterlessCalls), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportsRollbackOnParameterlessCalls))
                .ForMethod(
                    nameof(Execute.SupportsSuppressedDistributedTransactions), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportsSuppressedDistributedTransactions))
                .ForMethod(
                    $"{nameof(Execute.SupportsTransactionRollback)}.Count", d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportsTransactionRollbackCount))
                .ForMethod(
                    nameof(Execute.SupportsTransactionRollback), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportsTransactionRollback))
                .ForMethod(
                    nameof(Execute.SupportsEnlistingInAmbientTransactions), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SupportsEnlistingInAmbientTransactions))
                .ForMethod(
                    nameof(Execute.SuccessfullyWithResponse), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SuccessfullyWithResponse))
                .ForMethod(
                    $"{nameof(Execute.SuccessfullyWithResponse)}.Response", d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SuccessfullyWithResponseResponse))
                .ForMethod(
                    nameof(Execute.Successful), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.Successful))
                .ForMethod(
                    nameof(Execute.SingleType), d => d
                    .UseConnectionAlias(OracleCommandStrings.Alias)
                    .UseCommandText(OracleCommandStrings.Execute.SingleType))
                ));
        }

        public static CommanderSettingsBuilder AddDisposeCommands(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<Dispose>(b => b
                    .ForMethod(
                        nameof(Dispose.Successfully), c => c
                        .UseConnectionAlias(OracleCommandStrings.Alias)
                        .UseCommandText(OracleCommandStrings.Dispose.Successfully))));
        }
    }

}

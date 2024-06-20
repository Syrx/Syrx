using Syrx.Commanders.Databases.Connectors.Npgsql.Extensions;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Extensions;
using Syrx.Npgsql.Tests.Integration.DatabaseCommanderTests;

namespace Syrx.Npgsql.Tests.Integration.Setup
{
    public static class PostgresSetupExtensions
    {
        const string Alias = "Syrx.Postgres";
        const string Instance = "Syrx.Postgres";

        public static SyrxBuilder SetupPostgres(this SyrxBuilder builder)
        {
            return builder.UsePostgres(
                            b => b
                            .AddConnectionStrings()
                            .AddSetupBuilderOptions()
                            .AddQueryMultimap()
                            .AddQueryMultiple()
                            .AddExecute()
                            .AddDisposeCommands()
                            );
        }

        public static CommanderSettingsBuilder AddConnectionStrings(this CommanderSettingsBuilder builder)
        {
            // usually a terrible idea to store connection strings in source 
            // i'm leaving this in here as this is to be run against a default
            // docker instance of Postgres
            /*
             
             docker run --name syrx-postgres -e POSTGRES_PASSWORD=syrxforpostgres -e POSTGRES_DB=syrx -p 5432:5432 -d postgres
              
             */
            return builder
                .AddConnectionString(a => a
                    .UseAlias(PostgresCommandStrings.Alias)
                    .UseConnectionString(PostgresCommandStrings.ConnectionString));
        }

        public static CommanderSettingsBuilder AddSetupBuilderOptions(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<DatabaseBuilder>(
                    b => b
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateDatabase), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateDatabase))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableCreatorProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.DropTableCreatorProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableCreatorProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateTableCreatorProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTable), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropIdentityTesterProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.DropIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateIdentityTesterProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateIdentityTesterProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.DropBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateBulkInsertProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.DropBulkInsertAndReturnProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateBulkInsertAndReturnProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableClearingProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.DropTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableClearingProcedure), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.CreateTableClearingProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.ClearTable), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.ClearTable))
                    .ForMethod(
                        nameof(DatabaseBuilder.Populate), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Setup.Populate))
                    ));
        }

        public static CommanderSettingsBuilder AddQueryMultimap(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                    b => b.ForType<Query>(
                        c => c
                        .ForMethod(
                            nameof(Query.ExceptionsAreReturnedToCaller), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.ExceptionsAreReturnedToCaller))
                        .ForMethod(
                            nameof(Query.SingleType), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.SingleType))
                        .ForMethod(
                            nameof(Query.SingleTypeWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.SingleTypeWithParameters))
                        .ForMethod(
                            nameof(Query.TwoTypes), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.TwoTypes))
                        .ForMethod(
                            nameof(Query.TwoTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.TwoTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThreeTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.ThreeTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.FourTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FiveTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.FiveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.SixTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SevenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.SevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.EightTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.EightTypesWithParameters))
                        .ForMethod(
                            nameof(Query.NineTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.NineTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.TenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ElevenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.ElevenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.TwelveTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.TwelveTypesWithParameters))
                        .ForMethod(
                            nameof(Query.ThirteenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.ThirteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FourteenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.FourteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.FifteenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.FifteenTypesWithParameters))
                        .ForMethod(
                            nameof(Query.SixteenTypesWithParameters), d => d
                            .UseConnectionAlias(PostgresCommandStrings.Alias)
                            .UseCommandText(PostgresCommandStrings.Query.Multimap.SixteenTypesWithParameters))));

        }

        public static CommanderSettingsBuilder AddQueryMultiple(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Query>(c => c
                .ForMethod(
                    nameof(Query.OneTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.OneTypeMultiple))
                .ForMethod(
                    nameof(Query.TwoTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.TwoTypeMultiple))
                .ForMethod(
                    nameof(Query.ThreeTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.ThreeTypeMultiple))
                .ForMethod(
                    nameof(Query.FourTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.FourTypeMultiple))
                .ForMethod(
                    nameof(Query.FiveTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.FiveTypeMultiple))
                .ForMethod(
                    nameof(Query.SixTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.SixTypeMultiple))
                .ForMethod(
                    nameof(Query.SevenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.SevenTypeMultiple))
                .ForMethod(
                    nameof(Query.EightTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.EightTypeMultiple))
                .ForMethod(
                    nameof(Query.NineTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.NineTypeMultiple))
                .ForMethod(
                    nameof(Query.TenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.TenTypeMultiple))
                .ForMethod(
                    nameof(Query.ElevenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.ElevenTypeMultiple))
                .ForMethod(
                    nameof(Query.TwelveTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.TwelveTypeMultiple))
                .ForMethod(
                    nameof(Query.ThirteenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.ThirteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FourteenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.FourteenTypeMultiple))
                .ForMethod(
                    nameof(Query.FifteenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.FifteenTypeMultiple))
                .ForMethod(
                    nameof(Query.SixteenTypeMultiple), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Query.Multiple.SixteenTypeMultiple))));
        }

        public static CommanderSettingsBuilder AddExecute(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Execute>(c => c
                .ForMethod(
                    nameof(Execute.ExceptionsAreReturnedToCaller), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.ExceptionsAreReturnedToCaller))
                .ForMethod(
                    nameof(Execute.SupportParameterlessCalls), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportParameterlessCalls))
                .ForMethod(
                    $"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count", d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportsRollbackOnParameterlessCallsCount))
                .ForMethod(
                    nameof(Execute.SupportsRollbackOnParameterlessCalls), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportsRollbackOnParameterlessCalls))
                .ForMethod(
                    nameof(Execute.SupportsSuppressedDistributedTransactions), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportsSuppressedDistributedTransactions))
                .ForMethod(
                    $"{nameof(Execute.SupportsTransactionRollback)}.Count", d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportsTransactionRollbackCount))
                .ForMethod(
                    nameof(Execute.SupportsTransactionRollback), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportsTransactionRollback))
                .ForMethod(
                    nameof(Execute.SupportsEnlistingInAmbientTransactions), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SupportsEnlistingInAmbientTransactions))
                .ForMethod(
                    nameof(Execute.SuccessfullyWithResponse), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SuccessfullyWithResponse))
                .ForMethod(
                    $"{nameof(Execute.SuccessfullyWithResponse)}.Response", d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SuccessfullyWithResponseResponse))
                .ForMethod(
                    nameof(Execute.Successful), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.Successful))
                .ForMethod(
                    nameof(Execute.SingleType), d => d
                    .UseConnectionAlias(PostgresCommandStrings.Alias)
                    .UseCommandText(PostgresCommandStrings.Execute.SingleType))
                ));
        }

        public static CommanderSettingsBuilder AddDisposeCommands(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<Dispose>(b => b
                    .ForMethod(
                        nameof(Dispose.Successfully), c => c
                        .UseConnectionAlias(PostgresCommandStrings.Alias)
                        .UseCommandText(PostgresCommandStrings.Dispose.Successfully))));
        }
    }
}

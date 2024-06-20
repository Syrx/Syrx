using Syrx.Commanders.Databases.Connectors.SqlServer.Extensions;
using Syrx.Commanders.Databases.Settings.Extensions;
using Syrx.Commanders.Databases.Tests.Integration.DatabaseCommanderTests;
using Syrx.Extensions;
using System.Data;
using Testcontainers.MsSql;

namespace Syrx.Commanders.Databases.Tests.Integration.Setup
{
    public static class SqlServerSetupExtensions
    {
        const string Alias = "Syrx.SqlServer";
        const string Instance = "Syrx.SqlServer";

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
            // usually a terrible idea to store connection strings in source 
            // i'm leaving this in here as this is to be run against a default
            // docker instance of SqlServer
            /*
             
             */

            return builder
                .AddConnectionString(a => a
                    .UseAlias(SqlServerCommandStrings.Instance)
                    .UseConnectionString(connectionString))
                //.UseConnectionString(SqlServerCommandStrings.InstanceConnectionString))
                .AddConnectionString(a => a
                    .UseAlias(SqlServerCommandStrings.Alias)
                    .UseConnectionString(connectionString));
                    //.UseConnectionString(SqlServerCommandStrings.ConnectionString));
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


    /*
    public static class SqlServerSetupExtensions
    {
        const string Alias = "Syrx.Sql";

        
        public static SyrxBuilder SetupSqlServer(this SyrxBuilder builder)
        {
            return builder.UseSqlServer(
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
            return builder
                .AddConnectionString(a => a
                    .UseAlias(Alias)
                    .UseConnectionString("Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=Syrx;Integrated Security=true;"))
                .AddConnectionString(a => a
                    .UseAlias("Syrx.Sql.Master")
                    .UseConnectionString("Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=master;Integrated Security=true;"));
        }
        
        public static CommanderSettingsBuilder AddSetupBuilderOptions(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<DatabaseBuilder>(
                    b => b
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateDatabase), c => c
                        .UseConnectionAlias("Syrx.Sql.Master")
                        .UseCommandText(@"declare @sql nvarchar(max)
       ,@template nvarchar(max) = N'
-- create
if not exists (select * from [sys].[databases] where [name] = ''%name'')
begin
	create database [%name];
end';

select @sql = replace(@template, '%name', @name);
exec [sys].[sp_executesql] @sql;
"))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableCreatorProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"drop procedure if exists [dbo].[usp_create_table];"))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableCreatorProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"create procedure [dbo].[usp_create_table]
(@name nvarchar(max))
as
begin
    declare @template nvarchar(max)
           ,@sql nvarchar(max);

    select @template
        = N'
            if exists (select * from sys.objects where object_id = OBJECT_ID(N''[dbo].[%name]'') and type in (N''U'')) 
                begin
                    drop table [dbo].[%name];
                end 

            if not exists (select * from sys.objects where object_id = OBJECT_ID(N''[dbo].[%name]'') and type in (N''U'')) 
                begin 
                    create table [dbo].[%name] 
                    (
                        [id] [int] identity(1, 1) not null,
                        [name] [varchar](50) null,
                        [value] [decimal](18, 2) null,
                        [modified] [datetime] null); 
                end;';


    select @sql = replace(@template, '%name', @name);
    exec [sys].[sp_executesql] @sql;
end;"))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTable), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"[dbo].[usp_create_table]")
                        .SetCommandType(System.Data.CommandType.StoredProcedure))                    
                    .ForMethod(
                        nameof(DatabaseBuilder.DropIdentityTesterProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"drop procedure if exists [dbo].[usp_identity_tester];"))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateIdentityTesterProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"create procedure [dbo].[usp_identity_tester]
    @name varchar(50)
   ,@value decimal(18, 2)
as
begin
    insert into [identity_test]
    (
        [name]
       ,[value]
       ,[modified]
    )
    select @name
          ,@value
          ,getutcdate();

    select scope_identity();
end;"))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"drop procedure if exists [dbo].[usp_bulk_insert];"))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"create procedure [dbo].[usp_bulk_insert]
(@path varchar(max))
as
begin
    set nocount on;

    declare @command nvarchar(max)
           ,@template nvarchar(max)
        =   N'
                bulk insert [dbo].[bulk_insert] from ''%path'' with (fieldterminator = '','', rowterminator = ''\n'')';

    select @command = replace(@template, '%path', @path);
    exec [sys].[sp_executesql] @command;
end;"))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"drop procedure if exists [dbo].[usp_bulk_insert_and_return];"))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateBulkInsertAndReturnProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"create procedure [dbo].[usp_bulk_insert_and_return]
(@path varchar(max))
as
begin
    set nocount on;

    declare @command nvarchar(max)
           ,@template nvarchar(max)
        =   N'
                bulk insert [dbo].[bulk_insert]from ''%path'' with (fieldterminator = '','', rowterminator = ''\n'')';

    select @command = replace(@template, '%path', @path);
    exec [sys].[sp_executesql] @command;

    select *
    from [dbo].[bulk_insert];
end;"))
                    .ForMethod(
                        nameof(DatabaseBuilder.DropTableClearingProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"drop procedure if exists [dbo].[usp_clear_table];"))
                    .ForMethod(
                        nameof(DatabaseBuilder.CreateTableClearingProcedure), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"create procedure [dbo].[usp_clear_table]
(@name nvarchar(max))
as
begin
    declare @template nvarchar(max)
           ,@sql nvarchar(max);

    select @template
        = N'truncate table [%name];';


    select @sql = replace(@template, '%name', @name);
    exec [sys].[sp_executesql] @sql;
end;"))
                    .ForMethod(
                        nameof(DatabaseBuilder.ClearTable), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"[dbo].[usp_clear_table]")
                        .SetCommandType(System.Data.CommandType.StoredProcedure))
                    .ForMethod(
                        nameof(DatabaseBuilder.Populate), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"insert into Poco([Name], [Value], [Modified]) select @Name, @Value, @Modified;"))
                    
                    ));
        }

        public static CommanderSettingsBuilder AddQueryMultimap(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                    b => b.ForType<Query>(
                        c => c
                        .ForMethod(
                            nameof(Query.ExceptionsAreReturnedToCaller), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"select 1/0;"))
                        .ForMethod(
                            nameof(Query.SingleType), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"select [id][Id]
      ,[name][Name]
      ,[value][Value]
      ,[modified][Modified]
from [poco];"))
                        .ForMethod(
                            nameof(Query.SingleTypeWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [id][Id]
      ,[name][Name]
      ,[value][Value]
      ,[modified][Modified]
from [poco]
where [id] = @id;"))
                        .ForMethod(
                            nameof(Query.TwoTypes), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b]
        on [b].[Id] = ([a].[id] + 10)
where 1 = 1;"))
                        .ForMethod(
                            nameof(Query.TwoTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b]
        on [b].[Id] = ([a].[id] + 1)
where [a].[id] = @id;"))
                        .ForMethod(
                            nameof(Query.ThreeTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
	  ,[c].[Id]
      ,[c].[Name]
      ,[c].[Value]
      ,[c].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b] on [b].[Id] = ([a].[id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [c] on [c].[Id] = ([b].[id] + 1)
where [a].[id] = @id;"))
                        .ForMethod(
                            nameof(Query.FourTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
      ,[c].[Id]
      ,[c].[Name]
      ,[c].[Value]
      ,[c].[Modified]
      ,[d].[Id]
      ,[d].[Name]
      ,[d].[Value]
      ,[d].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b]
        on [b].[Id] = ([a].[id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [c]
        on [c].[Id] = ([b].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [d]
        on [d].[Id] = ([c].[Id] + 1)
where [a].[id] = @id;"))
                        .ForMethod(
                            nameof(Query.FiveTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
      ,[c].[Id]
      ,[c].[Name]
      ,[c].[Value]
      ,[c].[Modified]
      ,[d].[Id]
      ,[d].[Name]
      ,[d].[Value]
      ,[d].[Modified]
	  ,[e].[Id]
      ,[e].[Name]
      ,[e].[Value]
      ,[e].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b] on [b].[Id] = ([a].[id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [c] on [c].[Id] = ([b].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [d] on [d].[Id] = ([c].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [e] on [e].[Id] = ([d].[Id] + 1)
where [a].[id] = @id;"))
                        .ForMethod(
                            nameof(Query.SixTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
      ,[c].[Id]
      ,[c].[Name]
      ,[c].[Value]
      ,[c].[Modified]
      ,[d].[Id]
      ,[d].[Name]
      ,[d].[Value]
      ,[d].[Modified]
	  ,[e].[Id]
      ,[e].[Name]
      ,[e].[Value]
      ,[e].[Modified]
	  ,[f].[Id]
      ,[f].[Name]
      ,[f].[Value]
      ,[f].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b] on [b].[Id] = ([a].[id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [c] on [c].[Id] = ([b].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [d] on [d].[Id] = ([c].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [e] on [e].[Id] = ([d].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [f] on [f].[Id] = ([e].[Id] + 1)
where [a].[id] = @id;"))
                        .ForMethod(
                            nameof(Query.SevenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select [a].[id]
      ,[a].[name] [Name]
      ,[a].[value] [Value]
      ,[a].[modified] [Modified]
      ,[b].[Id]
      ,[b].[Name]
      ,[b].[Value]
      ,[b].[Modified]
      ,[c].[Id]
      ,[c].[Name]
      ,[c].[Value]
      ,[c].[Modified]
      ,[d].[Id]
      ,[d].[Name]
      ,[d].[Value]
      ,[d].[Modified]
	  ,[e].[Id]
      ,[e].[Name]
      ,[e].[Value]
      ,[e].[Modified]
	  ,[f].[Id]
      ,[f].[Name]
      ,[f].[Value]
      ,[f].[Modified]
	  ,[g].[Id]
      ,[g].[Name]
      ,[g].[Value]
      ,[g].[Modified]
from [dbo].[poco] [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b] on [b].[Id] = ([a].[id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [c] on [c].[Id] = ([b].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [d] on [d].[Id] = ([c].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [e] on [e].[Id] = ([d].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [f] on [f].[Id] = ([e].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [g] on [g].[Id] = ([f].[Id] + 1)
where [a].[id] = @id;"))
                        .ForMethod(
                            nameof(Query.EightTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [a]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [b]
        on [b].[Id] = ([a].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [c]
        on [c].[Id] = ([b].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [d]
        on [d].[Id] = ([c].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [e]
        on [e].[Id] = ([d].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [f]
        on [f].[Id] = ([e].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [g]
        on [g].[Id] = ([f].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [h]
        on [h].[Id] = ([g].[Id] + 1)
where [a].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.NineTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.TenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten]
        on [ten].[Id] = ([nine].[Id] + 1)
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.ElevenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten] on [ten].[Id] = ([nine].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.TwelveTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten] on [ten].[Id] = ([nine].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)		
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.ThirteenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten] on [ten].[Id] = ([nine].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)		
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.FourteenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten] on [ten].[Id] = ([nine].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [fourteen] on [fourteen].[Id] = ([thirteen].[Id] + 1)		
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.FifteenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten] on [ten].[Id] = ([nine].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [fourteen] on [fourteen].[Id] = ([thirteen].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [fifteen] on [fifteen].[Id] = ([fourteen].[Id] + 1)	
where [one].[Id] = @id;"))
                        .ForMethod(
                            nameof(Query.SixteenTypesWithParameters), d => d
                            .UseConnectionAlias(Alias)
                            .UseCommandText(@"
select *
from
(
    select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco]
) [one]
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [two]
        on [two].[Id] = ([one].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [three]
        on [three].[Id] = ([two].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [four]
        on [four].[Id] = ([three].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [five]
        on [five].[Id] = ([four].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [six]
        on [six].[Id] = ([five].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [seven]
        on [seven].[Id] = ([six].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eight]
        on [eight].[Id] = ([seven].[Id] + 1)
    join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [nine]
        on [nine].[Id] = ([eight].[Id] + 1)
		join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [ten] on [ten].[Id] = ([nine].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [fourteen] on [fourteen].[Id] = ([thirteen].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [fifteen] on [fifteen].[Id] = ([fourteen].[Id] + 1)
	join
    (
        select [id] [Id]
              ,[name] [Name]
              ,[value] [Value]
              ,[modified] [Modified]
        from [dbo].[poco]
    ) [sixteen] on [sixteen].[Id] = ([fifteen].[Id] + 1)
where [one].[Id] = @id;"))));
           
        }
                
        public static CommanderSettingsBuilder AddQueryMultiple(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Query>(c => c
                .ForMethod(
                    nameof(Query.OneTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 1
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.OneTypeMultiple) , d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 1
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.TwoTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 2
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.ThreeTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 3
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.FourTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 4
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.FiveTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 5
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.SixTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 6
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.SevenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 7
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.EightTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 8
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.NineTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 9
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.TenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 10
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.ElevenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 11
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.TwelveTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 12
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.ThirteenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 13
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.FourteenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 14
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.FifteenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 15
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))
                .ForMethod(
                    nameof(Query.SixteenTypeMultiple), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"declare @sets int = 16
       ,@loop int = 1
       ,@step int = 1
	   ,@message nvarchar(max)
       ,@params nvarchar(500) = N'@max int'
       ,@template nvarchar(max) = N'
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [id] between 1 and @max';

while @loop <= @sets
begin

	select @message = concat('@loop: ', @loop, ' @sets:', @sets, ' @step:', @step);
	raiserror (@message, 10, 1) with nowait;
	   
    exec [sys].[sp_executesql] @template, @params, @max = @loop;
    select @loop = @loop + 1;

end;"))));
        }

        public static CommanderSettingsBuilder AddExecute(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                b => b.ForType<Execute>(c => c
                .ForMethod(
                    nameof(Execute.ExceptionsAreReturnedToCaller), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText("select 1/0;"))
                .ForMethod(
                    nameof(Execute.SupportParameterlessCalls), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
declare @result table
(
    [Value] int
);
insert into @result
(
    [Value]
)
select 1;

select *
from @result;"))
                .ForMethod(
                    $"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count", d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
select count(1) [result]
from [dbo].[poco];"))
                .ForMethod(
                    nameof(Execute.SupportsRollbackOnParameterlessCalls), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
delete from [dbo].[poco];
select 1 / 0;"))
                .ForMethod(
                    nameof(Execute.SupportsSuppressedDistributedTransactions), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
insert into [dbo].[distributed_transaction]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;"))
                .ForMethod(
                    $"{nameof(Execute.SupportsTransactionRollback)}.Count", d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
select *
from [dbo].[poco]
where [name] = @Name;"))
                .ForMethod(
                    nameof(Execute.SupportsTransactionRollback), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
insert into [dbo].[poco]
(
    [name]
   ,[value]
)
select @Name
      ,@Value * power(@Value, @Value);"))
                .ForMethod(
                    nameof(Execute.SupportsEnlistingInAmbientTransactions), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
insert into [dbo].[distributed_transaction]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;"))
                .ForMethod(
                    nameof(Execute.SuccessfullyWithResponse), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
insert into [dbo].[poco]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;"))
                .ForMethod(
                    $"{nameof(Execute.SuccessfullyWithResponse)}.Response", d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [name] = @name;"))
                .ForMethod(
                    nameof(Execute.Successful), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
insert into [dbo].[poco]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;"))
                .ForMethod(
                    nameof(Execute.SingleType), d => d
                    .UseConnectionAlias(Alias)
                    .UseCommandText(@"
insert into [dbo].[poco]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;"))
                ));
        }

        public static CommanderSettingsBuilder AddDisposeCommands(this CommanderSettingsBuilder builder)
        {
            return builder.AddCommand(
                a => a.ForType<Dispose>(b => b
                    .ForMethod(
                        nameof(Dispose.Successfully), c => c
                        .UseConnectionAlias(Alias)
                        .UseCommandText(@"select cast(rand() * 100 as int);"))));
        }
    
        
    }

    */

}

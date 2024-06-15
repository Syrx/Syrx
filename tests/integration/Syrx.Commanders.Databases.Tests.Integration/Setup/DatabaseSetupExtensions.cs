using Syrx.Commanders.Databases.Settings.Extensions.Options;
using Syrx.Commanders.Databases.Tests.Integration.DatabaseCommanderTests;

namespace Syrx.Commanders.Databases.Tests.Integration.Setup
{
    public static class DatabaseSetupExtensions
    {
        const string alias = "Syrx.Sql";

        public static DatabaseCommanderSettingsOptions AddConnectionStrings(this DatabaseCommanderSettingsOptions options)
        {
            options
                .AddConnectionString(alias, "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=Syrx;Integrated Security=true;")
                .AddConnectionString("Syrx.Sql.Master", "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=master;Integrated Security=true;");
            return options;
        }

        public static DatabaseCommanderSettingsOptions AddSetupBuilderCommands(this DatabaseCommanderSettingsOptions options)
        {
            options
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateDatabase))
                       .AgainstConnectionAlias("Syrx.Sql.Master")
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
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.DropTableCreatorProcedure))
                       .AgainstConnectionAlias(alias)
                       .UseCommandText(@"drop procedure if exists [dbo].[usp_create_table];"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateTableCreatorProcedure))
                       .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateTable))
                       .AgainstConnectionAlias(alias)
                       .UsingCommandType(System.Data.CommandType.StoredProcedure)
                       .UseCommandText(@"[dbo].[usp_create_table]"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.DropIdentityTesterProcedure))
                       .AgainstConnectionAlias(alias)
                       .UseCommandText(@"drop procedure if exists [dbo].[usp_identity_tester];"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateIdentityTesterProcedure))
                       .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.DropBulkInsertProcedure))
                       .AgainstConnectionAlias(alias)
                       .UseCommandText(@"drop procedure if exists [dbo].[usp_bulk_insert];"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateBulkInsertProcedure))
                       .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.DropBulkInsertAndReturnProcedure))
                       .AgainstConnectionAlias(alias)
                       .UseCommandText(@"drop procedure if exists [dbo].[usp_bulk_insert_and_return];"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateBulkInsertAndReturnProcedure))
                       .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.DropTableClearingProcedure))
                       .AgainstConnectionAlias(alias)
                       .UseCommandText(@"drop procedure if exists [dbo].[usp_clear_table];"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.CreateTableClearingProcedure))
                       .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.ClearTable))
                       .AgainstConnectionAlias(alias)
                       .UsingCommandType(System.Data.CommandType.StoredProcedure)
                       .UseCommandText(@"[dbo].[usp_clear_table]"))
                .AddCommand(c => c
                       .ForRepositoryType<DatabaseBuilder>()
                       .ForMethodNamed(nameof(DatabaseBuilder.Populate))
                       .AgainstConnectionAlias(alias)
                       .UseCommandText(@"insert into Poco([Name], [Value], [Modified]) select @Name, @Value, @Modified;"))
            ;
            return options;
        }

        public static DatabaseCommanderSettingsOptions AddMultimapCommands(this DatabaseCommanderSettingsOptions options)
        {
            return options
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ExceptionsAreReturnedToCaller))
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"select 1/0;"))
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SingleType))
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
select [id][Id]
      ,[name][Name]
      ,[value][Value]
      ,[modified][Modified]
from [poco];"))
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SingleTypeWithParameters))
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
select [id][Id]
      ,[name][Name]
      ,[value][Value]
      ,[modified][Modified]
from [poco]
where [id] = @id;"))
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TwoTypes))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TwoTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ThreeTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FourTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FiveTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SixTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SevenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.EightTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.NineTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ElevenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TwelveTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ThirteenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FourteenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FifteenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SixteenTypesWithParameters))
                    .AgainstConnectionAlias(alias)
                    .SplitResultsOn("Id")
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
where [one].[Id] = @id;"))

                ;

            //return options;
        }

        public static DatabaseCommanderSettingsOptions AddMultipleCommands(this DatabaseCommanderSettingsOptions options)
        {
            return options
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.OneTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TwoTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ThreeTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FourTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FiveTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SixTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SevenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.EightTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.NineTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ElevenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.TwelveTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.ThirteenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FourteenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.FifteenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Query>()
                    .ForMethodNamed(nameof(Query.SixteenTypeMultiple))
                    .AgainstConnectionAlias(alias)
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

end;"))
                ;

            //return options;
        }

        public static DatabaseCommanderSettingsOptions AddExecuteCommands(this DatabaseCommanderSettingsOptions options)
        {
            options
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.ExceptionsAreReturnedToCaller))
                    .AgainstConnectionAlias(alias)
                    .UseCommandText("select 1/0;"))
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SupportParameterlessCalls))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed($"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count")
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
select count(1) [result]
from [dbo].[poco];"))
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SupportsRollbackOnParameterlessCalls))
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
delete from [dbo].[poco];
select 1 / 0;"))
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SupportsSuppressedDistributedTransactions))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed($"{nameof(Execute.SupportsTransactionRollback)}.Count")
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
select *
from [dbo].[poco]
where [name] = @Name;"))
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SupportsTransactionRollback))
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
insert into [dbo].[poco]
(
    [name]
   ,[value]
)
select @Name
      ,@Value * power(@Value, @Value);"))
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SupportsEnlistingInAmbientTransactions))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SuccessfullyWithResponse))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed($"{nameof(Execute.SuccessfullyWithResponse)}.Response")
                    .AgainstConnectionAlias(alias)
                    .UseCommandText(@"
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [name] = @name;"))
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.Successful))
                    .AgainstConnectionAlias(alias)
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
                .AddCommand(c => c
                    .ForRepositoryType<Execute>()
                    .ForMethodNamed(nameof(Execute.SingleType))
                    .AgainstConnectionAlias(alias)
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


                ;
            return options;
        }

        public static DatabaseCommanderSettingsOptions AddDisposeCommands(this DatabaseCommanderSettingsOptions options)
        {
            return options.AddCommand(
                    c => c.ForRepositoryType<Dispose>()
                        .ForMethodNamed(nameof(Dispose.Successfully))
                        .AgainstConnectionAlias(alias)
                        .UseCommandText(@"select cast(rand() * 100 as int);")
                        );
        }
    }
}

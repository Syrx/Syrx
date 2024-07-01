namespace Syrx.SqlServer.Tests.Integration
{
    public static class SqlServerCommandStrings
    {
        public const string Alias = "Syrx.Sql";
        public const string Instance = "Syrx.Sql.Instance";
        public const string ConnectionString = "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=Syrx;Integrated Security=true;";
        public const string InstanceConnectionString = "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=master;Integrated Security=true;";

        public static class Setup
        {
            public const string CreateDatabase = @"declare @sql nvarchar(max)
       ,@template nvarchar(max) = N'
-- create
if not exists (select * from [sys].[databases] where [name] = ''%name'')
begin
	create database [%name];
end';

select @sql = replace(@template, '%name', @name);
exec [sys].[sp_executesql] @sql;
";

            public const string DropTableCreatorProcedure = @"drop procedure if exists [dbo].[usp_create_table];";

            public const string CreateTableCreatorProcedure = @"create procedure [dbo].[usp_create_table]
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
end;
";

            public const string CreateTable = @"[dbo].[usp_create_table]";

            public const string DropIdentityTesterProcedure = @"drop procedure if exists [dbo].[usp_identity_tester];";

            public const string CreateIdentityTesterProcedure = @"create procedure [dbo].[usp_identity_tester]
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
end;";

            public const string DropBulkInsertProcedure = @"drop procedure if exists [dbo].[usp_bulk_insert];";

            public const string CreateBulkInsertProcedure = @"create procedure [dbo].[usp_bulk_insert]
(@path varchar(max))
as
begin
    set nocount on;
    declare @command nvarchar(max)
           ,@template nvarchar(max) =   N'
                bulk insert [dbo].[bulk_insert] from ''%path'' with (fieldterminator = '','', rowterminator = ''\n'')';

    select @command = replace(@template, '%path', @path);
    exec [sys].[sp_executesql] @command;
end;";

            public const string DropBulkInsertAndReturnProcedure = @"drop procedure if exists [dbo].[usp_bulk_insert_and_return];";

            public const string CreateBulkInsertAndReturnProcedure = @"create procedure [dbo].[usp_bulk_insert_and_return]
(@path varchar(max))
as
begin
    set nocount on;

    declare @command nvarchar(max)
           ,@template nvarchar(max) = N'
                bulk insert [dbo].[bulk_insert]from ''%path'' with (fieldterminator = '','', rowterminator = ''\n'')';

    select @command = replace(@template, '%path', @path);
    exec [sys].[sp_executesql] @command;

    select *
    from [dbo].[bulk_insert];
end;";

            public const string DropTableClearingProcedure = @"drop procedure if exists [dbo].[usp_clear_table];";

            public const string CreateTableClearingProcedure = @"create procedure [dbo].[usp_clear_table]
(@name nvarchar(max))
as
begin
    declare @template nvarchar(max)
           ,@sql nvarchar(max);

    select @template
        = N'truncate table [%name];';


    select @sql = replace(@template, '%name', @name);
    exec [sys].[sp_executesql] @sql;
end;";

            public const string ClearTable = @"[dbo].[usp_clear_table]";

            public const string Populate = @"insert into Poco([Name], [Value], [Modified]) select @Name, @Value, @Modified;";

        }

        public static class Query
        {
            public static class Multimap
            {

                public const string ExceptionsAreReturnedToCaller = @"select 1/0;";

                public const string SingleType = @"select [id][Id]
      ,[name][Name]
      ,[value][Value]
      ,[modified][Modified]
from [poco];";

                public const string SingleTypeWithParameters = @"
select [id][Id]
      ,[name][Name]
      ,[value][Value]
      ,[modified][Modified]
from [poco]
where [id] = @id;";

                public const string TwoTypes = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 10);";

                public const string TwoTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
where [one].[Id] = @id;";

                public const string ThreeTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
where [one].[Id] = @id;";

                public const string FourTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
where [one].[Id] = @id;";

                public const string FiveTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
where [one].[Id] = @id;";

                public const string SixTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
where [one].[Id] = @id;";

                public const string SevenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
where [one].[Id] = @id;";

                public const string EightTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
where [one].[Id] = @id;";

                public const string NineTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
where [one].[Id] = @id;";

                public const string TenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
where [one].[Id] = @id;";

                public const string ElevenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
    join [data] [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
where [one].[Id] = @id;";

                public const string TwelveTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
    join [data] [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
    join [data] [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
where [one].[Id] = @id;";

                public const string ThirteenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
    join [data] [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
    join [data] [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
    join [data] [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
where [one].[Id] = @id;";

                public const string FourteenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
    join [data] [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
    join [data] [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
    join [data] [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
    join [data] [fourteen] on [fourteen].[Id] = ([thirteen].[Id] + 1)
where [one].[Id] = @id;";

                public const string FifteenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
    join [data] [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
    join [data] [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
    join [data] [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
    join [data] [fourteen] on [fourteen].[Id] = ([thirteen].[Id] + 1)
    join [data] [fifteen] on [fifteen].[Id] = ([fourteen].[Id] + 1)
where [one].[Id] = @id;";

                public const string SixteenTypesWithParameters = @";with [data]
as (select [id] [Id]
          ,[name] [Name]
          ,[value] [Value]
          ,[modified] [Modified]
    from [dbo].[poco])
select *
from [data] [one]
    join [data] [two] on [two].[Id] = ([one].[Id] + 1)
    join [data] [three] on [three].[Id] = ([two].[Id] + 1)
    join [data] [four] on [four].[Id] = ([three].[Id] + 1)
    join [data] [five] on [five].[Id] = ([four].[Id] + 1)
    join [data] [six] on [six].[Id] = ([five].[Id] + 1)
    join [data] [seven] on [seven].[Id] = ([six].[Id] + 1)
    join [data] [eight] on [eight].[Id] = ([seven].[Id] + 1)
    join [data] [nine] on [nine].[Id] = ([eight].[Id] + 1)
    join [data] [ten] on [ten].[Id] = ([nine].[Id] + 1)
    join [data] [eleven] on [eleven].[Id] = ([ten].[Id] + 1)
    join [data] [twelve] on [twelve].[Id] = ([eleven].[Id] + 1)
    join [data] [thirteen] on [thirteen].[Id] = ([twelve].[Id] + 1)
    join [data] [fourteen] on [fourteen].[Id] = ([thirteen].[Id] + 1)
    join [data] [fifteen] on [fifteen].[Id] = ([fourteen].[Id] + 1)
    join [data] [sixteen] on [sixteen].[Id] = ([fifteen].[Id] + 1)
where [one].[Id] = @id;";
            }

            public static class Multiple
            {
                public const string OneTypeMultiple = @"declare @sets int = 1
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

end;";

                public const string TwoTypeMultiple = @"declare @sets int = 2
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

end;";

                public const string ThreeTypeMultiple = @"declare @sets int = 3
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

end;";

                public const string FourTypeMultiple = @"declare @sets int = 4
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

end;";

                public const string FiveTypeMultiple = @"declare @sets int = 5
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

end;";

                public const string SixTypeMultiple = @"declare @sets int = 6
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

end;";

                public const string SevenTypeMultiple = @"declare @sets int = 7
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

end;";

                public const string EightTypeMultiple = @"declare @sets int = 8
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

end;";

                public const string NineTypeMultiple = @"declare @sets int = 9
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

end;";

                public const string TenTypeMultiple = @"declare @sets int = 10
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

end;";

                public const string ElevenTypeMultiple = @"declare @sets int = 11
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

end;";

                public const string TwelveTypeMultiple = @"declare @sets int = 12
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

end;";

                public const string ThirteenTypeMultiple = @"declare @sets int = 13
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

end;";

                public const string FourteenTypeMultiple = @"declare @sets int = 14
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

end;";

                public const string FifteenTypeMultiple = @"declare @sets int = 15
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

end;";

                public const string SixteenTypeMultiple = @"declare @sets int = 16
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

end;";
            }
        }

        public static class Execute
        {

            public const string ExceptionsAreReturnedToCaller = "select 1/0;";

            public const string SupportParameterlessCalls = @"declare @result table
(
    [Value] int
);
insert into @result
(
    [Value]
)
select 1;

select *
from @result;";

            public const string SupportsRollbackOnParameterlessCallsCount = @"
select count(1) [result]
from [dbo].[poco];";

            public const string SupportsRollbackOnParameterlessCalls = @"
delete from [dbo].[poco];
select 1 / 0;";

            public const string SupportsSuppressedDistributedTransactions = @"
insert into [dbo].[distributed_transaction]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SupportsTransactionRollbackCount = @"
select *
from [dbo].[poco]
where [name] = @Name;";

            public const string SupportsTransactionRollback = @"
insert into [dbo].[poco]
(
    [name]
   ,[value]
)
select @Name
      ,@Value * power(@Value, @Value);";

            public const string SupportsEnlistingInAmbientTransactions = @"
insert into [dbo].[distributed_transaction]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SuccessfullyWithResponse = @"
insert into [dbo].[poco]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SuccessfullyWithResponseResponse = @"
select [id]
      ,[name]
      ,[value]
      ,[modified]
from [dbo].[poco]
where [name] = @name;";

            public const string Successful = @"
insert into [dbo].[poco]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SingleType = @"
insert into [dbo].[poco]
(
    [name]
   ,[value]
   ,[modified]
)
select @Name
      ,@Value
      ,@Modified;";

        }

        public static class Dispose
        {

            public const string Successfully = @"select cast(rand() * 100 as int);";
        }
    }
}

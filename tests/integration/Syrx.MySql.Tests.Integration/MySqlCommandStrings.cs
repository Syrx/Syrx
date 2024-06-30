namespace Syrx.MySql.Tests.Integration
{
    public static class MySqlCommandStrings
    {
        public const string Alias = "Syrx.MySql";
        
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

            public const string DropTableCreatorProcedure = @"DROP PROCEDURE IF EXISTS usp_create_table;";

            public const string CreateTableCreatorProcedure = @"
DROP PROCEDURE IF EXISTS `usp_create_table`;
CREATE PROCEDURE `usp_create_table` (IN name VARCHAR(255))
BEGIN
    SET @drop_template = CONCAT(""DROP TABLE IF EXISTS `"", name, ""`;"");
    PREPARE drop_stmt FROM @drop_template;
    EXECUTE drop_stmt;
    DEALLOCATE PREPARE drop_stmt;

    SET @create_template = CONCAT(""CREATE TABLE IF NOT EXISTS `"", name, ""` (
        `Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        `Name` VARCHAR(50) NOT NULL,
        `Value` DECIMAL(18) NOT NULL,
        `Modified` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    ) DEFAULT CHARSET=utf8;"");
    PREPARE create_stmt FROM @create_template;
    EXECUTE create_stmt;
    DEALLOCATE PREPARE create_stmt;
END;
";

            public const string CreateTable = @"usp_create_table";

            //public const string DropIdentityTesterProcedure = @"DROP PROCEDURE IF EXISTS usp_identity_tester;";

            public const string CreateIdentityTesterProcedure = @"
DROP PROCEDURE IF EXISTS `usp_identity_tester`;
CREATE PROCEDURE usp_identity_tester(
    IN p_name VARCHAR(50),
    IN p_value DECIMAL(18, 2)
)
BEGIN
    INSERT INTO identity_test (name, value, modified)
    VALUES (p_name, p_value, UTC_TIMESTAMP());

    SELECT LAST_INSERT_ID();
END;
";

            //public const string DropBulkInsertProcedure = @"DROP PROCEDURE IF EXISTS usp_bulk_insert;";

            public const string CreateBulkInsertProcedure = @"
DROP PROCEDURE IF EXISTS usp_bulk_insert;
CREATE PROCEDURE usp_bulk_insert(IN p_path VARCHAR(255))
BEGIN
    SET SESSION sql_mode = 'NO_AUTO_VALUE_ON_ZERO';
    SET @command = CONCAT('LOAD DATA INFILE ''', p_path, ''' INTO TABLE bulk_insert FIELDS TERMINATED BY '','' LINES TERMINATED BY ''\\n'';');
    PREPARE stmt FROM @command;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
END;
";

            //public const string DropBulkInsertAndReturnProcedure = @"DROP PROCEDURE IF EXISTS usp_bulk_insert_and_return;";

            public const string CreateBulkInsertAndReturnProcedure = @"
DROP PROCEDURE IF EXISTS usp_bulk_insert_and_return;
CREATE PROCEDURE usp_bulk_insert_and_return(IN p_path VARCHAR(255))
BEGIN
    SET SESSION sql_mode = 'NO_AUTO_VALUE_ON_ZERO';
    SET @command = CONCAT('LOAD DATA INFILE ''', p_path, ''' INTO TABLE bulk_insert FIELDS TERMINATED BY '','' LINES TERMINATED BY ''\\n'';');
    PREPARE stmt FROM @command;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;

    SELECT * FROM bulk_insert;
END;
";

            //public const string DropTableClearingProcedure = @"DROP PROCEDURE IF EXISTS usp_clear_table;";

            public const string CreateTableClearingProcedure = @"
DROP PROCEDURE IF EXISTS usp_clear_table;
CREATE PROCEDURE usp_clear_table(IN name VARCHAR(255))
BEGIN
    SET @sql = CONCAT('TRUNCATE TABLE ', name);
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
END;
";

            public const string ClearTable = @"usp_clear_table";

            public const string Populate = @"INSERT INTO poco (Name, Value, Modified) VALUES (@Name, @Value, @Modified)";

        }

        public static class Query
        {
            public static class Multimap
            {

                public const string ExceptionsAreReturnedToCaller = @"SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Division by zero error';";

                public const string SingleType = @"select * from poco;";

                public const string SingleTypeWithParameters = @"select * from poco where id = @id;
";

                public const string TwoTypes = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 10);";

                public const string TwoTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
where one.Id = @id;";

                public const string ThreeTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
where one.Id = @id;";

                public const string FourTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
where one.Id = @id;";

                public const string FiveTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
where one.Id = @id;";

                public const string SixTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
where one.Id = @id;";

                public const string SevenTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
where one.Id = @id;";

                public const string EightTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
where one.Id = @id;";

                public const string NineTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
where one.Id = @id;";

                public const string TenTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
where one.Id = @id;";

                public const string ElevenTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
    join data eleven on eleven.Id = (ten.Id + 1)
where one.Id = @id;";

                public const string TwelveTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
    join data eleven on eleven.Id = (ten.Id + 1)
    join data twelve on twelve.Id = (eleven.Id + 1)
where one.Id = @id;";

                public const string ThirteenTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
    join data eleven on eleven.Id = (ten.Id + 1)
    join data twelve on twelve.Id = (eleven.Id + 1)
    join data thirteen on thirteen.Id = (twelve.Id + 1)
where one.Id = @id;";

                public const string FourteenTypesWithParameters = @"with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
    join data eleven on eleven.Id = (ten.Id + 1)
    join data twelve on twelve.Id = (eleven.Id + 1)
    join data thirteen on thirteen.Id = (twelve.Id + 1)
    join data fourteen on fourteen.Id = (thirteen.Id + 1)
where one.Id = @id;";

                public const string FifteenTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
    join data eleven on eleven.Id = (ten.Id + 1)
    join data twelve on twelve.Id = (eleven.Id + 1)
    join data thirteen on thirteen.Id = (twelve.Id + 1)
    join data fourteen on fourteen.Id = (thirteen.Id + 1)
    join data fifteen on fifteen.Id = (fourteen.Id + 1)
where one.Id = @id;";

                public const string SixteenTypesWithParameters = @"
with data
as (select Id
          ,Name
          ,Value
          ,Modified
    from poco)
select *
from data one
    join data two on two.Id = (one.Id + 1)
    join data three on three.Id = (two.Id + 1)
    join data four on four.Id = (three.Id + 1)
    join data five on five.Id = (four.Id + 1)
    join data six on six.Id = (five.Id + 1)
    join data seven on seven.Id = (six.Id + 1)
    join data eight on eight.Id = (seven.Id + 1)
    join data nine on nine.Id = (eight.Id + 1)
    join data ten on ten.Id = (nine.Id + 1)
    join data eleven on eleven.Id = (ten.Id + 1)
    join data twelve on twelve.Id = (eleven.Id + 1)
    join data thirteen on thirteen.Id = (twelve.Id + 1)
    join data fourteen on fourteen.Id = (thirteen.Id + 1)
    join data fifteen on fifteen.Id = (fourteen.Id + 1)
    join data sixteen on sixteen.Id = (fifteen.Id + 1)
where one.Id = @id;";
            }

            public static class Multiple
            {
                public const string OneTypeMultiple = @"
select * from poco where id between 1 and 1;";

                public const string TwoTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;";

                public const string ThreeTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;";

                public const string FourTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;";

                public const string FiveTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;";

                public const string SixTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;";

                public const string SevenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;";

                public const string EightTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;";

                public const string NineTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;";

                public const string TenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;";

                public const string ElevenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;
select * from poco where id between 1 and 11;";

                public const string TwelveTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;
select * from poco where id between 1 and 11;
select * from poco where id between 1 and 12;";

                public const string ThirteenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;
select * from poco where id between 1 and 11;
select * from poco where id between 1 and 12;
select * from poco where id between 1 and 13;";

                public const string FourteenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;
select * from poco where id between 1 and 11;
select * from poco where id between 1 and 12;
select * from poco where id between 1 and 13;
select * from poco where id between 1 and 14;";

                public const string FifteenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;
select * from poco where id between 1 and 11;
select * from poco where id between 1 and 12;
select * from poco where id between 1 and 13;
select * from poco where id between 1 and 14;
select * from poco where id between 1 and 15;";

                public const string SixteenTypeMultiple = @"
select * from poco where id between 1 and 1;
select * from poco where id between 1 and 2;
select * from poco where id between 1 and 3;
select * from poco where id between 1 and 4;
select * from poco where id between 1 and 5;
select * from poco where id between 1 and 6;
select * from poco where id between 1 and 7;
select * from poco where id between 1 and 8;
select * from poco where id between 1 and 9;
select * from poco where id between 1 and 10;
select * from poco where id between 1 and 11;
select * from poco where id between 1 and 12;
select * from poco where id between 1 and 13;
select * from poco where id between 1 and 14;
select * from poco where id between 1 and 15;
select * from poco where id between 1 and 16;";
            }
        }

        public static class Execute
        {
            public const string ExceptionsAreReturnedToCaller = @"SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Division by zero error';";

            public const string SupportParameterlessCalls = @"CREATE TEMPORARY TABLE result
(
    Value INT
);

INSERT INTO result (Value)
VALUES (1);

SELECT *
FROM result;
";

            public const string SupportsRollbackOnParameterlessCallsCount = @"select count(1) `result` from poco;";

            public const string SupportsRollbackOnParameterlessCalls = @"
delete from poco;
SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Deliberate exception.';";

            public const string SupportsSuppressedDistributedTransactions = @"
insert into distributed_transaction
(
    name
   ,value
   ,modified
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SupportsTransactionRollbackCount = @"
select *
from poco
where name = @Name;";

            public const string SupportsTransactionRollback = @"
insert into poco
(
    name
   ,value
)
select @Name
      ,@Value * power(@Value, @Value);";

            public const string SupportsEnlistingInAmbientTransactions = @"
insert into distributed_transaction
(
    name
   ,value
   ,modified
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SuccessfullyWithResponse = @"
insert into poco
(
    name
   ,value
   ,modified
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SuccessfullyWithResponseResponse = @"
select id
      ,name
      ,value
      ,modified
from poco
where name = @name;";

            public const string Successful = @"
insert into poco
(
    name
   ,value
   ,modified
)
select @Name
      ,@Value
      ,@Modified;";

            public const string SingleType = @"
insert into poco
(
    name
   ,value
   ,modified
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
namespace Syrx.Npgsql.Tests.Integration.Setup
{
    public static class PostgresCommandStrings
    {
        public const string Alias = "Syrx.Postgres";
        //const string Instance = "Syrx.Postgres";
        public const string ConnectionString = "Host=localhost;Port=5432;Database=syrx;Username=postgres;Password=syrxforpostgres;Include Error Detail=true;";

        public static class Setup
        {
            public const string CreateDatabase = @"create database Syrx;";

            public const string DropTableCreatorProcedure = @"DROP PROCEDURE IF EXISTS usp_create_table(text);";

            public const string CreateTableCreatorProcedure = @"CREATE OR REPLACE PROCEDURE usp_create_table(_name text)
LANGUAGE plpgsql
AS $$
DECLARE
    _template text;
    _sql text;
BEGIN
    -- Check if the table exists and drop it if it does
    IF EXISTS (SELECT FROM pg_catalog.pg_tables WHERE schemaname = 'public' AND tablename = lower(_name)) THEN
        _template := 'DROP TABLE IF EXISTS public.%I';
        _sql := format(_template, _name);
        EXECUTE _sql;
    END IF;

    -- Create the table if it does not exist
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_tables WHERE schemaname = 'public' AND tablename = lower(_name)) THEN
        _template := 'CREATE TABLE public.%I (id SERIAL PRIMARY KEY, name VARCHAR(50), value NUMERIC(18, 2), modified TIMESTAMP)';
        _sql := format(_template, _name);
        EXECUTE _sql;
    END IF;
END;
$$;
";

            public const string CreateTable = @"DO $$
DECLARE
    name text := 'poco';
BEGIN
      CALL usp_create_table(name);
END $$;
";

            public const string DropIdentityTesterProcedure = @"DROP FUNCTION IF EXISTS usp_identity_tester(varchar, numeric);";

            public const string CreateIdentityTesterProcedure = @"CREATE OR REPLACE FUNCTION usp_identity_tester(_name VARCHAR(50), _value NUMERIC(18, 2))
RETURNS SETOF bigint AS
$$
DECLARE
    _id bigint;
BEGIN
    INSERT INTO identity_test(name, value, modified)
    VALUES (_name, _value, CURRENT_TIMESTAMP)
    RETURNING id INTO _id;

    RETURN NEXT _id;
END;
$$ LANGUAGE plpgsql;
";

            public const string DropBulkInsertProcedure = @"DROP PROCEDURE IF EXISTS usp_bulk_insert(text);";

            public const string CreateBulkInsertProcedure = @"CREATE OR REPLACE PROCEDURE usp_bulk_insert(_path text)
LANGUAGE plpgsql
AS $$
DECLARE
    _command text;
BEGIN
    -- Prepare the COPY command
    _command := format('COPY bulk_insert FROM %L WITH (FORMAT csv, DELIMITER '','', NULL '''', HEADER);', _path);

    -- Execute the COPY command
    EXECUTE _command;
END;
$$;
";

            public const string DropBulkInsertAndReturnProcedure = @"DROP PROCEDURE IF EXISTS usp_bulk_insert_and_return(text);";

            public const string CreateBulkInsertAndReturnProcedure = @"CREATE OR REPLACE PROCEDURE usp_bulk_insert_and_return(_path text, OUT ref refcursor)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Perform the bulk insert using COPY
    EXECUTE format('COPY bulk_insert FROM %L WITH (FORMAT csv, DELIMITER '','', NULL '''', HEADER);', _path);

    -- Open a refcursor to return the result set
    OPEN ref FOR SELECT * FROM bulk_insert;
END;
$$;
";

            public const string DropTableClearingProcedure = @"DROP PROCEDURE IF EXISTS usp_clear_table();";

            public const string CreateTableClearingProcedure = @"CREATE OR REPLACE PROCEDURE usp_clear_table(_name text)
LANGUAGE plpgsql
AS $$
DECLARE
    _template text;
    _sql text;
BEGIN
    -- Prepare the TRUNCATE TABLE statement
    _template := 'TRUNCATE TABLE public.%I';
    _sql := format(_template, _name);

    -- Execute the TRUNCATE TABLE statement
    EXECUTE _sql;
END;
$$;
";

            public const string ClearTable = @"DO $$
DECLARE
    name text := 'poco';
BEGIN
      CALL usp_clear_table(name);
END $$;";

            public const string Populate = @"INSERT INTO poco(name, value, modified) VALUES (@Name, @Value, @Modified);";
        }

        public static class Query
        {

            public static class Multimap
            {

                public const string ExceptionsAreReturnedToCaller = @"SELECT 1/0;";

                public const string SingleType = @"SELECT id AS ""Id"",
       name AS ""Name"",
       value AS ""Value"",
       modified AS ""Modified""
FROM poco;";

                public const string SingleTypeWithParameters = @"SELECT id AS ""Id"",
       name AS ""Name"",
       value AS ""Value"",
       modified AS ""Modified""
FROM poco
WHERE id = @id;";

                public const string TwoTypes = @"SELECT a.id,
       a.name AS ""Name"",
       a.value AS ""Value"",
       a.modified AS ""Modified"",
       b.id AS ""Id"",
       b.name,
       b.value,
       b.modified
FROM poco a
JOIN (
    SELECT id,
           name,
           value,
           modified
    FROM poco
) b ON b.id = (a.id + 10);";

                public const string TwoTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
WHERE one.""Id"" = :id;
";

                public const string ThreeTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
WHERE one.""Id"" = :id;

";

                public const string FourTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
WHERE one.""Id"" = :id;

";

                public const string FiveTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
WHERE one.""Id"" = :id;

";

                public const string SixTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
WHERE one.""Id"" = :id;

";

                public const string SevenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
WHERE one.""Id"" = :id;
";

                public const string EightTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
WHERE one.""Id"" = :id;

";

                public const string NineTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
WHERE one.""Id"" = :id;
";

                public const string TenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
WHERE one.""Id"" = :id;
";

                public const string ElevenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
JOIN data eleven ON eleven.""Id"" = (ten.""Id"" + 1)
WHERE one.""Id"" = :id;
";

                public const string TwelveTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
JOIN data eleven ON eleven.""Id"" = (ten.""Id"" + 1)
JOIN data twelve ON twelve.""Id"" = (eleven.""Id"" + 1)
WHERE one.""Id"" = :id;

";

                public const string ThirteenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
JOIN data eleven ON eleven.""Id"" = (ten.""Id"" + 1)
JOIN data twelve ON twelve.""Id"" = (eleven.""Id"" + 1)
JOIN data thirteen ON thirteen.""Id"" = (twelve.""Id"" + 1)
WHERE one.""Id"" = :id;

";


                public const string FourteenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
JOIN data eleven ON eleven.""Id"" = (ten.""Id"" + 1)
JOIN data twelve ON twelve.""Id"" = (eleven.""Id"" + 1)
JOIN data thirteen ON thirteen.""Id"" = (twelve.""Id"" + 1)
JOIN data fourteen ON fourteen.""Id"" = (thirteen.""Id"" + 1)
WHERE one.""Id"" = :id;
";


                public const string FifteenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
JOIN data eleven ON eleven.""Id"" = (ten.""Id"" + 1)
JOIN data twelve ON twelve.""Id"" = (eleven.""Id"" + 1)
JOIN data thirteen ON thirteen.""Id"" = (twelve.""Id"" + 1)
JOIN data fourteen ON fourteen.""Id"" = (thirteen.""Id"" + 1)
JOIN data fifteen ON fifteen.""Id"" = (fourteen.""Id"" + 1)
WHERE one.""Id"" = :id;
";

                public const string SixteenTypesWithParameters = @"WITH data AS (
    SELECT id AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
JOIN data four ON four.""Id"" = (three.""Id"" + 1)
JOIN data five ON five.""Id"" = (four.""Id"" + 1)
JOIN data six ON six.""Id"" = (five.""Id"" + 1)
JOIN data seven ON seven.""Id"" = (six.""Id"" + 1)
JOIN data eight ON eight.""Id"" = (seven.""Id"" + 1)
JOIN data nine ON nine.""Id"" = (eight.""Id"" + 1)
JOIN data ten ON ten.""Id"" = (nine.""Id"" + 1)
JOIN data eleven ON eleven.""Id"" = (ten.""Id"" + 1)
JOIN data twelve ON twelve.""Id"" = (eleven.""Id"" + 1)
JOIN data thirteen ON thirteen.""Id"" = (twelve.""Id"" + 1)
JOIN data fourteen ON fourteen.""Id"" = (thirteen.""Id"" + 1)
JOIN data fifteen ON fifteen.""Id"" = (fourteen.""Id"" + 1)
JOIN data sixteen ON sixteen.""Id"" = (fifteen.""Id"" + 1)
WHERE one.""Id"" = :id;
";
            }

            public static class Multiple
            {

                public const string OneTypeMultiple = @"select * from poco where id < 2;";

                public const string TwoTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
";

                public const string ThreeTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
";

                public const string FourTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
";

                public const string FiveTypeMultiple = @"select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
";

                public const string SixTypeMultiple = @"select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
";

                public const string SevenTypeMultiple = @"select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
";

                public const string EightTypeMultiple = @"select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
";

                public const string NineTypeMultiple = @"select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
";

                public const string TenTypeMultiple = @"select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
";

                public const string ElevenTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
select * from poco where id < 12;
";

                public const string TwelveTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
select * from poco where id < 12;
select * from poco where id < 13;
";

                public const string ThirteenTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
select * from poco where id < 12;
select * from poco where id < 13;
select * from poco where id < 14;
";

                public const string FourteenTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
select * from poco where id < 12;
select * from poco where id < 13;
select * from poco where id < 14;
select * from poco where id < 15;
";

                public const string FifteenTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
select * from poco where id < 12;
select * from poco where id < 13;
select * from poco where id < 14;
select * from poco where id < 15;
select * from poco where id < 16;
";

                public const string SixteenTypeMultiple = @"
select * from poco where id < 2;
select * from poco where id < 3;
select * from poco where id < 4;
select * from poco where id < 5;
select * from poco where id < 6;
select * from poco where id < 7;
select * from poco where id < 8;
select * from poco where id < 9;
select * from poco where id < 10;
select * from poco where id < 11;
select * from poco where id < 12;
select * from poco where id < 13;
select * from poco where id < 14;
select * from poco where id < 15;
select * from poco where id < 16;
select * from poco where id < 17;
";
            }

        }

        public static class Execute
        {
            public const string ExceptionsAreReturnedToCaller = @"SELECT 1/0;";

            public const string SupportParameterlessCalls = @"CREATE TEMP TABLE result (
    Value int
);
INSERT INTO result (Value) VALUES (1);
SELECT * FROM result;
DROP TABLE result;";

            public const string SupportsRollbackOnParameterlessCallsCount = @"SELECT count(1) AS result FROM poco;";

            public const string SupportsRollbackOnParameterlessCalls = @"BEGIN;
DELETE FROM poco;
-- This will cause an error and rollback in a transaction block
SELECT 1 / 0;
ROLLBACK;";

            public const string SupportsSuppressedDistributedTransactions = @"INSERT INTO poco (name, value, modified)
VALUES (@Name, @Value, @Modified);";

            public const string SupportsTransactionRollbackCount = @"SELECT * FROM poco WHERE name = @Name;";

            public const string SupportsTransactionRollback = @"INSERT INTO poco (name, value)
VALUES (@Name, @Value * POWER(@Value, @Value));
";

            public const string SupportsEnlistingInAmbientTransactions = @"
INSERT INTO distributed_transaction (name, value, modified)
VALUES (@Name, @Value, @Modified);
";
            public const string SuccessfullyWithResponse = @"
INSERT INTO poco (name, value, modified)
VALUES (@Name, @Value, @Modified);
";
            public const string SuccessfullyWithResponseResponse = @"SELECT id, name, value, modified FROM poco WHERE name = @Name;";

            public const string Successful = @"
INSERT INTO poco (name, value, modified)
VALUES (@Name, @Value, @Modified);
";

            public const string SingleType = @"
INSERT INTO poco (name, value, modified)
VALUES (@Name, @Value, @Modified);
";
        }

        public static class Dispose
        {
            public const string Successfully = @"SELECT floor(random() * 100)::int;";
        }
    }
}
namespace Syrx.Oracle.Tests.Integration
{
    public static class OracleCommandStrings
    {
        public const string Alias = "Syrx.Oracle";
        
        public static class Setup
        {
            public const string DropPocoTable = "drop table poco";
            public const string DropWritesTable = "drop table writes";
            public const string DropIdentityInsertTable = "drop table identity_tester";
            public const string DropBulkInsertTable = "drop table bulk_insert";
            public const string DropDistributeedTransactionTable = "drop table distributed_transaction";

            public const string CreatePocoTable = @"
            CREATE TABLE poco (
                id NUMBER(3),
                name VARCHAR2(50),
                value NUMBER(18, 2),
                modified TIMESTAMP(3)
            )";

            public const string CreateWritesTable = @"
            CREATE TABLE writes (
                id NUMBER(3),
                name VARCHAR2(50),
                value NUMBER(18, 2),
                modified TIMESTAMP(3)
            )";

            public const string CreateIdentityTesterTable = @"
        CREATE TABLE identity_tester (
            id NUMBER(3),
            name VARCHAR2(50),
            value NUMBER(18, 2),
            modified TIMESTAMP(3)
        )";

            public const string CreateBulkInsertTable = @"
        CREATE TABLE bulk_insert (
            id NUMBER(3),
            name VARCHAR2(50),
            value NUMBER(18, 2),
            modified TIMESTAMP(3)
        )";

            public const string CreateDistributedTransactionTable = @"
        CREATE TABLE distributed_transaction (
            id NUMBER(3),
            name VARCHAR2(50),
            value NUMBER(18, 2),
            modified TIMESTAMP(3)
        )";

            public const string CreateTable = "create_dynamic_table";

            public const string TableChecker = "select table_name from user_tables";

            public const string DropIdentityTesterProcedure = @"
DECLARE
    v_exists NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_exists
    FROM user_objects
    WHERE object_type = 'PROCEDURE' AND object_name = 'usp_identity_tester';

    IF v_exists > 0 THEN
        EXECUTE IMMEDIATE 'DROP PROCEDURE usp_identity_tester';
        DBMS_OUTPUT.PUT_LINE('Procedure dropped successfully.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Procedure does not exist.');
    END IF;
END;
";

            public const string CreateIdentityTesterProcedure = @"
CREATE OR REPLACE FUNCTION usp_identity_tester(_name IN VARCHAR2, _value IN NUMBER)
RETURN sys.odcinumberlist PIPELINED
IS
    _id NUMBER;
BEGIN
    INSERT INTO identity_test(name, value, modified)
    VALUES (_name, _value, CURRENT_TIMESTAMP)
    RETURNING id INTO _id;

    PIPE ROW(_id);
    RETURN;
END;
";
                        
            public const string DropBulkInsertProcedure = @"
DECLARE
    v_exists NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_exists
    FROM user_objects
    WHERE object_type = 'PROCEDURE' AND object_name = 'usp_bulk_insert';

    IF v_exists > 0 THEN
        EXECUTE IMMEDIATE 'DROP PROCEDURE usp_bulk_insert';
        DBMS_OUTPUT.PUT_LINE('Procedure dropped successfully.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Procedure does not exist.');
    END IF;
END;
";

            public const string CreateBulkInsertProcedure = @"CREATE OR REPLACE PROCEDURE usp_bulk_insert(p_path IN VARCHAR2) IS
    v_command VARCHAR2(32767);
    v_template VARCHAR2(32767) := 'BEGIN
        EXECUTE IMMEDIATE ''BULK INSERT bulk_insert FROM ''''%path'''' WITH (fieldterminator = '''','''', rowterminator = ''''\n'''')'';
    END;';
BEGIN
    v_command := REPLACE(v_template, '%path', p_path);
    EXECUTE IMMEDIATE v_command;
END;
";

            public const string DropBulkInsertAndReturnProcedure = @"
DECLARE
    v_exists NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_exists
    FROM user_objects
    WHERE object_type = 'PROCEDURE' AND object_name = 'usp_bulk_insert_and_return';

    IF v_exists > 0 THEN
        EXECUTE IMMEDIATE 'DROP PROCEDURE usp_bulk_insert_and_return';
        DBMS_OUTPUT.PUT_LINE('Procedure dropped successfully.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Procedure does not exist.');
    END IF;
END;
";

            public const string CreateBulkInsertAndReturnProcedure = @"CREATE OR REPLACE PROCEDURE usp_bulk_insert_and_return(p_path IN VARCHAR2) IS
    v_command VARCHAR2(32767);
    v_template VARCHAR2(32767) := 'BEGIN
        EXECUTE IMMEDIATE ''BULK INSERT bulk_insert FROM ''''%path'''' WITH (fieldterminator = '''','''', rowterminator = ''''\n'''')'';
    END;';
BEGIN
    v_command := REPLACE(v_template, '%path', p_path);
    EXECUTE IMMEDIATE v_command;

    -- Assuming you want to return all rows from the bulk_insert table
    FOR rec IN (SELECT * FROM bulk_insert) LOOP
        DBMS_OUTPUT.PUT_LINE('ID: ' || rec.id || ', Name: ' || rec.name || ', Value: ' || rec.value || ', Modified: ' || rec.modified);
    END LOOP;
END;
";

            public const string DropTableClearingProcedure = @"
DECLARE
    v_exists NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_exists
    FROM user_objects
    WHERE object_type = 'PROCEDURE' AND object_name = 'usp_clear_table';

    IF v_exists > 0 THEN
        EXECUTE IMMEDIATE 'DROP PROCEDURE usp_clear_table';
        DBMS_OUTPUT.PUT_LINE('Procedure dropped successfully.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Procedure does not exist.');
    END IF;
END;
";

            public const string CreateTableClearingProcedure = @"CREATE OR REPLACE PROCEDURE usp_clear_table(name IN VARCHAR2) IS
    v_sql VARCHAR2(32767);
BEGIN
    v_sql := 'TRUNCATE TABLE ' || p_name;
    EXECUTE IMMEDIATE v_sql;
    DBMS_OUTPUT.PUT_LINE('Table ' || p_name || ' truncated successfully.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Error truncating table: ' || SQLERRM);
END;
";

            public const string ClearTable = @"
DELETE FROM poco
";

            public const string Populate = "INSERT INTO poco(id, name, value, modified) VALUES (:Id, :Name, :Value, :modified)";

        }

        public static class Query
        {
            public static class Multimap
            {
                public const string ExceptionsAreReturnedToCaller = @"invalid command;";

                public const string SingleType = @"SELECT CAST(id AS NUMBER(5)) AS ""Id"",
                       name AS ""Name"",
                       value AS ""Value"",
                       modified AS ""Modified""
                FROM poco";
                                
                public const string SingleTypeWithParameters = @"SELECT CAST(id AS NUMBER(5)) AS ""Id"",
       name AS ""Name"",
       value AS ""Value"",
       modified AS ""Modified""
FROM poco
WHERE id = :id";

                public const string TwoTypes = @"
WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 10)";

                public const string TwoTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
WHERE one.""Id"" = :id
";

                public const string ThreeTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
           name AS ""Name"",
           value AS ""Value"",
           modified AS ""Modified""
    FROM poco
)
SELECT *
FROM data one
JOIN data two ON two.""Id"" = (one.""Id"" + 1)
JOIN data three ON three.""Id"" = (two.""Id"" + 1)
WHERE one.""Id"" = :id

";

                public const string FourTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id

";

                public const string FiveTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id

";

                public const string SixTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id

";

                public const string SevenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";

                public const string EightTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id

";

                public const string NineTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";

                public const string TenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";

                public const string ElevenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";

                public const string TwelveTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id

";

                public const string ThirteenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id

";

                public const string FourteenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";

                public const string FifteenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";

                public const string SixteenTypesWithParameters = @"WITH data AS (
    SELECT CAST(id AS NUMBER(5)) AS ""Id"",
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
WHERE one.""Id"" = :id
";
            }

            public static class Multiple
            {
                public const string OneTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
END;
";
                
                public const string TwoTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
END;
";

                public const string ThreeTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
END;
";

                public const string FourTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
END;
";

                public const string FiveTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
END;
";

                public const string SixTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
END;
";

                public const string SevenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
END;
";

                public const string EightTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
END;
";

                public const string NineTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
END;
";

                public const string TenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
END;
";

                public const string ElevenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
    OPEN :11 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 12;
END;
";

                public const string TwelveTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
    OPEN :11 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 12;
    OPEN :12 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 13;
END;
";

                public const string ThirteenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
    OPEN :11 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 12;
    OPEN :12 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 13;
    OPEN :13 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 14;
END;
";

                public const string FourteenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
    OPEN :11 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 12;
    OPEN :12 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 13;
    OPEN :13 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 14;
    OPEN :14 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 15;
END;
";

                public const string FifteenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
    OPEN :11 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 12;
    OPEN :12 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 13;
    OPEN :13 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 14;
    OPEN :14 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 15;
    OPEN :15 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 16;
END;
";
                
                public const string SixteenTypeMultiple = @"
BEGIN
    OPEN :1 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 2;
    OPEN :2 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 3;
    OPEN :3 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 4;
    OPEN :4 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 5;
    OPEN :5 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 6;
    OPEN :6 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 7;
    OPEN :7 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 8;
    OPEN :8 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 9;
    OPEN :9 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 10;
    OPEN :10 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 11;
    OPEN :11 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 12;
    OPEN :12 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 13;
    OPEN :13 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 14;
    OPEN :14 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 15;
    OPEN :15 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 16;
    OPEN :16 FOR select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from poco where id < 17;
END;
";
            }

        }

        public static class Execute
        {
            public const string ExceptionsAreReturnedToCaller = @"invalid command";

            public const string SupportParameterlessCalls = @"
INSERT INTO writes (id, name, value) VALUES (200, 'SupportParameterlessCalls', 1337.123)
";

            public const string SupportsRollbackOnParameterlessCallsCount = @"SELECT count(1) AS result FROM writes";

            public const string SupportsRollbackOnParameterlessCalls = @"
BEGIN
    DELETE FROM writes
:ddd
ROLLBACK";

            public const string SupportsSuppressedDistributedTransactions = @"
INSERT INTO writes (id, name, value, modified) VALUES (:Id, :Name, :Value, :Modified)
";

            public const string SupportsTransactionRollbackCount = @"select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from writes where name = :Name";

            public const string SupportsTransactionRollback = @"
DECLARE
    name VARCHAR2(50) := 'YourName'; -- Replace with the actual name value
    value NUMBER := 42; -- Replace with the actual value
BEGIN
    INSERT INTO writes (name, value)
    VALUES (name, value * POWER(value, value));
    COMMIT; -- Commit the transaction
END;
";

            public const string SupportsEnlistingInAmbientTransactions = @"
INSERT INTO distributed_transaction (id, name, value, modified) VALUES (:Id, :Name, :Value, :Modified)
";
            
            public const string SuccessfullyWithResponse = @"
INSERT INTO writes (id, name, value, modified) VALUES (:Id, :Name, :Value, :Modified)
";
            
            public const string SuccessfullyWithResponseResponse = @"select cast(id as number(5)) as ""Id"", name as ""Name"", value as ""Value"", modified as ""Modified"" from writes where name = :Name";

            public const string Successful = @"
INSERT INTO writes (id, name, value, modified) VALUES (:Id, :Name, :Value, :Modified)
";

            public const string SingleType = @"
INSERT INTO writes (id, name, value, modified) VALUES (:Id, :Name, :Value, :Modified)
";
        }

        public static class Dispose
        {
            public const string Successfully = @"SELECT floor(random() * 100)::int";
        }
    }
}
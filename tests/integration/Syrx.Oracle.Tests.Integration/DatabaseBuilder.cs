namespace Syrx.Oracle.Tests.Integration
{
    public class DatabaseBuilder
    {
        private readonly ICommander<DatabaseBuilder> _commander;

        public DatabaseBuilder(ICommander<DatabaseBuilder> commander)
        {
            _commander = commander;
        }

        public DatabaseBuilder CreateDatabase(string name = "Syrx")
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));
            _commander.Query<bool>(new { name }).SingleOrDefault();
            return this;
        }

        public DatabaseBuilder DropTableCreatorProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateTableCreatorProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }


        public DatabaseBuilder CreatePocoTable()
        {
            TableChecker("poco", nameof(OracleCommandStrings.Setup.DropPocoTable));

            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateWritesTable()
        {
            TableChecker("writes", nameof(OracleCommandStrings.Setup.DropWritesTable));

            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateIdentityTesterTable()
        {
            TableChecker("identity_tester", nameof(OracleCommandStrings.Setup.DropIdentityInsertTable));
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateBulkInsertTable()
        {
            TableChecker("bulk_insert", nameof(OracleCommandStrings.Setup.DropBulkInsertTable));
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateDistributedTransactionTable()
        {
            TableChecker("distributed_transaction", nameof(OracleCommandStrings.Setup.DropDistributeedTransactionTable));
            _commander.Execute<bool>();
            return this;
        }


        public DatabaseBuilder DropIdentityTesterProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateIdentityTesterProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder DropBulkInsertProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateBulkInsertProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder DropBulkInsertAndReturnProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateBulkInsertAndReturnProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder DropTableClearingProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder CreateTableClearingProcedure()
        {
            _commander.Execute<bool>();
            return this;
        }

        public DatabaseBuilder IsStale(DateTime date)
        {
            var modified = _commander.Query<DateTime>().SingleOrDefault();
            if (modified.Date != date)
            {
                Populate();
            }
            return this;
        }

        public DatabaseBuilder ClearTable(string name = "poco")
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));
            _commander.Execute(() =>
            {
                return _commander.Query<bool>(new { name });
            });
            return this;
        }

        public DatabaseBuilder Populate()
        {
            ClearTable();

            for (var i = 1; i < 151; i++)
            {
                var entry = new {
                    Id = i,
                    Name = $"entry {i}",
                    Value = i * 10,
                    Modified = DateTime.Today
                };

                _commander.Execute(entry);
            }

            return this;
        }

        public DatabaseBuilder TableChecker(string table, string command)
        {
            var result = _commander.Query<string>();

            if (result.Any(x => x == table.ToUpperInvariant()))
            {
                _commander.Execute<bool>(method: command);
            }

            return this;
        }

        public DatabaseBuilder Build()
        {
            CreatePocoTable();
            CreateWritesTable();
            CreateIdentityTesterTable();
            CreateBulkInsertTable();
            CreateDistributedTransactionTable();
            DropIdentityTesterProcedure();
            CreateIdentityTesterProcedure();
            DropBulkInsertProcedure();
            CreateBulkInsertProcedure();
            DropBulkInsertAndReturnProcedure();
            CreateBulkInsertAndReturnProcedure();
            DropTableClearingProcedure();
            CreateTableClearingProcedure();
            ClearTable();
            Populate();

            return this;
        }
    }
}

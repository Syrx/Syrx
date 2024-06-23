namespace Syrx.SqlServer.Tests.Integration
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

        public DatabaseBuilder CreateTable(string name = "poco")
        {
            Throw<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), nameof(name));
            _commander.Execute(() =>
            {
                return _commander.Query<bool>(new { name });
            });
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
            for (var i = 1; i < 151; i++)
            {
                var entry = new {
                    Name = $"entry {i}",
                    Value = i * 10,
                    Modified = DateTime.Today
                };

                _commander.Execute(entry);
            }

            return this;
        }

        public DatabaseBuilder Build()
        {
            return DropTableCreatorProcedure()
                .CreateTableCreatorProcedure()
                .CreateTable("poco")
                .CreateTable("identity_tester")
                .CreateTable("bulk_insert")
                .CreateTable("distributed_transaction")
                .DropIdentityTesterProcedure()
                .CreateIdentityTesterProcedure()
                .DropBulkInsertProcedure()
                .CreateBulkInsertProcedure()
                .DropBulkInsertAndReturnProcedure()
                .CreateBulkInsertAndReturnProcedure()
                .DropTableClearingProcedure()
                .CreateTableClearingProcedure()
                .ClearTable()
                .Populate();
        }
    }
}

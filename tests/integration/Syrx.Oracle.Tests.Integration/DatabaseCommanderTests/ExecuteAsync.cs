namespace Syrx.Oracle.Tests.Integration.DatabaseCommanderTests
{
    [Collection(nameof(FixtureCollection))]
    public class ExecuteAsync(BaseFixture fixture) 
    {
        private readonly ICommander<Execute> _commander = fixture.GetCommander<Execute>();

        [Fact]
        public async Task ExceptionsAreReturnedToCaller()
        {
            var result = await ThrowsAnyAsync<Exception>(() => _commander.ExecuteAsync(new { value = 1 }));
            var expected = $"ORA-00900: invalid SQL statement{ Environment.NewLine }https://docs.oracle.com/error-help/db/ora-00900/";
            result.HasMessage(expected);
        }

        //[Fact]
        [Fact]
        public async Task SupportParameterlessCalls()
        {
            var result = await _commander.ExecuteAsync<bool>();
            True(result);
        }

        [Fact]
        public async Task SupportsRollbackOnParameterlessCalls()
        {
            // get a count from [dbo].[Poco]
            // try to delete a result.
            //  throw exception
            // check count again. should match.
            var method = $"{nameof(Execute.SupportsRollbackOnParameterlessCalls)}.Count";
            var preCount = _commander.Query<int>(method: method);
            var result = await ThrowsAnyAsync<Exception>(() => _commander.ExecuteAsync<bool>());
            var postCount = _commander.Query<int>(method: method);

            result.Message.PrintAsJson();

            StartsWith("ORA-06550", result.Message);

            Equal(preCount, postCount);
        }

        [Fact]
        public async Task SupportsSuppressedDistributedTransactions()
        {
            var one = new ImmutableType(1, Guid.NewGuid().ToString(), 1, DateTime.UtcNow);
            var two = new ImmutableType(2, Guid.NewGuid().ToString(), 2, DateTime.UtcNow);

            var result = await _commander.ExecuteAsync(() =>
            {
                var a = _commander.Execute(one) ? one : null;//, (a) => a);
                var b = _commander.Execute(two) ? two : null; // (b) => b);

                return new ImmutableTwoType<ImmutableType, ImmutableType, ImmutableType>(a, b);
            });

            NotNull(result);
            NotNull(result.One);
            NotNull(result.Two);

            Same(one, result.One);
            Same(two, result.Two);
        }

        [Fact]
        public async Task SupportsTransactionRollback()
        {
            var method = $"{nameof(Execute.SupportsTransactionRollback)}.Count";

            var model = new ImmutableType(1, Guid.NewGuid().ToString(), int.MaxValue, DateTime.UtcNow);

            var result = await ThrowsAnyAsync<Exception>(() => _commander.ExecuteAsync(model));
            var expected =
                $"ORA-01438: value larger than specified precision allowed for this column{Environment.NewLine}ORA-06512: at line 6{Environment.NewLine}https://docs.oracle.com/error-help/db/ora-01438/";
            result.HasMessage(expected);

            // check if the result has been rolled back.
            // ReSharper disable once ExplicitCallerInfoArgument
            var record = _commander.Query<ImmutableType>(new { model.Name }, method: method);
            NotNull(record);
            False(record.Any());
        }

        [Theory(Skip = "Not supported by Oracle.")]
        [MemberData(nameof(TransactionScopeOptions))]
        public async Task SupportsEnlistingInAmbientTransactions(TransactionScopeOption scopeOption)
        {
            var name = Enum.GetName(typeof(TransactionScopeOption), scopeOption);

            var one = new ImmutableType(1, $"{name}--{Guid.NewGuid()}", 1, DateTime.UtcNow);
            var two = new ImmutableType(2, $"{name}--{Guid.NewGuid()}", 2, DateTime.UtcNow);

            var result = await _commander.ExecuteAsync(() =>
            {
                var a = _commander.Execute(one) ? one : null;//, (a) => a);
                var b = _commander.Execute(two) ? two : null; // (b) => b);

                return new ImmutableTwoType<ImmutableType, ImmutableType, ImmutableType>(a, b);
            }, scopeOption);

            NotNull(result);
            NotNull(result.One);
            NotNull(result.Two);

            Same(one, result.One);
            Same(two, result.Two);
        }

        [Fact]
        public async Task SuccessfullyWithResponse()
        {
            var random = new Random();
            var overload = $"{nameof(SuccessfullyWithResponse)}.Response";
            var one = new ImmutableType(500, $"{nameof(ImmutableType)}.{random.Next(int.MaxValue)}", random.Next(int.MaxValue), DateTime.UtcNow);
            var result = await _commander.ExecuteAsync(() =>
            {
                return _commander.Execute(one) ?
                        _commander.Query<ImmutableType>(new { name = one.Name }, overload).SingleOrDefault()
                        : null;
            }
            );

            NotEqual(one, result);
            Equal(one.Id, result!.Id);
            Equal(one.Name, result.Name);
            Equal(one.Value, result.Value);
        }

        [Fact]
        public async Task Successful()
        {
            var random = new Random();
            var one = new ImmutableType(500, nameof(ImmutableType), random.Next(int.MaxValue), DateTime.UtcNow);
            var result = await _commander.ExecuteAsync(one);
            True(result);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SingleTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task SingleType<T1>(SingleType<T1> input)
        {
            var result = await _commander.ExecuteAsync(input.One);
            True(result);
        }

        public static TheoryData<TransactionScopeOption> TransactionScopeOptions => new()
            {
            TransactionScopeOption.Required,
            TransactionScopeOption.RequiresNew,
            TransactionScopeOption.Suppress
            };

    }

}

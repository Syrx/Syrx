﻿namespace Syrx.Npgsql.Tests.Integration.DatabaseCommanderTests
{
    [Collection(BaseFixture.ExecuteFixtureCollectionDefinition)]
    public class Execute(ExecuteFixture fixture) //: IClassFixture<ExecuteFixture>
    {
        private readonly ICommander<Execute> _commander = fixture.GetCommander<Execute>();

        [Fact]
        public void ExceptionsAreReturnedToCaller()
        {
            var result = ThrowsAny<Exception>(() => _commander.Execute(new { value = 1 }));
            //result.DivideByZero();
            result.HasMessage("22012: division by zero");
        }

        [Fact]
        public void SupportParameterlessCalls()
        {
            var result = _commander.Execute<bool>();
            True(result);
        }

        [Fact]
        public void SupportsRollbackOnParameterlessCalls()
        {
            // get a count from [dbo].[Poco]
            // try to delete a result.
            //  throw exception
            // check count again. should match.
            var method = $"{nameof(SupportsRollbackOnParameterlessCalls)}.Count";
            var preCount = _commander.Query<int>(method: method);
            var result = ThrowsAny<Exception>(() => _commander.Execute<bool>());
            var postCount = _commander.Query<int>(method: method);

            //result.DivideByZero();
            result.HasMessage("22012: division by zero");
            Equal(preCount, postCount);
        }

        [Fact]
        public void SupportsSuppressedDistributedTransactions()
        {
            var one = new ImmutableType(1, Guid.NewGuid().ToString(), 1, DateTime.UtcNow);
            var two = new ImmutableType(2, Guid.NewGuid().ToString(), 2, DateTime.UtcNow);

            var result = _commander.Execute(() =>
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
        public void SupportsTransactionRollback()
        {
            var method = $"{nameof(SupportsTransactionRollback)}.Count";

            var model = new ImmutableType(1, Guid.NewGuid().ToString(), int.MaxValue, DateTime.UtcNow);

            var result = ThrowsAny<Exception>(() => _commander.Execute(model));
            //const string expected = "Arithmetic overflow error converting expression to data type float.\r\nThe statement has been terminated.";
            const string expected = "22003: value overflows numeric format";
            result.HasMessage(expected);

            // check if the result has been rolled back.
            var record = _commander.Query<ImmutableType>(new { model.Name }, method: method);
            NotNull(record);
            False(record.Any());
        }

        [Theory(Skip = "Not supported by Postgres.")]
        [MemberData(nameof(TransactionScopeOptions))]
        public void SupportsEnlistingInAmbientTransactions(TransactionScopeOption scopeOption)
        {
            var name = Enum.GetName(typeof(TransactionScopeOption), scopeOption);

            var one = new ImmutableType(1, $"{name}--{Guid.NewGuid()}", 1, DateTime.UtcNow);
            var two = new ImmutableType(2, $"{name}--{Guid.NewGuid()}", 2, DateTime.UtcNow);

            var result = _commander.Execute(() =>
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
        public void SuccessfullyWithResponse()
        {
            var random = new Random();
            var overload = $"{nameof(SuccessfullyWithResponse)}.Response";
            var one = new ImmutableType(500, nameof(ImmutableType), random.Next(int.MaxValue), DateTime.UtcNow);
            var result = _commander.Execute(() =>
            {
                return _commander.Execute(one) ?
                        _commander.Query<ImmutableType>(new { name = one.Name }, overload).FirstOrDefault()
                        : null;
            }
            );

            NotEqual(one, result);
            NotEqual(one.Id, result.Id);
            Equal(one.Name, result.Name);
            Equal(one.Value, result.Value);
        }

        [Fact]
        public void Successful()
        {
            var random = new Random();
            var one = new ImmutableType(500, nameof(ImmutableType), random.Next(int.MaxValue), DateTime.UtcNow);
            var result = _commander.Execute(one);
            True(result);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SingleTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public void SingleType<T1>(int id, SingleType<T1> input)
        {
            var result = _commander.Execute(input.One);
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

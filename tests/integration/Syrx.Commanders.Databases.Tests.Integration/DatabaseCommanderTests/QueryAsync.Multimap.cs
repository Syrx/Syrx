﻿
namespace Syrx.Commanders.Databases.Tests.Integration.DatabaseCommanderTests
{
    public abstract partial class QueryAsync(BaseFixture fixture) : IClassFixture<BaseFixture>
    {
        private readonly ICommander<Query> _commander = fixture.GetCommander<Query>();
        
        [Fact]
        public async Task ExceptionsAreReturnedToCaller()
        {
            var result = await ThrowsAnyAsync<Exception>(() => _commander.QueryAsync<int>());
            const string expected = "Divide by zero error encountered.";
            Equal(expected, result.Message);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SingleTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task SingleType<T1>(int id, SingleType<T1> input)
        {
            var result = await _commander.QueryAsync<T1>();

            NotNull(result);
            Equal(150, result.Count());

        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SingleTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task SingleTypeWithParameters<T1>(int id, SingleType<T1> input)
        {
            Console.WriteLine($"Executing {nameof(SingleTypeWithParameters)} against type {typeof(T1).FullName}");
            var result = await _commander.QueryAsync<T1>(new { id });

            var single = result.Single();

            NotNull(result);
            Equivalent(input.One, single);
            Equal(input.One, single);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.TwoTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task TwoTypes<T1, T2, TResult>(int id, int expect, TwoType<T1, T2, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map);

            // assert we got something back and
            // that the count should match what we've 
            // defined on the ModelDataGenerator
            NotNull(result);
            Equal(expect, result.Count());
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.TwoTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task TwoTypesWithParameters<T1, T2, TResult>(int id, int expect, TwoType<T1, T2, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.ThreeTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task ThreeTypesWithParameters<T1, T2, T3, TResult>(int id, int expect, ThreeType<T1, T2, T3, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }


        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.FourTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task FourTypesWithParameters<T1, T2, T3, T4, TResult>(int id, int expect, FourType<T1, T2, T3, T4, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.FiveTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task FiveTypesWithParameters<T1, T2, T3, T4, T5, TResult>(int id, int expect, FiveType<T1, T2, T3, T4, T5, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SixTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task SixTypesWithParameters<T1, T2, T3, T4, T5, T6, TResult>(int id, int expect, SixType<T1, T2, T3, T4, T5, T6, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SevenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task SevenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, TResult>(int id, int expect, SevenType<T1, T2, T3, T4, T5, T6, T7, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.EightTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task EightTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(int id, int expect, EightType<T1, T2, T3, T4, T5, T6, T7, T8, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.NineTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task NineTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(int id, int expect, NineType<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.TenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task TenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(int id, int expect, TenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.ElevenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task ElevenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(int id, int expect, ElevenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten,
                input.Eleven);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.TwelveTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task TwelveTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(int id, int expect, TwelveType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten,
                input.Eleven,
                input.Twelve);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.ThirteenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task ThirteenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(int id, int expect, ThirteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten,
                input.Eleven,
                input.Twelve,
                input.Thirteen);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.FourteenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task FourteenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(int id, int expect, FourteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten,
                input.Eleven,
                input.Twelve,
                input.Thirteen,
                input.Fourteen);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.FifteenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task FifteenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(int id, int expect, FifteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten,
                input.Eleven,
                input.Twelve,
                input.Thirteen,
                input.Fourteen,
                input.Fifteen);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multimap.SixteenTypeData), MemberType = typeof(ModelGenerators.Multimap))]
        public async Task SixteenTypesWithParameters<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(int id, int expect, SixteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> input)
        {
            var map = input.Map;
            var result = await _commander.QueryAsync(map, new { id });

            // assert that we got _something_ back
            NotNull(result);
            Single(result);

            // let's set up the values for assertions
            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight,
                input.Nine,
                input.Ten,
                input.Eleven,
                input.Twelve,
                input.Thirteen,
                input.Fourteen,
                input.Fifteen,
                input.Sixteen);
            var actual = result.Single();

            // now compare equivalence and equality
            Equivalent(expected, actual);
            Equal(expected, actual);
        }


    }
}

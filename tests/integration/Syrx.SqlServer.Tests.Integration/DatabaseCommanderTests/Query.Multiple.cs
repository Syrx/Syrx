namespace Syrx.SqlServer.Tests.Integration.DatabaseCommanderTests
{
    public partial class Query
    {

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.OneType), MemberType = typeof(ModelGenerators.Multiple))]
        public void OneTypeMultiple<T1, TResult>(OneType<IEnumerable<T1>, IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var parameters = input.Parameters;
            var method = input.Method;
            
            var result = _commander.Query(map, parameters, method);
            
            NotNull(result);
            Single(result);

            var expect = map(input.One);
            Equivalent(expect, result);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.TwoType), MemberType = typeof(ModelGenerators.Multiple))]
        public void TwoTypeMultiple<T1, T2, TResult>(TwoType<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expect = map(input.One, input.Two);
            Equivalent(expect, result);

        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.ThreeType), MemberType = typeof(ModelGenerators.Multiple))]
        public void ThreeTypeMultiple<T1, T2, T3, TResult>(
          ThreeType<
              IEnumerable<T1>,
              IEnumerable<T2>,
              IEnumerable<T3>,
              IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expected = map(
                input.One,
                input.Two,
                input.Three);
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.FourType), MemberType = typeof(ModelGenerators.Multiple))]
        public void FourTypeMultiple<T1, T2, T3, T4, TResult>(
          FourType<
              IEnumerable<T1>,
              IEnumerable<T2>,
              IEnumerable<T3>,
              IEnumerable<T4>,
              IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four);
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.FiveType), MemberType = typeof(ModelGenerators.Multiple))]
        public void FiveTypeMultiple<T1, T2, T3, T4, T5, TResult>(
          FiveType<
              IEnumerable<T1>,
              IEnumerable<T2>,
              IEnumerable<T3>,
              IEnumerable<T4>,
              IEnumerable<T5>,
              IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five);
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.SixType), MemberType = typeof(ModelGenerators.Multiple))]
        public void SixTypeMultiple<T1, T2, T3, T4, T5, T6, TResult>(
           SixType<
               IEnumerable<T1>,
               IEnumerable<T2>,
               IEnumerable<T3>,
               IEnumerable<T4>,
               IEnumerable<T5>,
               IEnumerable<T6>,
               IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six);
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.SevenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void SevenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, TResult>(
           SevenType<
               IEnumerable<T1>,
               IEnumerable<T2>,
               IEnumerable<T3>,
               IEnumerable<T4>,
               IEnumerable<T5>,
               IEnumerable<T6>,
               IEnumerable<T7>,
               IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven);
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.EightType), MemberType = typeof(ModelGenerators.Multiple))]
        public void EightTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
           EightType<
               IEnumerable<T1>,
               IEnumerable<T2>,
               IEnumerable<T3>,
               IEnumerable<T4>,
               IEnumerable<T5>,
               IEnumerable<T6>,
               IEnumerable<T7>,
               IEnumerable<T8>,
               IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

            var expected = map(
                input.One,
                input.Two,
                input.Three,
                input.Four,
                input.Five,
                input.Six,
                input.Seven,
                input.Eight);
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.NineType), MemberType = typeof(ModelGenerators.Multiple))]
        public void NineTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
           NineType<
               IEnumerable<T1>,
               IEnumerable<T2>,
               IEnumerable<T3>,
               IEnumerable<T4>,
               IEnumerable<T5>,
               IEnumerable<T6>,
               IEnumerable<T7>,
               IEnumerable<T8>,
               IEnumerable<T9>,
               IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.TenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void TenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
           TenType<
               IEnumerable<T1>,
               IEnumerable<T2>,
               IEnumerable<T3>,
               IEnumerable<T4>,
               IEnumerable<T5>,
               IEnumerable<T6>,
               IEnumerable<T7>,
               IEnumerable<T8>,
               IEnumerable<T9>,
               IEnumerable<T10>,
               IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.ElevenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void ElevenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
           ElevenType<
               IEnumerable<T1>,
               IEnumerable<T2>,
               IEnumerable<T3>,
               IEnumerable<T4>,
               IEnumerable<T5>,
               IEnumerable<T6>,
               IEnumerable<T7>,
               IEnumerable<T8>,
               IEnumerable<T9>,
               IEnumerable<T10>,
               IEnumerable<T11>,
               IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.TwelveType), MemberType = typeof(ModelGenerators.Multiple))]
        public void TwelveTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            TwelveType<
                IEnumerable<T1>,
                IEnumerable<T2>,
                IEnumerable<T3>,
                IEnumerable<T4>,
                IEnumerable<T5>,
                IEnumerable<T6>,
                IEnumerable<T7>,
                IEnumerable<T8>,
                IEnumerable<T9>,
                IEnumerable<T10>,
                IEnumerable<T11>,
                IEnumerable<T12>,
                IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.ThirteenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void ThirteenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            ThirteenType<
                IEnumerable<T1>,
                IEnumerable<T2>,
                IEnumerable<T3>,
                IEnumerable<T4>,
                IEnumerable<T5>,
                IEnumerable<T6>,
                IEnumerable<T7>,
                IEnumerable<T8>,
                IEnumerable<T9>,
                IEnumerable<T10>,
                IEnumerable<T11>,
                IEnumerable<T12>,
                IEnumerable<T13>,
                IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.FourteenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void FourteenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            FourteenType<
                IEnumerable<T1>,
                IEnumerable<T2>,
                IEnumerable<T3>,
                IEnumerable<T4>,
                IEnumerable<T5>,
                IEnumerable<T6>,
                IEnumerable<T7>,
                IEnumerable<T8>,
                IEnumerable<T9>,
                IEnumerable<T10>,
                IEnumerable<T11>,
                IEnumerable<T12>,
                IEnumerable<T13>,
                IEnumerable<T14>,
                IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }

        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.FifteenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void FifteenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            FifteenType<
                IEnumerable<T1>,
                IEnumerable<T2>,
                IEnumerable<T3>,
                IEnumerable<T4>,
                IEnumerable<T5>,
                IEnumerable<T6>,
                IEnumerable<T7>,
                IEnumerable<T8>,
                IEnumerable<T9>,
                IEnumerable<T10>,
                IEnumerable<T11>,
                IEnumerable<T12>,
                IEnumerable<T13>,
                IEnumerable<T14>,
                IEnumerable<T15>,
                IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);

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
            Equivalent(expected, result, true);
        }


        [Theory]
        [MemberData(nameof(ModelGenerators.Multiple.SixteenType), MemberType = typeof(ModelGenerators.Multiple))]
        public void SixteenTypeMultiple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            SixteenType<
                IEnumerable<T1>, 
                IEnumerable<T2>, 
                IEnumerable<T3>, 
                IEnumerable<T4>, 
                IEnumerable<T5>, 
                IEnumerable<T6>, 
                IEnumerable<T7>, 
                IEnumerable<T8>,
                IEnumerable<T9>,
                IEnumerable<T10>,
                IEnumerable<T11>,
                IEnumerable<T12>,
                IEnumerable<T13>,
                IEnumerable<T14>,
                IEnumerable<T15>,
                IEnumerable<T16>,
                IEnumerable<TResult>> input)
        {
            var map = input.Map;
            var result = _commander.Query(map);

            NotNull(result);
            Single(result);                      
            
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
            Equivalent(expected, result, true);
        }
    }
}

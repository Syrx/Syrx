using System.Data;

namespace Syrx.Tests.Extensions
{

    public class Generators
    {
        public static TheoryData<string> NullEmptyWhiteSpace => new()
            {
                null, string.Empty, " "
            };

        public static IEnumerable<object[]> AssignableIsolationLevels =>
            EnumExtensions
                .GetFlags<IsolationLevel>()
                .Select(level => new object[] { (int) level, level });


        public static IEnumerable<object[]> IsolationLevels =>
            EnumExtensions
                .GetFlags<IsolationLevel>()
                .Select(level => new object[] { level });

        public static TheoryData<bool> BoolValues => new() { true, false };

        public static IEnumerable<object[]> SqlDbTypes =>
            EnumExtensions.GetFlags<SqlDbType>().Select(type => new object[] { type });

        public static IEnumerable<object[]> CommandTypes
            => new List<object[]>
            {
                new object[] { CommandType.StoredProcedure },
                new object[] { CommandType.Text },
                new object[] { CommandType.TableDirect }

            };
    }
}
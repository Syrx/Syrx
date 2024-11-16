using System.Data;

namespace Syrx.Tests.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<TResult> GetFlags<TResult>(Func<TResult, bool> filter = null) where TResult : Enum
            => filter == null ?
                Enum.GetValues(typeof(TResult)).Cast<TResult>() :
                Enum.GetValues(typeof(TResult)).Cast<TResult>().Where(filter);
    }
}
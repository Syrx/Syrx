using System.Runtime.CompilerServices;

namespace Syrx.Tests.Extensions
{
    public static class DictionaryExtensions
    {
        public static string GetMessage(
            this IDictionary<string, string> messageDictionary,
            [CallerMemberName] string key = null)
        {
            return messageDictionary[key];
        }
    }
}
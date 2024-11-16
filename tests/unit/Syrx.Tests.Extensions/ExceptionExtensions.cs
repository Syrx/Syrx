using static Xunit.Assert;

namespace Syrx.Tests.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ArgumentNull(this ArgumentNullException exception, string parameter)
        {
            HasMessage(exception, $"Value cannot be null. (Parameter '{parameter}')");
        }

        public static void ArgumentOutOfRange(this ArgumentOutOfRangeException exception, string parameterName)
        {
            HasMessage(exception, $"Specified argument was out of the range of valid values. (Parameter '{parameterName}')");
        }

        public static void DuplicateKey(this ArgumentException exception, string key)
        {
            HasMessage(exception, $"An item with the same key has already been added. Key: {key}");
        }

        public static void HasMessage(this Exception exception, string message)
        {
            Equal(message.ReplaceLineEndings(), exception.Message.ReplaceLineEndings());
        }

        public static void DivideByZero(this Exception exception)
        {
            const string message = "Divide by zero error encountered.";
            Equal(message, exception.Message);
        }

        public static void Print(this Exception excetion)
        {
            Console.WriteLine($@"{excetion.Message}");
        }
    }
}
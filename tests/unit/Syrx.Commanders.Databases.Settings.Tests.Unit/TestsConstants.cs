using System.Data;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit
{
    public class TestsConstants
    {
        public class NamespaceSettings
        {
            public const string Namespace = "test-namespace";
        }

        public class TypeSettings 
        {
            public const string Name = "test-type";
        }

        public class CommandSettings
        {
            // these two are required
            public const string CommandText = "test-command-test";
            public const string ConnectionAlias = "test-alias";

            // these are deliberately not using default
            // values so that we make sure we're not asserting 
            // against defaults. 
            public const int CommandTimeout = 15;
            public static CommandType CommandType = CommandType.StoredProcedure;
            public static CommandFlagSetting Flags = CommandFlagSetting.None;
            public static IsolationLevel IsolationLevel = IsolationLevel.Chaos;
            public const string Split = "split";
        }
    }
}


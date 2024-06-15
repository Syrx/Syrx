// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (19:00)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    public class SplitResultsOn
    {
        private const string CommandText = "test-command-text";

        public SplitResultsOn()
        {
        }

        [Fact]
        public void DefaultsToNull()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToNull))
                    .UseCommandText(CommandText)
                    .SplitResultsOn()
                    .WithIsolationLevel());
            Null(result.CommandSetting.Split);
        }

        [Fact]
        public void OptionalParameterDefaultsToNull()
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToNull))
                    .UseCommandText(CommandText)
                    .WithIsolationLevel());
            Null(result.CommandSetting.Split);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void OptionalParameterAcceptsNullEmptyWhitespace(string split)
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToNull))
                    .UseCommandText(CommandText)
                    .SplitResultsOn(split)
                    .WithIsolationLevel());
            Equal(split, result.CommandSetting.Split);
        }

        [Theory]
        [MemberData(nameof(SplitValues))]
        public void Succesfully(string split)
        {
            var result = DatabaseCommandSettingOptionsBuilderExtensions
                .AddCommand(a => a
                    .ForRepositoryType<WithIsolationLevel>()
                    .ForMethodNamed(nameof(DefaultsToNull))
                    .UseCommandText(CommandText)
                    .SplitResultsOn(split)
                    .WithIsolationLevel());
            Equal(split, result.CommandSetting.Split);
        }

        /// <summary>
        /// this call should not be part of the Generators type as it's
        /// specific only to this test class.
        /// </summary>
        public static IEnumerable<object[]> SplitValues => new List<object[]>
        {
            // the split can have multiple comma separated values.
            // only the first seven will be used.
            new object[] {"Id"},
            new object[] {"Id, FieldA"},
            new object[] {"Id, FieldA, FieldB"},
            new object[] {"Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9"},
        };
    }
}
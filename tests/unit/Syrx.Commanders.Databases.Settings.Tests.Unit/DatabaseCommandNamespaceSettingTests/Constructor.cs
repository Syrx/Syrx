//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Tests.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.DatabaseCommandNamespaceSettingTests
{
    public class Constructor
    {
        public Constructor()
        {
            _types = new List<DatabaseCommandTypeSetting>
            {
                new DatabaseCommandTypeSetting("test_types", new Dictionary<string, DatabaseCommandSetting>
                {
                    ["test"] = new DatabaseCommandSetting("test", "select 1")
                })
            };
        }

        private readonly IEnumerable<DatabaseCommandTypeSetting> _types;

        [Fact]
        public void EmptyTypesListThrowsArgumentException()
        {
            var result = Throws<ArgumentException>(() =>
                new DatabaseCommandNamespaceSetting("test_namespace", new List<DatabaseCommandTypeSetting>()));
            const string expect =
                "'test_namespace' must have at least 1 Type setting to be valid. Please check settings.";
            Equal(expect, result.Message);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceNameThrowsArgumentNullException(string name)
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandNamespaceSetting(name, _types));

            const string expect =
                "Value cannot be null. (Parameter 'namespace. All namespace entries must have a name to be valid. Please check settings for an empty namespace namespace.')";
            Equal(expect, result.Message);
            //result.ArgumentNull("namespace");
        }

        [Fact]
        public void NullTypesListThrowsArgumentNullException()
        {
            var result =
                Throws<ArgumentNullException>(() => new DatabaseCommandNamespaceSetting("test_namespace", null));
            const string expect =
                "Value cannot be null. (Parameter 'types. The types collection for 'test_namespace' is null. Please check settings.')";
            Equal(expect, result.Message);

        }

        [Fact]
        public void Successfully()
        {
            var result = new DatabaseCommandNamespaceSetting("test", _types);
            Equal("test", result.Namespace);
            NotNull(result.Types);
            True(result.Types.Any());
        }
    }
}
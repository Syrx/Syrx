//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================


namespace Syrx.Commanders.Databases.Settings.Readers.Tests.Unit.DatabaseCommandReaderTests
{
    public class GetCommand
    {
        private readonly IDatabaseCommandReader _reader;
        public GetCommand()
        {
            // not using the extensions/builders in this test as those are meant
            // to enforce the correct set up where as these tests are meant to
            // exercise what happens when we load a non-standard config. 

            var namespaces = new List<DatabaseCommandNamespaceSetting>
            {
                new DatabaseCommandNamespaceSetting(
                    "Syrx",
                    new List<DatabaseCommandTypeSetting>
                    {
                        new DatabaseCommandTypeSetting(
                            "Syrx.RootNamespaceTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias.rootnamespace", "root namespace"),
                            }),
                        new DatabaseCommandTypeSetting(
                            $"{typeof(Syrx.Commanders.NotConfiguredNamespace.ParentNamespaceTest).FullName}",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                [nameof(FindsTypeInParentNamespace)] = new DatabaseCommandSetting("test.alias.parentnamespace", "parent namespace")
                            })
                    }),
                new DatabaseCommandNamespaceSetting(
                    $"{typeof(GetCommand).Namespace}",
                    new List<DatabaseCommandTypeSetting>
                    {
                        new DatabaseCommandTypeSetting(
                            $"{typeof(GetCommand).Namespace}.NoCommandSettingTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias", "dummy text")
                            })
                        ,
                        new DatabaseCommandTypeSetting(
                            $"{typeof(GetCommand).Namespace}.ParentNamespaceTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias.parentnamespace", "parent namespace")
                            })
                    }),
                new DatabaseCommandNamespaceSetting(
                    "Syrx.Testing.Readers",
                    new List<DatabaseCommandTypeSetting>
                    {
                        new DatabaseCommandTypeSetting(
                            "Syrx.Testing.Readers.FullNamespaceTest",
                            new Dictionary<string, DatabaseCommandSetting>
                            {
                                ["Retrieve"] = new DatabaseCommandSetting("test.alias.fullnamespacetest", "fullnamespacetest")
                            })
                    })
            };

            var settings = new DatabaseCommanderSettings(namespaces);

            _reader = new DatabaseCommandReader(settings);
        }


        [Fact]
        public void NoNamespaceSettingThrowsNullReferenceException()
        {
            var result = Throws<NullReferenceException>(() =>
                _reader.GetCommand(typeof(A.Syrx.Commanders.Databases.Readers.NoNamespaceType), "NoNamespaceSettingThrowsNullReferenceException"));
            Console.WriteLine(result.Message);
            Equal(
                $@"'{typeof(A.Syrx.Commanders.Databases.Readers.NoNamespaceType).FullName}' does not belong to any NamespaceSetting.
Please check settings.",
                result.Message);

        }

        [Fact]
        public void NoTypeSettingThrowsNullReferenceException()
        {
            var result = Throws<NullReferenceException>(() => _reader.GetCommand(typeof(NoTypeSettingTest), "NoTypeSettingThrowsNullReferenceException"));
            var expect = $"The type '{typeof(NoTypeSettingTest).FullName}' has no entry in the type settings of namespace '{typeof(NoTypeSettingTest).Namespace}'. Please add a type setting entry to the namespace setting.";
            Equal(expect, result.Message);
        }

        [Fact]
        public void NoCommandSettingThrowsNullReferenceException()
        {
            var result = Throws<NullReferenceException>(() => _reader.GetCommand(typeof(NoCommandSettingTest), "DoesNotExist"));
            result.Print();
            var expect = $"The command setting 'DoesNotExist' has no entry for the type setting '{typeof(NoCommandSettingTest).FullName}'. Please add a command setting entry to the type setting.";
            Equal(expect, result.Message);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceKeyThrowsArgumentNullException(string key)
        {
            var result = Throws<ArgumentNullException>(() => _reader.GetCommand(typeof(NoCommandSettingTest), key));
            result.ArgumentNull("key");
        }

        [Fact]
        public void NullTypePassedThrowsArgumentNullException()
        {
            var result = Throws<ArgumentNullException>(() => _reader.GetCommand(null, "test"));
            result.ArgumentNull("type");
        }

        [Fact]
        public void FindsSettingWithFullyQualified()
        {
            var result = _reader.GetCommand(typeof(FullNamespaceTest), "Retrieve");
            NotNull(result);
            Equal("fullnamespacetest", result.CommandText);
            Equal("test.alias.fullnamespacetest", result.ConnectionAlias);
        }

        [Fact]
        public void FindsTypeInParentNamespace()
        {
            var result = _reader.GetCommand(typeof(Syrx.Commanders.NotConfiguredNamespace.ParentNamespaceTest), nameof(FindsTypeInParentNamespace));
            NotNull(result);
            Equal("parent namespace", result.CommandText);
            Equal("test.alias.parentnamespace", result.ConnectionAlias);
            
        }

        [Fact]
        public void FindsTypeInRootNamespace()
        {
            var result = _reader.GetCommand(typeof(RootNamespaceTest), "Retrieve");
            NotNull(result);
            Equal("root namespace", result.CommandText);
            Equal("test.alias.rootnamespace", result.ConnectionAlias);
            result.PrintAsJson();
        }

    }

    //internal class ParentNamespaceTest { }    
    internal class NoTypeSettingTest { }
    internal class NoCommandSettingTest { }
}

namespace Syrx.Commanders.NotConfiguredNamespace
{
    internal class ParentNamespaceTest { }
}

namespace A.Syrx.Commanders.Databases.Readers
{
    internal class NoNamespaceType
    {
    }
}

namespace Syrx
{
    internal class RootNamespaceTest { }
}

namespace Syrx.Testing.Readers
{

    internal class FullNamespaceTest { }

    // root namespace
    // parent namespace
    // full namespace
    // no namespace
    // no type setting
    // no command setting
}
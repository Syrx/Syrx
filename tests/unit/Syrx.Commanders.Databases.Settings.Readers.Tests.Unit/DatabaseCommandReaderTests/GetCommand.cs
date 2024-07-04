//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================


using Microsoft.Extensions.Options;
using Syrx.Commanders.Databases.Settings.Extensions;

namespace Syrx.Commanders.Databases.Settings.Readers.Tests.Unit.DatabaseCommandReaderTests
{
    public class GetCommand
    {
        private readonly IDatabaseCommandReader _reader;
        private const string Alias = "test-alias";
        private const string ConnectionString = "test-connection-string";
        private const string CommandText = "select 'readers.test.settings'";
        private const string Method = "Retrieve";

        public GetCommand()
        {
            // not using the extensions/builders in this test as those are meant
            // to enforce the correct set up where as these tests are meant to
            // exercise what happens when we load a non-standard config. 

#pragma warning disable CS8601 // Possible null reference assignment.
            var options = new CommanderSettings
            {
                Namespaces = [
                    new NamespaceSetting {
                        Namespace  = "Syrx",
                        Types = [
                            new TypeSetting {
                                Name = "Syrx.RootNamespaceTest",
                                Commands = new Dictionary<string, CommandSetting>{
                                    [Method] = new CommandSetting{ ConnectionAlias = "test.alias.rootnamespace", CommandText = "root namespace" }
                                }
                            },
                            new TypeSetting {
                                Name = typeof(NotConfiguredNamespace.ParentNamespaceTest).FullName,
                                Commands = new Dictionary<string, CommandSetting>{
                                    [nameof(FindsTypeInParentNamespace)] = new CommandSetting{ ConnectionAlias = "test.alias.parentnamespace", CommandText = "parent namespace" }
                                }
                            }
                            ]
                    },
                    new NamespaceSetting {
                        Namespace = typeof(GetCommand).Namespace!,
                        Types = [
                            new TypeSetting{
                                Name = $"{typeof(GetCommand).Namespace}.NoCommandSettingTest",
                                Commands = new Dictionary<string, CommandSetting>{
                                    [Method] = new CommandSetting{ ConnectionAlias = Alias, CommandText = CommandText }
                                }
                            },
                            new TypeSetting{
                                Name = $"{typeof(GetCommand).Namespace}.ParentNamespaceTest",
                                Commands = new Dictionary<string, CommandSetting>{
                                    [Method] = new CommandSetting{ ConnectionAlias = "test.alias.parentnamespace", CommandText = "parent namespace" }
                                }
                            }
                            ]
                    },
                    new NamespaceSetting{
                        Namespace = "Syrx.Testing.Readers",
                        Types = [
                            new TypeSetting {
                                Name =  "Syrx.Testing.Readers.FullNamespaceTest",
                                Commands = new Dictionary<string, CommandSetting>{
                                    [Method] = new CommandSetting { ConnectionAlias = "test.alias.fullnamespacetest", CommandText = "fullnamespacetest" }
                                }
                            }
                            ]
                    },
                    new NamespaceSetting{
                        Namespace = typeof(NoTypeSettingTest).Namespace,
                        Types = []
                    }
                    ],
                Connections = [
                    new ConnectionStringSetting { Alias = Alias, ConnectionString = ConnectionString }
                    ]
            };
#pragma warning restore CS8601 // Possible null reference assignment.

            var settings = Options.Create(options);
            
            _reader = new DatabaseCommandReader(options);

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
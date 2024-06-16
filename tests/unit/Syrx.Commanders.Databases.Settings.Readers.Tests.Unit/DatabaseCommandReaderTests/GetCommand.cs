﻿//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================


using Microsoft.Extensions.Options;
using Syrx.Commanders.Databases.Extensions.Configuration;
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

            var options = new CommanderOptions
            {
                Namespaces = [
                    new NamespaceSettingOptions {
                        Namespace  = "Syrx",
                        Types = [
                            new TypeSettingOptions {
                                Name = "Syrx.RootNamespaceTest",
                                Commands = new Dictionary<string, CommandSettingOptions>{
                                    [Method] = new CommandSettingOptions{ ConnectionAlias = "test.alias.rootnamespace", CommandText = "root namespace" }
                                }
                            },
                            new TypeSettingOptions {
                                Name = typeof(Syrx.Commanders.NotConfiguredNamespace.ParentNamespaceTest).FullName,
                                Commands = new Dictionary<string, CommandSettingOptions>{
                                    [nameof(FindsTypeInParentNamespace)] = new CommandSettingOptions{ ConnectionAlias = "test.alias.parentnamespace", CommandText = "parent namespace" }
                                }
                            }
                            ]
                    },
                    new NamespaceSettingOptions {
                        Namespace = typeof(GetCommand).Namespace!,
                        Types = [
                            new TypeSettingOptions{
                                Name = $"{typeof(GetCommand).Namespace}.NoCommandSettingTest",
                                Commands = new Dictionary<string, CommandSettingOptions>{
                                    [Method] = new CommandSettingOptions{ ConnectionAlias = Alias, CommandText = CommandText }
                                }
                            },
                            new TypeSettingOptions{
                                Name = $"{typeof(GetCommand).Namespace}.ParentNamespaceTest",
                                Commands = new Dictionary<string, CommandSettingOptions>{
                                    [Method] = new CommandSettingOptions{ ConnectionAlias = "test.alias.parentnamespace", CommandText = "parent namespace" }
                                }
                            }
                            ]
                    },
                    new NamespaceSettingOptions{
                        Namespace = "Syrx.Testing.Readers",
                        Types = [
                            new TypeSettingOptions {
                                Name =  "Syrx.Testing.Readers.FullNamespaceTest",
                                Commands = new Dictionary<string, CommandSettingOptions>{
                                    [Method] = new CommandSettingOptions { ConnectionAlias = "test.alias.fullnamespacetest", CommandText = "fullnamespacetest" }
                                }
                            }
                            ]
                    }
                    ],
                Connections = [
                    new ConnectionStringSettingOptions { Alias = Alias, ConnectionString = ConnectionString }
                    ]
            };

            var settings = Options.Create(options);
            
            _reader = new DatabaseCommandReader(options);

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
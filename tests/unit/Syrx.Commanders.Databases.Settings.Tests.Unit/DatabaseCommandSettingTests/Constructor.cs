//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Tests.Extensions;
using System.Data;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.DatabaseCommandSettingTests
{
    public class Constructor
    {
        [Theory]
        [InlineData(-1, IsolationLevel.Unspecified)]
        [InlineData(16, IsolationLevel.Chaos)]
        [InlineData(256, IsolationLevel.ReadUncommitted)]
        [InlineData(4096, IsolationLevel.ReadCommitted)]
        [InlineData(65536, IsolationLevel.RepeatableRead)]
        [InlineData(1048576, IsolationLevel.Serializable)]
        [InlineData(16777216, IsolationLevel.Snapshot)]
        [InlineData(0, IsolationLevel.Serializable)] // no value should default to serializable.
        public void AssignsIsolationLevelSuccessfully(int value, IsolationLevel expected)
        {
            const string alias = "alias";
            const string commandText = "select 1;";

            var result = new DatabaseCommandSetting(alias, commandText, isolationLevel: (IsolationLevel) value);
            var isolationLevel = (IsolationLevel) value;

            Equal(expected, result.IsolationLevel);
            Equal(value, (int) isolationLevel);
        }

        [Fact]
        public void AssignsValuesSuccessfully()
        {
            const string commandText = "select 1;";
            const int timeout = 45;
            const CommandType commandType = CommandType.StoredProcedure;
            const string alias = "alias";
            const CommandFlagSetting flag = CommandFlagSetting.Pipelined;
            const IsolationLevel isolationLevel = IsolationLevel.Chaos;
            const string split = "field";

            var result = new DatabaseCommandSetting(
                alias,
                commandText,
                commandType,
                timeout,
                split,
                flag,
                isolationLevel);

            Equal(commandText, result.CommandText);
            Equal(timeout, result.CommandTimeout);
            Equal(commandType, result.CommandType);
            Equal(alias, result.ConnectionAlias);
            Equal(flag, result.Flags);
            Equal(isolationLevel, result.IsolationLevel);
            Equal(split, result.Split);
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentNullException(string connectionAlias)
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandSetting(connectionAlias, "select 1"));
            result.ArgumentNull(nameof(connectionAlias));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceCommandTextThrowsArgumentNullException(string commandText)
        {
            var result = Throws<ArgumentNullException>(() => new DatabaseCommandSetting("alias", commandText));
            result.ArgumentNull(nameof(commandText));
        }

        [Fact]
        public void ReturnsDefaults()
        {
            const string alias = "alias";
            const string commandText = "select 1;";

            var result = new DatabaseCommandSetting(alias, commandText);

            Equal(commandText, result.CommandText);
            Equal(30, result.CommandTimeout);
            Equal(CommandType.Text, result.CommandType);
            Equal(alias, result.ConnectionAlias);
            Equal(CommandFlagSetting.None, result.Flags);
            Equal(IsolationLevel.Serializable, result.IsolationLevel);
            Equal("Id", result.Split);
        }
    }
}
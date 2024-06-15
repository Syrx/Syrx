//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.10.15 (17:59)
//  modified     : 2017.10.15 (22:43)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

using Syrx.Tests.Extensions;
using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.ConnectionStringSettingTests
{
    public class Constructor
    {
        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceAliasThrowsArgumentNullException(string alias)
        {
            var result = Throws<ArgumentNullException>(() => new ConnectionStringSetting(alias, "connectionString"));
            result.ArgumentNull(nameof(alias));
        }

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceConnectionStringThrowsArgumentNullException(string connectionString)
        {
            var result = Throws<ArgumentNullException>(() => new ConnectionStringSetting("alias", connectionString));
            result.ArgumentNull(nameof(connectionString));
        }


        [Fact]
        public void Successfully()
        {
            const string alias = "test";
            const string connectionString = "Data Source=(LocalDb)\\mssqllocaldb;Initial Catalog=Zulu.Tests;Integrated Security=true";
            var result = new ConnectionStringSetting(alias, connectionString);
            Equal(alias, result.Alias);
            Equal(connectionString, result.ConnectionString);
        }
    }
}
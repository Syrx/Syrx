﻿namespace Syrx.Commanders.Databases.Extensions.Configuration.Tests.Unit.CommandSettingOptionsBuilderTests
{
    public class UseConnectionAlias(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        // NOTE:
        // as the build method is protected internal, we 
        // can't assert the happy paths in these tests. 
        // tehy can only be tested by the extention method. 
        // this is because the purpose of the builders/extentions
        // is to enforce the happy path. 
        private CommandSettingOptionsBuilder _builder = fixture.Builder;

        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceSplitThrowsArgumentException(string alias)
        {
            var result = Throws<ArgumentNullException>(() => _builder.UseConnectionAlias(alias));
            result.ArgumentNull(nameof(alias));
        }
    }


}

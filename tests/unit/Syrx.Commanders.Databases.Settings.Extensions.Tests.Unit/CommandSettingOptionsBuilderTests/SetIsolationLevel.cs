﻿namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.CommandSettingOptionsBuilderTests
{
    public class SetIsolationLevel(CommandSettingOptionBuilderFixture fixture) : IClassFixture<CommandSettingOptionBuilderFixture>
    {
        // NOTE:
        // as the build method is protected internal, we 
        // can't assert the happy paths in these tests. 
        // tehy can only be tested by the extention method. 
        // this is because the purpose of the builders/extentions
        // is to enforce the happy path. 
        private CommandSettingOptionsBuilder _builder = fixture.Builder;

        [Fact]
        public void DefaultsToSerializable()
        {
            // the absence of an exception is kinda the point. 
            var result = _builder.SetIsolationLevel();
        }
    }
}

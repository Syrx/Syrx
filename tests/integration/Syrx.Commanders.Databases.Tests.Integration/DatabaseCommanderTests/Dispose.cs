﻿namespace Syrx.Commanders.Databases.Tests.Integration.DatabaseCommanderTests
{
    public abstract class Dispose(BaseFixture fixture) : IClassFixture<BaseFixture>
    {
        
        [Fact]
        public void Successfully()
        {
            // there's nothing to actually dispose of so... 
            var commander = fixture.GetCommander<Dispose>();
            commander.Dispose();            
        }
    }
}
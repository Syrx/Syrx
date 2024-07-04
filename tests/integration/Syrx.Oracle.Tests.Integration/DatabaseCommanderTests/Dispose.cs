namespace Syrx.Oracle.Tests.Integration.DatabaseCommanderTests
{
    //[Collection(nameof(FixtureCollection))]
    public class Dispose(BaseFixture fixture) : IClassFixture<BaseFixture>
    {

        [Fact(Skip = "Container timeouts")]
        public void Successfully()
        {
            // there's nothing to actually dispose of so... 
            var commander = fixture.GetCommander<Dispose>();
            commander.Dispose();
        }
    }
}

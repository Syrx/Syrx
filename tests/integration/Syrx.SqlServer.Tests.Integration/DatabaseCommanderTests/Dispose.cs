namespace Syrx.SqlServer.Tests.Integration.DatabaseCommanderTests
{
    [Collection(nameof(FixtureCollection))]
    public class Dispose(BaseFixture fixture)
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

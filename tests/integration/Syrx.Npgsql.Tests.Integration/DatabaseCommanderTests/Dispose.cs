using Syrx.Npgsql.Tests.Integration;

namespace Syrx.Npgsql.Tests.Integration.DatabaseCommanderTests
{
    public class Dispose(BaseFixture fixture) : IClassFixture<BaseFixture>
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

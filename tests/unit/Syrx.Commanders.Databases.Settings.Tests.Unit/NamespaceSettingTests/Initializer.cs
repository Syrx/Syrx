using static Xunit.Assert;

namespace Syrx.Commanders.Databases.Settings.Tests.Unit.NamespaceSettingTests
{
    public class Initializer : IClassFixture<SettingsFixture>
    {
        private SettingsFixture _fixture;
        public Initializer(SettingsFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void Successfully()
        {
            var types = _fixture.GetTypeSettingList();
            var result = new NamespaceSetting
            {
                Namespace = TestsConstants.NamespaceSettings.Namespace,
                Types = types
            };

            Equal(TestsConstants.NamespaceSettings.Namespace, result.Namespace);
            // i don't like that an empty list
            // is still valid but I'll live with it. 
            Single(result.Types);
            Equal(types, result.Types);
        }

        [Fact]
        public void SuccessfullyWithEmptyList()
        {
            var result = new NamespaceSetting 
            { 
                Namespace = TestsConstants.NamespaceSettings.Namespace, 
                Types = []
            };

            Equal(TestsConstants.NamespaceSettings.Namespace, result.Namespace);
            // i don't like that an empty list
            // is still valid but I'll live with it. 
            Empty(result.Types);
        }
    }
}

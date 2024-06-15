// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.11.25 (18:11)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Settings.Extensions.Tests.Unit.Options.DatabaseCommandSettingOptionsTests
{
    // todo: need to redo these tests to go up one level. 
    public class Named
    {
        private readonly DatabaseCommandSettingOptionsBuilder _options;
        public Named()
        {
            _options = new DatabaseCommandSettingOptionsBuilder();
        }


        [Theory]
        [MemberData(nameof(Generators.NullEmptyWhiteSpace), MemberType = typeof(Generators))]
        public void NullEmptyWhitespaceThrowsArgumentNullException(string method)
        {
            var result = Throws<ArgumentNullException>(() => _options.ForMethodNamed(method));
            result.ArgumentNull(nameof(method));
        }

        [Fact]
        public void Succesfully()
        {
            const string method = "test";
            var result = _options.ForMethodNamed(method);

        }
    }
}
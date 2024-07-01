// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2021.01.17 (21:54)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Tests.Integration.Models.Mutable
{
    public abstract class AbstractMutableType
    {
        public required string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Modified { get; set; }
    }
}
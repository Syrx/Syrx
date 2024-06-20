// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2021.01.17 (21:54)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

namespace Syrx.Commanders.Databases.Tests.Integration.Models.Immutable
{
    public abstract class AbstractImmutableType
    {
        public string Name { get; }
        public decimal Value { get; }
        public DateTime Modified { get; }

        protected AbstractImmutableType(string name, decimal value, DateTime modified)
        {
            Name = name;
            Value = value;
            Modified = modified;
        }
    }
}
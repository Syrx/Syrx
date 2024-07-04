//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Tests.Integration.Models.Immutable
{

    /// <summary>
    /// Set up solely to prove that the different syntax of a 
    /// primary constructor makes no difference to materialization 
    /// of the type from the commander.
    /// 
    /// We've implemented value equality for this type specifically
    /// so that we can add it to the list of types to be materialized
    /// by the Query.SingleType<> test. 
    /// </summary>
    public class PrimaryConstructorImmutableType(int id, string name, decimal value, DateTime modified) : IEquatable<PrimaryConstructorImmutableType>
    {
        public int Id { get; } = id;
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
        public decimal Value { get; } = value;
        public DateTime Modified { get; } = modified;

        public override bool Equals(object obj) => Equals(obj as PrimaryConstructorImmutableType);

        public bool Equals(PrimaryConstructorImmutableType other)
        {
            if (other is null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            var result = (this.Id == other.Id) && (this.Name == other.Name) && (this.Value == other.Value) && (this.Modified == other.Modified);
            return result;
        }

        public override int GetHashCode()
        {
            return (Id, Name, Value, Modified).GetHashCode();
        }

        public static bool operator ==(PrimaryConstructorImmutableType left, PrimaryConstructorImmutableType right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(PrimaryConstructorImmutableType left, PrimaryConstructorImmutableType right) => !(left == right);

    }

}
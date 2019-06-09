using System.Collections.Generic;

namespace Spartacus.Database.DBModels.ProtoAge
{
    public class ProtoAgeUnitFlag
    {
        public ProtoAgeUnitFlag()
        {
        }

        public ProtoAgeUnitFlag(string name, long flag)
        {
            Name = name;
            Flag = flag;
        }

        public ProtoAgeUnitFlag(ProtoAgeUnitFlag flag)
        {
            Id = flag.Id;
            Name = flag.Name;
            Flag = flag.Flag;
        }

        public static IEqualityComparer<ProtoAgeUnitFlag> Comparer { get; } = new NameFlagEqualityComparer();

         public int Id { get; set; }

        public string Name { get; set; }
        public long Flag { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Flag)}: {Flag}";
        }

        private sealed class NameFlagEqualityComparer : IEqualityComparer<ProtoAgeUnitFlag>
        {
            public bool Equals(ProtoAgeUnitFlag x, ProtoAgeUnitFlag y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name) && x.Flag == y.Flag;
            }

            public int GetHashCode(ProtoAgeUnitFlag obj)
            {
                unchecked
                {
                    return ((obj.Name != null ? obj.Name.GetHashCode() : 0) * 397) ^ obj.Flag.GetHashCode();
                }
            }
        }
    }
}
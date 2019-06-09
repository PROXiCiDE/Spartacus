using System.Collections.Generic;

namespace Spartacus.Database.DBModels.ProtoAge
{
    public class ProtoAgeUnitType
    {
        public ProtoAgeUnitType()
        {
        }

        public ProtoAgeUnitType(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public ProtoAgeUnitType(ProtoAgeUnitType type)
        {
            Id = type.Id;
            Name = type.Name;
            Type = type.Type;
        }

        public static IEqualityComparer<ProtoAgeUnitType> Comparer { get; } = new NameTypeEqualityComparer();

         public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Type)}: {Type}";
        }

        private sealed class NameTypeEqualityComparer : IEqualityComparer<ProtoAgeUnitType>
        {
            public bool Equals(ProtoAgeUnitType x, ProtoAgeUnitType y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name) && string.Equals(x.Type, y.Type);
            }

            public int GetHashCode(ProtoAgeUnitType obj)
            {
                unchecked
                {
                    return ((obj.Name != null ? obj.Name.GetHashCode() : 0) * 397) ^
                           (obj.Type != null ? obj.Type.GetHashCode() : 0);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Spartacus.Database.DBModels.ProtoAge
{
    class ProtoAgeReader
    {
        public static IEnumerable<ProtoAge> GetProtoAges(IDbConnection connection)
        {
            var sqlCommand = "SELECT protoAge.*, resourceCost.*, protoFlag.*, protoUnitType.* " +
                             "FROM ProtoAge protoAge " +
                             "INNER JOIN ProtoAgeResourceCost resourceCost on protoAge.Name = resourceCost.Name " +
                             "INNER JOIN ProtoAgeUnitFlag protoFlag on protoAge.Name = protoFlag.Name " +
                             "INNER JOIN ProtoAgeUnitType protoUnitType on protoAge.Name = protoUnitType.Name ";
            var lookup = new Dictionary<string, ProtoAge>();
            return connection
                .Query<ProtoAge, ProtoAgeResourceCost, ProtoAgeUnitFlag, ProtoAgeUnitType, ProtoAge>(sqlCommand,
                    (protoAge, resourceCost, unitFlag, unitType) =>
                    {
                        ProtoAge newAge;
                        if (!lookup.TryGetValue(protoAge.Name, out newAge))
                            lookup.Add(protoAge.Name, newAge = protoAge);
                        if (newAge.ResourceCost == null)
                            newAge.ResourceCost = new ProtoAgeResourceCost(resourceCost);
                        if (newAge.UnitTypes == null)
                            newAge.UnitTypes = new List<ProtoAgeUnitType>();
                        if (newAge.UnitFlags == null)
                            newAge.UnitFlags = new List<ProtoAgeUnitFlag>();

                        if (!newAge.UnitFlags.Contains(unitFlag, ProtoAgeUnitFlag.Comparer))
                            newAge.UnitFlags.Add(unitFlag);
                        if (!newAge.UnitTypes.Contains(unitType, ProtoAgeUnitType.Comparer))
                            newAge.UnitTypes.Add(unitType);
                        return newAge;
                    }, splitOn: "Name");
        }
    }
}

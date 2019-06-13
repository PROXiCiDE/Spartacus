using Dapper.Contrib.Extensions;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.SQLite;
using SpartacusUtils.SQLite.Annotations;

namespace Spartacus.Database.DBModels.Civilizations
{
    [Table("CivilizationStartingResource")]
    public class CivilizationStartingResource
    {
        public CivilizationStartingResource(long civId, long food, long wood, long gold, long stone)
        {
            CivId = civId;
            Food = food;
            Wood = wood;
            Gold = gold;
            Stone = stone;
        }

        public CivilizationStartingResource(CivilizationXml civilizationXml) : this()
        {
            CivId = (long)civilizationXml.Civid;
            var resource = civilizationXml.Startingresources;
            if (int.TryParse(resource.Food, out var food))
                Food = food;
            if (int.TryParse(resource.Wood, out var wood))
                Wood = wood;
            if (int.TryParse(resource.Gold, out var gold))
                Gold = gold;
            if (int.TryParse(resource.Stone, out var stone))
                Stone = stone;
        }

        public CivilizationStartingResource() : this(0, 0,0,0,0)
        {
        }

        [ExplicitKey]
        public long CivId { get; set; }
        public long Food { get; set; }
        public long Wood { get; set; }
        public long Gold { get; set; }
        public long Stone { get; set; }

        public override string ToString()
        {
            return $"{nameof(CivId)}: {CivId}, {nameof(Food)}: {Food}, {nameof(Wood)}: {Wood}, {nameof(Gold)}: {Gold}, {nameof(Stone)}: {Stone}";
        }
    }
}
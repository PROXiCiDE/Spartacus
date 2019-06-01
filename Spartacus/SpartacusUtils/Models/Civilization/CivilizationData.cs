using System;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using SpartacusUtils.Models.Civilization;

namespace SpartacusUtils.Models
{
    public class CivilizationData
    {
        public CivilizationData(CivilizationEnum civId, string name, ECivilizationEnum civMatchingId, string displayName, CivilizationShieldData shieldData, StartingResourcesData startingResourcesData)
        {
            CivId = civId;
            Name = name;
            CivMatchingId = civMatchingId;
            DisplayName = displayName;
            ShieldData = shieldData;
            StartingResourcesData = startingResourcesData;
        }

        public override string ToString()
        {
            return Name;
        }

        public CivilizationEnum CivId { get; private set; }
        public string Name { get; private set; }
        public ECivilizationEnum CivMatchingId { get; private set; }
        public string DisplayName { get; private set; }
        public CivilizationShieldData ShieldData { get; private set; }
        public StartingResourcesData StartingResourcesData { get; private set; }
    }
}

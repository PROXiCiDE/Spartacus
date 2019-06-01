using System;
using ProjectCeleste.GameFiles.XMLParser.Enum;

namespace SpartacusUtils.Models
{
    public class CivilizationData
    {
        public string Name;
        public CivilizationEnum CivId { get; }

        public CivilizationData(CivilizationEnum civId, string name)
        {
            Name = name;

            if (!Enum.IsDefined(typeof(CivilizationEnum), civId))
                CivId = CivilizationEnum.Greek;
            else
                CivId = civId;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

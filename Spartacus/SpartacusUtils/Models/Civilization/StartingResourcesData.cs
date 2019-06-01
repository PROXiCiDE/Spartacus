using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCeleste.GameFiles.XMLParser;

namespace SpartacusUtils.Models.Civilization
{
    public class StartingResourcesData
    {
        public int Food { get; }
        public int Wood { get; }
        public int Gold { get; }
        public int Stone { get; }

        public StartingResourcesData(int food, int wood, int gold, int stone)
        {
            Food = food;
            Wood = wood;
            Gold = gold;
            Stone = stone;
        }

        public StartingResourcesData(CivilizationXmlStartingresources startingresources)
        {
            if (int.TryParse(startingresources.Food, out var food))
                Food = food;
            if (int.TryParse(startingresources.Wood, out var wood))
                Wood = wood;
            if (int.TryParse(startingresources.Gold, out var gold))
                Gold = gold;
            if (int.TryParse(startingresources.Stone, out var stone))
                Stone = stone;
        }
    }
}

using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;

namespace Spartacus.Database.DBModels.Civilizations
{
    public static class CivilizationHelpers
    {
        /// <summary>
        /// Gets a civilization shield
        /// </summary>
        /// <param name="civilization"></param>
        /// <param name="shieldTextureType"></param>
        /// <param name="age">Range 1..4</param>
        /// <returns></returns>
        public static string GetShieldTexture(this Civilization civilization, ShieldTextureType shieldTextureType, CivilizationAgeTech ageTech = CivilizationAgeTech.Age1)
        {
            if (shieldTextureType == ShieldTextureType.Enabled)
            {
                var shield = $"{civilization.ShieldTexture}_ua.ddt";
                if (ageTech > CivilizationAgeTech.Age1 && ageTech <= CivilizationAgeTech.Age4)
                    shield = $"{civilization.ShieldTexture}{(int)ageTech}_ua.ddt";
                return shield;
            }
            else
                return $"{civilization.ShieldGreyTexture}.ddt";
        }
    }
}
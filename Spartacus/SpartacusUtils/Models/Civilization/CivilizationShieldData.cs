using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartacusUtils.Models.Civilization
{
    public class CivilizationShieldData
    {
        public string ShieldTexture { get; }
        public string ShieldDisabledTexture { get; }

        //TODO: Build Shield data for different level's 2,3,4
        public CivilizationShieldData(string shieldTexture, string shieldDisabledTexture)
        {
            ShieldTexture = shieldTexture;
            ShieldDisabledTexture = shieldDisabledTexture;
        }
    }
}

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

        public CivilizationShieldData(string shieldTexture, string shieldDisabledTexture)
        {
            ShieldTexture = shieldTexture;
            ShieldDisabledTexture = shieldDisabledTexture;
        }
    }
}

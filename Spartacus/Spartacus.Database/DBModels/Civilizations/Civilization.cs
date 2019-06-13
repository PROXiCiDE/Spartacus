using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.SQLite;

namespace Spartacus.Database.DBModels.Civilizations
{
    public class Civilization
    {
        public Civilization(long civilizationId, long displayNameId, long rolloverNameId, string shieldTexture, string shieldGreyTexture, string townCenter, string age0, string age1, string age2, string age3, long storeHouseTechId, CivilizationStartingResource startingResource)
        {
            CivilizationId = civilizationId;
            DisplayNameId = displayNameId;
            RolloverNameId = rolloverNameId;
            ShieldTexture = shieldTexture;
            ShieldGreyTexture = shieldGreyTexture;
            TownCenter = townCenter;
            Age0 = age0;
            Age1 = age1;
            Age2 = age2;
            Age3 = age3;
            StoreHouseTechId = storeHouseTechId;
            StartingResource = startingResource;
        }

        public Civilization(long civilizationId, long displayNameId, long rolloverNameId, string shieldTexture, string shieldGreyTexture, string townCenter, string age0, string age1, string age2, string age3, long storeHouseTechId)
        {
            CivilizationId = civilizationId;
            DisplayNameId = displayNameId;
            RolloverNameId = rolloverNameId;
            ShieldTexture = shieldTexture;
            ShieldGreyTexture = shieldGreyTexture;
            TownCenter = townCenter;
            Age0 = age0;
            Age1 = age1;
            Age2 = age2;
            Age3 = age3;
            StoreHouseTechId = storeHouseTechId;
        }

        public Civilization(CivilizationXml civilizationXml) : this(0, 0,0,null,null,null,null,null,null,null,0)
        {
            //Little hack to prevent repetitive typing
            string GetAgeTech(string ageName)
            {
                return civilizationXml.Agetech?.FirstOrDefault(x =>
                               string.Equals(x.Age, ageName, StringComparison.OrdinalIgnoreCase))
                           ?.Tech ?? "";
            }

            Age0 = GetAgeTech("age0");
            Age1 = GetAgeTech("age1");
            Age2 = GetAgeTech("age2");
            Age3 = GetAgeTech("age3");

            if (int.TryParse(civilizationXml.Displaynameid, out var displayNameId))
                DisplayNameId = displayNameId;
            if (int.TryParse(civilizationXml.Rollovernameid, out var rollOverId))
                RolloverNameId = rollOverId;
            if (int.TryParse(civilizationXml.Ui.Storehousetechid, out var storageTechId))
                StoreHouseTechId = storageTechId;

            CivilizationId = (long) civilizationXml.Civid;
            ShieldGreyTexture = civilizationXml.Ui?.Shieldtexture;
            ShieldTexture = civilizationXml.Ui?.Shieldtexture;
            TownCenter = civilizationXml.Towncenter;

            StartingResource = new CivilizationStartingResource(civilizationXml);
        }

        [Key]
        public long CivilizationId { get; set; }
        public long DisplayNameId { get; set; }
        public long RolloverNameId { get; set; }
        public string ShieldTexture { get; set; }
        public string ShieldGreyTexture { get; set; }
        public string TownCenter { get; set; }
        public string Age0 { get; set; }
        public string Age1 { get; set; }
        public string Age2 { get; set; }
        public string Age3 { get; set; }
        public long StoreHouseTechId { get; set; }

        [Write(false)]
        public CivilizationStartingResource StartingResource { get; set; }

        public override string ToString()
        {
            return $"{nameof(CivilizationId)}: {CivilizationId}, {nameof(DisplayNameId)}: {DisplayNameId}, {nameof(RolloverNameId)}: {RolloverNameId}, {nameof(ShieldTexture)}: {ShieldTexture}, {nameof(ShieldGreyTexture)}: {ShieldGreyTexture}, {nameof(TownCenter)}: {TownCenter}, {nameof(Age0)}: {Age0}, {nameof(Age1)}: {Age1}, {nameof(Age2)}: {Age2}, {nameof(Age3)}: {Age3}, {nameof(StoreHouseTechId)}: {StoreHouseTechId}, {nameof(StartingResource)}: {StartingResource}";
        }
    }
}
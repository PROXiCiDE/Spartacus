using System;

namespace Spartacus.Database.DBModels.Civilizations
{
    public class CivilizationsModel
    {
        private long civid;
        private int displayNameId;
        private int rollOverId;
        private string shieldtexture;
        private string shieldgreytexture;
        private string age0;
        private string age1;
        private string age2;
        private string age3;
        private int storageTechId;

        public CivilizationsModel(long civid, int displayNameId, int rollOverId, string shieldtexture, string shieldgreytexture, string age0, string age1, string age2, string age3, int storageTechId)
        {
            this.civid = civid;
            this.displayNameId = displayNameId;
            this.rollOverId = rollOverId;
            this.shieldtexture = shieldtexture;
            this.shieldgreytexture = shieldgreytexture;
            this.age0 = age0;
            this.age1 = age1;
            this.age2 = age2;
            this.age3 = age3;
            this.storageTechId = storageTechId;
        }
    }
}
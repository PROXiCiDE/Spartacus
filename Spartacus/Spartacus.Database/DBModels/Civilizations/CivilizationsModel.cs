namespace Spartacus.Database.DBModels.Civilizations
{
    public class CivilizationsModel
    {
        public CivilizationsModel(long civilizationId, long displayNameId, long rolloverNameId, string shieldTexture,
            string shieldGreyTexture, string age0, string age1, string age2, string age3, long storehouseTechId)
        {
            CivilizationId = civilizationId;
            DisplayNameId = displayNameId;
            RolloverNameId = rolloverNameId;
            ShieldTexture = shieldTexture;
            ShieldGreyTexture = shieldGreyTexture;
            Age0 = age0;
            Age1 = age1;
            Age2 = age2;
            Age3 = age3;
            StorehouseTechId = storehouseTechId;
        }

        public long CivilizationId { get; set; }
        public long DisplayNameId { get; set; }
        public long RolloverNameId { get; set; }
        public string ShieldTexture { get; set; }
        public string ShieldGreyTexture { get; set; }
        public string Age0 { get; set; }
        public string Age1 { get; set; }
        public string Age2 { get; set; }
        public string Age3 { get; set; }
        public long StorehouseTechId { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(CivilizationId)}: {CivilizationId}, {nameof(DisplayNameId)}: {DisplayNameId}, {nameof(RolloverNameId)}: {RolloverNameId}, {nameof(ShieldTexture)}: {ShieldTexture}, {nameof(ShieldGreyTexture)}: {ShieldGreyTexture}, {nameof(Age0)}: {Age0}, {nameof(Age1)}: {Age1}, {nameof(Age2)}: {Age2}, {nameof(Age3)}: {Age3}, {nameof(StorehouseTechId)}: {StorehouseTechId}";
        }
    }
}
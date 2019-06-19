namespace Spartacus.Database.DBModels.ProtoAgeUnit
{
    public class ProtoUnitProtoActionDamageBonus
    {
        public long Id { get; set; }
        public long UnitId { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
    }
}
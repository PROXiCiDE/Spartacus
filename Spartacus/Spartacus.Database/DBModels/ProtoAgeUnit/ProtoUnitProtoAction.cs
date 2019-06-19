namespace Spartacus.Database.DBModels.ProtoAgeUnit
{
    public class ProtoUnitProtoAction
    {
        public long Id { get; set; }
        public long UnitId { get; set; }
        public long Name { get; set; }
        public double Damage { get; set; }
        public double Rof { get; set; }
        public long DamageType { get; set; }
        public long Projectile { get; set; }
        public double MaxHeight { get; set; }
        public double MinRange { get; set; }
        public double DamageArea { get; set; }
        public long DamageFlags { get; set; }
        public double TrackRating { get; set; }
        public long Active { get; set; }
        public double DamageCap { get; set; }
        public long HandLogic { get; set; }
        public long Poison { get; set; }
        public double DamageOverTimeDuration { get; set; }
        public double DamageOverTimeRate { get; set; }
    }
}
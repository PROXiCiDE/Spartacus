namespace Spartacus.Database.DBModels.TechTree
{
    public class TechTreeEffects
    {
        public long Id { get; set; }
        public string TechNameId { get; set; }
        public string Type { get; set; }
        public long Amount { get; set; }
        public long Scaling { get; set; }
        public string SubType { get; set; }
        public string Relativity { get; set; }
        public string Target { get; set; }
        public long AllActions { get; set; }
        public string Action { get; set; }
        public string UnitType { get; set; }
        public string Proto { get; set; }
        public string Culture { get; set; }
        public string Resource { get; set; }
        public string Component { get; set; }
        public string Status { get; set; }
        public string NewName { get; set; }
        public string DamageType { get; set; }
    }
}
namespace Spartacus.Database.DBModels.TechTree
{
    public class TechTreeResource
    {
        public long Id { get; set; }
        public long TechNameId { get; set; }
        public long ResourceTypeEnum { get; set; }
        public long Value { get; set; }
    }
}
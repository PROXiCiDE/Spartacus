using System;

namespace Spartacus.Database.DBModels.TechTree
{
    public class TechTree
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long DisplayNameId { get; set; }
        public long ResearchPoints { get; set; }
        public string Status { get; set; }
        public string Icon { get; set; }
        public long ContentPack { get; set; }
    }
}
namespace Spartacus.Database.DBModels.Advisors
{
    public class AdvisorsModel
    {
        public AdvisorsModel(string name, long civid, long age, string icon, long rarirty, long rollverTextId,
            long displayDescriptionId, long displayNameId, long minLevel, long itemLevel, string techId)
        {
            Name = name;
            Civid = civid;
            Age = age;
            Icon = icon;
            Rarirty = rarirty;
            RollverTextId = rollverTextId;
            DisplayDescriptionId = displayDescriptionId;
            DisplayNameId = displayNameId;
            MinLevel = minLevel;
            ItemLevel = itemLevel;
            TechId = techId;
        }

        public string Name { get; set; }
        public long Civid { get; set; }
        public long Age { get; set; }
        public string Icon { get; set; }
        public long Rarirty { get; set; }
        public long RollverTextId { get; set; }
        public long DisplayDescriptionId { get; set; }
        public long DisplayNameId { get; set; }
        public long MinLevel { get; set; }
        public long ItemLevel { get; set; }
        public string TechId { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Name)}: {Name}, {nameof(Civid)}: {Civid}, {nameof(Age)}: {Age}, {nameof(Icon)}: {Icon}, {nameof(Rarirty)}: {Rarirty}, {nameof(RollverTextId)}: {RollverTextId}, {nameof(DisplayDescriptionId)}: {DisplayDescriptionId}, {nameof(DisplayNameId)}: {DisplayNameId}, {nameof(MinLevel)}: {MinLevel}, {nameof(ItemLevel)}: {ItemLevel}, {nameof(TechId)}: {TechId}";
        }
    }
}
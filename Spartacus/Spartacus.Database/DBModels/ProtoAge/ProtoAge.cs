using System.Collections.Generic;

namespace Spartacus.Database.DBModels.ProtoAge
{
    public class ProtoAge
    {
        public ProtoAge()
        {
        }

        public ProtoAge(string name, long displayNameId, long editorNameId, long populationCount,
            string animFile, string icon, string portraitIcon, long rolloverTextId, long shortRolloverTextId,
            long initialHitpoints, long maxHitpoints, long los, long allowedAge)
        {
            Name = name;
            DisplayNameId = displayNameId;
            EditorNameId = editorNameId;
            PopulationCount = populationCount;
            AnimFile = animFile;
            Icon = icon;
            PortraitIcon = portraitIcon;
            RolloverTextId = rolloverTextId;
            ShortRolloverTextId = shortRolloverTextId;
            InitialHitpoints = initialHitpoints;
            MaxHitpoints = maxHitpoints;
            LOS = los;
            AllowedAge = allowedAge;
        }

        public ProtoAge(ProtoAge protoAge)
        {
            Id = protoAge.Id;

            Name = protoAge.Name;
            DisplayNameId = protoAge.DisplayNameId;
            EditorNameId = protoAge.EditorNameId;
            PopulationCount = protoAge.PopulationCount;
            AnimFile = protoAge.AnimFile;
            Icon = protoAge.Icon;
            PortraitIcon = protoAge.PortraitIcon;
            RolloverTextId = protoAge.RolloverTextId;
            ShortRolloverTextId = protoAge.ShortRolloverTextId;
            InitialHitpoints = protoAge.InitialHitpoints;
            MaxHitpoints = protoAge.MaxHitpoints;
            LOS = protoAge.LOS;
            AllowedAge = protoAge.AllowedAge;
            ResourceCost = protoAge.ResourceCost;
            UnitTypes = protoAge.UnitTypes;
            UnitFlags = protoAge.UnitFlags;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public long DisplayNameId { get; set; }
        public long EditorNameId { get; set; }
        public long PopulationCount { get; set; }
        public string AnimFile { get; set; }
        public string Icon { get; set; }
        public string PortraitIcon { get; set; }
        public long RolloverTextId { get; set; }
        public long ShortRolloverTextId { get; set; }
        public long InitialHitpoints { get; set; }
        public long MaxHitpoints { get; set; }
        public long LOS { get; set; }
        public long AllowedAge { get; set; }

       public ProtoAgeResourceCost ResourceCost { get; set; }

       public List<ProtoAgeUnitType> UnitTypes { get; set; }

        public List<ProtoAgeUnitFlag> UnitFlags { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(DisplayNameId)}: {DisplayNameId}, {nameof(EditorNameId)}: {EditorNameId}, {nameof(PopulationCount)}: {PopulationCount}, {nameof(AnimFile)}: {AnimFile}, {nameof(Icon)}: {Icon}, {nameof(PortraitIcon)}: {PortraitIcon}, {nameof(RolloverTextId)}: {RolloverTextId}, {nameof(ShortRolloverTextId)}: {ShortRolloverTextId}, {nameof(InitialHitpoints)}: {InitialHitpoints}, {nameof(MaxHitpoints)}: {MaxHitpoints}, {nameof(LOS)}: {LOS}, {nameof(AllowedAge)}: {AllowedAge}";
        }
    }
}
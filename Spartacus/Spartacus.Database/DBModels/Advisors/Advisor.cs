using System;
using Dapper.Contrib.Extensions;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;

namespace Spartacus.Database.DBModels.Advisors
{
    [Table("Advisor")]
    public class Advisor
    {
        public Advisor()
        {
            
        }
        public Advisor(EconAdvisorXml advisor)
        {
            Name = advisor.Name;
            GroupId = advisor.GroupId;
            Age = advisor.Age;
            Rarity = advisor.Age;
            IconTextureCoords = advisor.IconTextureCoords;
            Icon = advisor.Icon;
            DisplayNameId = advisor.DisplayNameId;
            DisplayDescriptionId = advisor.DisplayDescriptionId;
            ShortDescriptionId = advisor.ShortDescriptionId;
            MinLevel = advisor.Minlevel;
            ItemLevel = advisor.ItemLevel;
            CivMatchingType = advisor.Civilization;
            OfferType = advisor.OfferType;
            TechId = advisor.Techs?.Tech;
            IsSpecialBorder = advisor.IsSpecialBorder;
            Sellable = advisor.Sellable;
            Destroyable = advisor.Destroyable;

            SellCostQuantity = advisor.SellCostOverride?.CapitalResource != null ? advisor.SellCostOverride?.CapitalResource?.Quantity : 0;
            SellCostType = advisor.SellCostOverride?.CapitalResource != null ? advisor.SellCostOverride?.CapitalResource?.Type : CapitalResourceTypeEnum.None;
        }

        public Advisor(string name, long groupId, long age, long rarity, string iconTextureCoords, string icon, long displayNameId, long displayDescriptionId, int shortDescriptionId, long minLevel, long itemLevel, ECivilizationEnum civMatchingType, EOfferTypeEnum offerType, string techId, bool sellable, bool destroyable, bool isSpecialBorder, double? sellCostQuantity, CapitalResourceTypeEnum? sellCostType)
        {
            Name = name;
            GroupId = groupId;
            Age = age;
            Rarity = rarity;
            IconTextureCoords = iconTextureCoords;
            Icon = icon;
            DisplayNameId = displayNameId;
            DisplayDescriptionId = displayDescriptionId;
            ShortDescriptionId = shortDescriptionId;
            MinLevel = minLevel;
            ItemLevel = itemLevel;
            CivMatchingType = civMatchingType;
            OfferType = offerType;
            TechId = techId;
            Sellable = sellable;
            Destroyable = destroyable;
            IsSpecialBorder = isSpecialBorder;
            SellCostQuantity = sellCostQuantity;
            SellCostType = sellCostType;
        }

        [ExplicitKey]
        public string Name { get; set; }
        public long GroupId { get; set; }
        public long Age { get; set; }
        public long Rarity { get; set; }
        public string IconTextureCoords { get; set; }
        public string Icon { get; set; }
        public long DisplayNameId { get; set; }
        public long DisplayDescriptionId { get; set; }
        public int ShortDescriptionId { get; set; }
        public long MinLevel { get; set; }
        public long ItemLevel { get; set; }
        public ECivilizationEnum CivMatchingType { get; set; }
        public EOfferTypeEnum OfferType { get; set; }
        public string TechId { get; set; }
        public bool Sellable { get; set; }
        public bool Destroyable { get; set; }
        public bool IsSpecialBorder { get; set; }
        public double? SellCostQuantity { get; set; }
        public CapitalResourceTypeEnum? SellCostType { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(GroupId)}: {GroupId}, {nameof(Age)}: {Age}, {nameof(Rarity)}: {Rarity}, {nameof(IconTextureCoords)}: {IconTextureCoords}, {nameof(Icon)}: {Icon}, {nameof(DisplayNameId)}: {DisplayNameId}, {nameof(DisplayDescriptionId)}: {DisplayDescriptionId}, {nameof(MinLevel)}: {MinLevel}, {nameof(ItemLevel)}: {ItemLevel}, {nameof(CivMatchingType)}: {CivMatchingType}, {nameof(OfferType)}: {OfferType}, {nameof(TechId)}: {TechId}, {nameof(SellCostQuantity)}: {SellCostQuantity}, {nameof(SellCostType)}: {SellCostType}";
        }
    }
}
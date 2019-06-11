namespace Spartacus.Database.DBModels.Advisors
{
    public class AdvisorsModel
    {
        private string name;
        private long civilization;
        private int age;
        private string icon;
        private long rarity;
        private int rolloverTextId;
        private int displayDescriptionId;
        private int displayNameId;
        private int minlevel;
        private int itemLevel;
        private string tech;

        public AdvisorsModel(string name, long civilization, int age, string icon, long rarity, int rolloverTextId, int displayDescriptionId, int displayNameId, int minlevel, int itemLevel, string tech)
        {
            this.name = name;
            this.civilization = civilization;
            this.age = age;
            this.icon = icon;
            this.rarity = rarity;
            this.rolloverTextId = rolloverTextId;
            this.displayDescriptionId = displayDescriptionId;
            this.displayNameId = displayNameId;
            this.minlevel = minlevel;
            this.itemLevel = itemLevel;
            this.tech = tech;
        }
    }
}
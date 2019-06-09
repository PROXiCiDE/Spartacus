namespace Spartacus.Database.DBModels.ProtoAge
{
    public class ProtoAgeResourceCost
    {
        public ProtoAgeResourceCost()
        {
        }

        public ProtoAgeResourceCost(string name, long food, long wood, long gold, long stone)
        {
            Name = name;
            Food = food;
            Wood = wood;
            Gold = gold;
            Stone = stone;
        }

        public ProtoAgeResourceCost(ProtoAgeResourceCost resourceCost)
        {
            Id = resourceCost.Id;
            Name = resourceCost.Name;
            Food = resourceCost.Food;
            Wood = resourceCost.Wood;
            Gold = resourceCost.Gold;
            Stone = resourceCost.Stone;
        }

       public int Id { get; set; }

        public string Name { get; set; }
        public long Food { get; set; }
        public long Wood { get; set; }
        public long Gold { get; set; }
        public long Stone { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Food)}: {Food}, {nameof(Wood)}: {Wood}, {nameof(Gold)}: {Gold}, {nameof(Stone)}: {Stone}";
        }
    }
}
using System.Collections.Generic;

namespace Common
{
    public abstract class ItemData
    {
        public ItemId Id;
        public string Name;
        public string Description;
        public List<StatModifier> StatModifiers;

        protected ItemData(ItemId id, string name, string description, List<StatModifier> statModifiers)
        {
            Id = id;
            Name = name;
            StatModifiers = statModifiers;
            Description = description;
        }

        public Item Create(int stack = 1)
        {
            return new Item(Id, Name, Description, new List<StatModifier>(StatModifiers), stack);
        }
    }
}
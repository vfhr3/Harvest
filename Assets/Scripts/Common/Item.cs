using System.Collections.Generic;
using Unity.Mathematics;

namespace Common
{
    public class Item
    {
        public string Name;
        public string Description;
        public ItemId Id { get; private set; }
        public readonly List<StatModifier> Modifiers;
        public int Stack { get; private set; }
        
        public Item(ItemId id, string name, string description, List<StatModifier> modifiers, int stack = 1)
        {
            Id = id;
            Name = name;
            Description = description;
            Modifiers = modifiers;
            Stack = stack;
        }

        public void AddStack() => Stack++;
        public void RemoveStack() => Stack = math.max(0, Stack - 1);

    }

    public enum ItemId
    {
        Boots,
        Hat,
        Fertilizer,
        CarrotJuice,
        Soil
    }
}
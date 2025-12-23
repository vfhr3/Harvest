using System.Collections.Generic;

namespace Common
{
    public static class ItemRegistry
    {
        private static readonly Dictionary<ItemId, ItemData> Items = new();

        public static void Register(ItemData data)
        {
            if (!Items.TryAdd(data.Id, data))
                throw new System.Exception($"Item {data.Id} already registered");
        }

        public static Item Create(ItemId id, int stack = 1)
        {
            return Items[id].Create();
        }
    }
}
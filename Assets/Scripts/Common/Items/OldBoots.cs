using System.Collections.Generic;
using Player;

namespace Common.Items
{
    public class OldBoots : ItemData
    {
        public OldBoots() : base(ItemId.Boots, "Old Boots", "Someone lost it.", new List<StatModifier>()
        {
            new StatModifier(StatType.Speed, 1, ModifierType.Additive),
            new StatModifier(StatType.Armor, 1, ModifierType.Additive)
        })
        {
            
        }
    }
}
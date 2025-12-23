using System.Collections.Generic;
using Player;

namespace Common.Items
{
    public class Fertilizer : ItemData
    {
        public Fertilizer() : base(ItemId.Fertilizer, "Fertiziler", "You feel larger", new List<StatModifier>()
        {
            new(StatType.AreaScale, 0.5f, ModifierType.Additive)
        })
        {
        }
    }
}
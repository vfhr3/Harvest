using System.Collections.Generic;
using Player;

namespace Common.Items
{
    public class FarmerHat : ItemData
    {
        public FarmerHat() 
            : 
            base(
                ItemId.Hat,
                "Farmer's Hat",
                "Old hat",
                new List<StatModifier>() 
                {
                    new StatModifier(StatType.Regeneration, 0.5f, ModifierType.Additive)
                }
                )
        {
        }
    }
}
using System.Collections.Generic;
using Player;

namespace Common.Items
{
    public class CarrotJuice : ItemData
    {
        public CarrotJuice() : 
            base(
                ItemId.CarrotJuice,
                "Carrot Juice",
                "Just a fresh juice",
                new List<StatModifier>()
                {
                    new StatModifier(StatType.PickUpRange, 0.5f, ModifierType.Additive)
                }
                
            )
        {
        }
    }
}
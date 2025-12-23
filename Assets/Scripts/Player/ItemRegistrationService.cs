using Common;
using Common.Items;
using UnityEngine;

namespace Player
{
    public class ItemRegistrationService : MonoBehaviour
    {
        private void Awake()
        {
            ItemRegistry.Register(new Fertilizer());
            ItemRegistry.Register(new CarrotJuice());
            ItemRegistry.Register(new FarmerHat());
            ItemRegistry.Register(new OldBoots());
        }
    }
}
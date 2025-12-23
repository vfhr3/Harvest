using Common;
using UnityEngine;

namespace Player
{
    public class StatComponent : MonoBehaviour
    {
        [SerializeField] private InventoryComponent inventory;

        private StatContainer _statContainer;
        
        public void Initialize(Stat[] baseStats)
        {
            _statContainer = new StatContainer(baseStats);
            
            
            if (inventory == null)
                inventory = GetComponent<InventoryComponent>();
            
            inventory.OnModifierAdded += Add;
            inventory.OnModifierRemoved += Remove;

        }
        
        public float Get(StatType type)
        {
            return _statContainer.Get(type);
        }
        
        public void Add(StatModifier statModifier)
        {
            _statContainer.AddModifier(statModifier);
        }

        public void Remove(StatModifier statModifier)
        {
            _statContainer.RemoveModifier(statModifier);
        }

        private void OnDisable()
        {
            inventory.OnModifierAdded -= Add;
            inventory.OnModifierRemoved -= Remove;
        }
    }
}
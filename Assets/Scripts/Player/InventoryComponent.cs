using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class InventoryComponent : MonoBehaviour
    {
        private List<Item> _items = new();
        [SerializeField] private StatComponent stats;

        public event Action<StatModifier> OnModifierAdded;
        public event Action<StatModifier> OnModifierRemoved;

        public IReadOnlyList<Item> Items => _items;

        private void Awake()
        {
            stats = GetComponent<StatComponent>();
        }

        public void AddItem(Item item)
        {
            var existing = _items.Find(i => i.Id == item.Id);
        
            if (existing != null)
                existing.AddStack();
            else
                _items.Add(item);
        
            foreach (var modifier in item.Modifiers)
                OnModifierAdded?.Invoke(modifier);
            
        }

        public void RemoveItem(Item item)
        {
            var existing = _items.Find(i => i.Id == item.Id);
            if (existing == null) 
                throw new ArgumentException("There is no such item in inventory");
        
            foreach (var modifier in existing.Modifiers)
                OnModifierRemoved?.Invoke(modifier);
        
            existing.RemoveStack();
            if (existing.Stack <= 0)
                _items.Remove(existing);
        }

        public IEnumerable<StatModifier> GetAllModifiers()
        {
            foreach (var item in _items) 
                for (int i = 0; i < item.Stack; i++) 
                    foreach (var modifier in item.Modifiers) 
                        yield return modifier;
        }

        [ContextMenu("Create Boots")]
        public void Mock_Add_Item()
        {
            AddItem(ItemRegistry.Create(ItemId.Boots));
        }
    }
}                   
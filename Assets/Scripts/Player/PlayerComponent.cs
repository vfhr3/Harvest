using System;
using Common;
using UnityEngine;

namespace Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeField] private InventoryComponent _inventory;
        [SerializeField] private StatComponent _stats;

        private void Start()
        {
            _inventory = GetComponent<InventoryComponent>();
            _stats = GetComponent<StatComponent>();
            
            _stats.Initialize(new Stat[]
            {
                new Stat(StatType.Speed, 1)
            });
        }
    }
}
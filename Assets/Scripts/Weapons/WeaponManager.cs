using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private int maxWeapons = 6;
        
        private List<BaseWeapon> _activeWeapons = new();
        private StatComponent _stats;

        private void Awake()
        {
            _stats = GetComponent<StatComponent>();
        }

        private void Update()
        {
            foreach (var weapon in _activeWeapons)
            {
                weapon.Tick(Time.deltaTime);
            }
        }

        public bool AddWeapon(WeaponData weaponData)
        {
            if (_activeWeapons.Count >= maxWeapons)
                return false;

            var existing = _activeWeapons.Find(w => w.Data.Id == weaponData.Id);
            if (existing != null)
            {
                existing.LevelUp();
                return true;
            }

            var weapon = weaponData.CreateWeapon(playerTransform, _stats);
            _activeWeapons.Add(weapon);
            return true;
        }

        public void RemoveWeapon(WeaponId id)
        {
            _activeWeapons.RemoveAll(w => w.Data.Id == id);
        }

        public BaseWeapon GetWeapon(WeaponId id)
        {
            return _activeWeapons.Find(w => w.Data.Id == id);
        }
    }
}
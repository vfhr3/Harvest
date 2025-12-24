using System.Collections.Generic;
using Common;
using Player;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Орбитальное оружие (как bible, garlic в VS)
    /// </summary>
    public class OrbitalWeapon : BaseWeapon
    {
        private OrbitalWeaponData _orbitalData;
        private List<GameObject> _orbitals = new();

        public OrbitalWeapon(OrbitalWeaponData data, Transform playerTransform, StatComponent stats) 
            : base(data, playerTransform, stats)
        {
            _orbitalData = data;
            SpawnOrbitals();
        }

        protected override void Attack()
        {
            // Орбитальное оружие не имеет активной атаки, оно всегда активно
        }

        public override void Tick(float deltaTime)
        {
            UpdateOrbitals(deltaTime);
        }

        private void SpawnOrbitals()
        {
            int count = _orbitalData.GetOrbitalCount(Level);
            
            for (int i = 0; i < count; i++)
            {
                var orbital = Object.Instantiate(_orbitalData.OrbitalPrefab, 
                    PlayerTransform.position, 
                    Quaternion.identity, 
                    PlayerTransform);

                float size = _orbitalData.GetSize(Level);
                size *= (1f + Stats.Get(StatType.AreaScale));

                var comp = orbital.GetComponent<OrbitalObject>();
                comp.Initialize(GetDamage(), size, _orbitalData.OrbitRadius, i, count);
                
                _orbitals.Add(orbital);
            }
        }

        private void UpdateOrbitals(float deltaTime)
        {
            foreach (var orbital in _orbitals)
            {
                if (orbital != null)
                {
                    var comp = orbital.GetComponent<OrbitalObject>();
                    comp.UpdateOrbit(deltaTime, _orbitalData.RotationSpeed);
                }
            }
        }

        protected override void OnLevelUp()
        {
            // При апгрейде пересоздаем орбитали
            foreach (var orbital in _orbitals)
            {
                if (orbital != null)
                    Object.Destroy(orbital);
            }
            _orbitals.Clear();
            SpawnOrbitals();
        }
    }
}
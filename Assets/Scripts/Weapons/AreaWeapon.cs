using Common;
using Player;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Оружие области действия (как святая вода в VS)
    /// </summary>
    public class AreaWeapon : BaseWeapon
    {
        private AreaWeaponData _areaData;

        public AreaWeapon(AreaWeaponData data, Transform playerTransform, StatComponent stats) 
            : base(data, playerTransform, stats)
        {
            _areaData = data;
        }

        protected override void Attack()
        {
            var target = TargetSelector.FindNearestEnemy(PlayerTransform.position);
            Vector3 spawnPosition = target != null 
                ? target.position 
                : PlayerTransform.position + (Vector3)Random.insideUnitCircle * 3f;

            SpawnArea(spawnPosition);
        }

        private void SpawnArea(Vector3 position)
        {
            var area = Object.Instantiate(_areaData.AreaPrefab, position, Quaternion.identity);

            float duration = _areaData.GetDuration(Level);
            duration *= (1f + Stats.Get(StatType.SkillDuration));

            float size = _areaData.GetSize(Level);
            size *= (1f + Stats.Get(StatType.AreaScale));

            var comp = area.GetComponent<DamageArea>();
            comp.Initialize(GetDamage(), duration, size);
        }
    }
}
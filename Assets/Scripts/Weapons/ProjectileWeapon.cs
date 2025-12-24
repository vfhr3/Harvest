using Common;
using Player;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Оружие, стреляющее проектайлами (как томаты, чеснок в VS)
    /// </summary>
    public class ProjectileWeapon : BaseWeapon
    {
        private ProjectileWeaponData _projectileData;

        public ProjectileWeapon(ProjectileWeaponData data, Transform playerTransform, StatComponent stats) 
            : base(data, playerTransform, stats)
        {
            _projectileData = data;
        }

        protected override void Attack()
        {
            int projectileCount = _projectileData.GetProjectileCount(Level);
            projectileCount += (int)Stats.Get(StatType.ProjectileCount);

            var targets = TargetSelector.FindNearestEnemies(PlayerTransform.position, projectileCount);

            if (targets.Count == 0)
            {
                // Если нет целей, стреляем в случайном направлении
                SpawnProjectile(Random.insideUnitCircle.normalized);
            }
            else
            {
                foreach (var target in targets)
                {
                    Vector2 direction = (target.position - PlayerTransform.position).normalized;
                    SpawnProjectile(direction);
                }
            }
        }

        private void SpawnProjectile(Vector2 direction)
        {
            var projectile = Object.Instantiate(_projectileData.ProjectilePrefab, 
                PlayerTransform.position, 
                Quaternion.identity);

            float speed = _projectileData.GetProjectileSpeed(Level);
            speed += Stats.Get(StatType.ProjectileSpeed);

            float duration = _projectileData.GetDuration(Level);
            duration *= (1f + Stats.Get(StatType.SkillDuration));

            float size = _projectileData.GetSize(Level);
            size *= (1f + Stats.Get(StatType.AreaScale));

            var comp = projectile.GetComponent<Projectile>();
            comp.Initialize(GetDamage(), speed, duration, direction, size, (int)Stats.Get(StatType.PierceCount));
        }
    }
}
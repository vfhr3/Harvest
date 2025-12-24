using Player;
using UnityEngine;

namespace Weapons
{
    public class ProjectileWeaponData : WeaponData
    {
        public GameObject ProjectilePrefab;
        [SerializeField] private float[] ProjectileSpeed;
        [SerializeField] private float[] Duration;
        [SerializeField] private int[] ProjectileCount;

        public override BaseWeapon CreateWeapon(Transform playerTransform, StatComponent stats)
        {
            return new ProjectileWeapon(this, playerTransform, stats);
        }

        public float GetProjectileSpeed(int level) => ProjectileSpeed[Mathf.Min(level - 1, ProjectileSpeed.Length - 1)];
        public float GetDuration(int level) => Duration[Mathf.Min(level - 1, Duration.Length - 1)];
        public int GetProjectileCount(int level) => ProjectileCount[Mathf.Min(level - 1, ProjectileCount.Length - 1)];
    }
}
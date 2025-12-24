using Player;
using UnityEngine;

namespace Weapons
{
    public class AreaWeaponData : WeaponData
    {
        public GameObject AreaPrefab;
        [SerializeField] private float[] Duration;

        public override BaseWeapon CreateWeapon(Transform playerTransform, StatComponent stats)
        {
            return new AreaWeapon(this, playerTransform, stats);
        }

        public float GetDuration(int level) => Duration[Mathf.Min(level - 1, Duration.Length - 1)];
    }
}
using Player;
using UnityEngine;

namespace Weapons
{
    public class OrbitalWeaponData : WeaponData
    {
        public GameObject OrbitalPrefab;
        public float OrbitRadius = 2f;
        public float RotationSpeed = 90f;
        [SerializeField] private int[] OrbitalCount;

        public override BaseWeapon CreateWeapon(Transform playerTransform, StatComponent stats)
        {
            return new OrbitalWeapon(this, playerTransform, stats);
        }

        public int GetOrbitalCount(int level) => OrbitalCount[Mathf.Min(level - 1, OrbitalCount.Length - 1)];
    }
}
using Player;
using UnityEngine;

namespace Weapons
{
    public abstract class WeaponData : ScriptableObject
    {
        public WeaponId Id;
        public string Name;
        public string Description;
        public Sprite Icon;
        public int MaxLevel = 8;
        
        [SerializeField] protected WeaponLevelData[] LevelData;

        public abstract BaseWeapon CreateWeapon(Transform playerTransform, StatComponent stats);

        public float GetDamage(int level) => LevelData[Mathf.Min(level - 1, LevelData.Length - 1)].Damage;
        public float GetCooldown(int level) => LevelData[Mathf.Min(level - 1, LevelData.Length - 1)].Cooldown;
        public float GetSize(int level) => LevelData[Mathf.Min(level - 1, LevelData.Length - 1)].Size;
    }
}
using UnityEngine;

namespace Weapons
{
    public abstract class BaseWeapon
    {
        private WeaponConfig _config;
        
        public BaseWeapon(WeaponConfig config)
        {
            _config = config;
        }
        
        public abstract void Attack(Transform position);
    }

    public class WeaponConfig
    {
        public float Size;
        public float Damage;
        public float Cooldown;
    }
}
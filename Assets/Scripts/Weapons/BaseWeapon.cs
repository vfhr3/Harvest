using Common;
using Player;
using UnityEngine;

namespace Weapons
{
    public abstract class BaseWeapon
    {
        public WeaponData Data { get; private set; }
        public int Level { get; private set; } = 1;
        
        protected Transform PlayerTransform;
        protected StatComponent Stats;
        protected float CooldownTimer;

        protected BaseWeapon(WeaponData data, Transform playerTransform, StatComponent stats)
        {
            Data = data;
            PlayerTransform = playerTransform;
            Stats = stats;
            CooldownTimer = 0f;
        }

        public virtual void Tick(float deltaTime)
        {
            CooldownTimer -= deltaTime;
            
            if (CooldownTimer <= 0f)
            {
                Attack();
                CooldownTimer = GetCooldown();
            }
        }

        protected abstract void Attack();

        protected float GetCooldown()
        {
            float baseCooldown = Data.GetCooldown(Level);
            float cooldownRecovery = Stats.Get(StatType.CooldownRecovery);
            return baseCooldown / (1f + cooldownRecovery);
        }

        protected float GetDamage()
        {
            float baseDamage = Data.GetDamage(Level);
            float statDamage = Stats.Get(StatType.Damage);
            return baseDamage + statDamage;
        }

        public void LevelUp()
        {
            if (Level < Data.MaxLevel)
            {
                Level++;
                OnLevelUp();
            }
        }

        protected virtual void OnLevelUp() { }
    }
}
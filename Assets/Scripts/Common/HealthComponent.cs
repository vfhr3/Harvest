using System;
using Player;
using UnityEngine;

namespace Common
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        private StatComponent _stats;
        
        public bool IsDead;
        public int Max { get; private set; }
        public int Current { get; private set; }
        public event Action<int> OnHealthChanged;
        public event Action<int> OnDamageTaken;
        public event Action OnDeath;

        
        public void Initialize(StatComponent stats)
        {
            _stats = stats;

            Max = (int)_stats.Get(StatType.Health);
            Current = Max;
        }
        
        public void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            
            Current = Mathf.Max(0, Current - damage);
            OnDamageTaken?.Invoke(damage);
            OnHealthChanged?.Invoke(Current);
            
            if (Current == 0) Die();
            
        }

        public void ApplyHeal(int amount)
        {
            if (amount <= 0) return;
            
            var oldCurrent = Current;
            Current = Mathf.Min(Current + amount, Max);
            if (Current != oldCurrent) 
                OnHealthChanged?.Invoke(Current);
        }

        public void Die()
        {
            if (IsDead) return;
            IsDead = true;
            OnDeath?.Invoke();
        }
    }

    public interface IDamageable
    {
        int Current { get; }
        int Max { get; }
        event Action<int> OnHealthChanged;
        event Action<int> OnDamageTaken;
        event Action OnDeath;
        void ApplyDamage(int damage);
        void ApplyHeal(int amount);
        void Die();
    }
}
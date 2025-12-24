using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Область урона - наносит периодический урон всем врагам в радиусе
    /// </summary>
    public class DamageArea : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D damageCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private float _damage;
        private float _tickRate = 0.5f; // урон каждые 0.5 сек
        private float _tickTimer;
        private List<IDamageable> _enemiesInRange = new();

        public void Initialize(float damage, float duration, float size)
        {
            _damage = damage;
            transform.localScale = Vector3.one * size;
            
            Destroy(gameObject, duration);
        }

        private void Update()
        {
            _tickTimer += Time.deltaTime;
            
            if (_tickTimer >= _tickRate)
            {
                _tickTimer = 0f;
                DamageEnemies();
            }
        }

        private void DamageEnemies()
        {
            foreach (var enemy in _enemiesInRange)
            {
                if (enemy != null)
                {
                    enemy.ApplyDamage((int)_damage);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null && !_enemiesInRange.Contains(damageable))
                {
                    _enemiesInRange.Add(damageable);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    _enemiesInRange.Remove(damageable);
                }
            }
        }
    }
}
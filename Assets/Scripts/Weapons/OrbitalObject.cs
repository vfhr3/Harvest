using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Орбитальный объект - вращается вокруг игрока и наносит урон при контакте
    /// </summary>
    public class OrbitalObject : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D damageCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private float _damage;
        private float _orbitRadius;
        private float _currentAngle;
        private float _angleOffset;
        private HashSet<GameObject> _hitEnemies = new();
        private float _hitCooldown = 0.2f; // кулдаун между ударами одного врага
        private Dictionary<GameObject, float> _enemyCooldowns = new();

        public void Initialize(float damage, float size, float orbitRadius, int index, int totalCount)
        {
            _damage = damage;
            _orbitRadius = orbitRadius;
            _angleOffset = (360f / totalCount) * index;
            _currentAngle = _angleOffset;

            transform.localScale = Vector3.one * size;
        }

        public void UpdateOrbit(float deltaTime, float rotationSpeed)
        {
            _currentAngle += rotationSpeed * deltaTime;
            
            float radians = _currentAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(
                Mathf.Cos(radians) * _orbitRadius,
                Mathf.Sin(radians) * _orbitRadius,
                0f
            );

            transform.localPosition = offset;

            // Обновляем кулдауны врагов
            var toRemove = new List<GameObject>();
            foreach (var kvp in _enemyCooldowns)
            {
                _enemyCooldowns[kvp.Key] -= deltaTime;
                if (_enemyCooldowns[kvp.Key] <= 0f)
                {
                    toRemove.Add(kvp.Key);
                }
            }
            foreach (var enemy in toRemove)
            {
                _enemyCooldowns.Remove(enemy);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (!_enemyCooldowns.ContainsKey(other.gameObject))
                {
                    var damageable = other.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.ApplyDamage((int)_damage);
                        _enemyCooldowns[other.gameObject] = _hitCooldown;
                    }
                }
            }
        }
    }
}
using UnityEngine;
using System.Collections.Generic;
using Common;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CircleCollider2D damageCollider;

        private float _damage;
        private float _speed;
        private Vector2 _direction;
        private int _pierceCount;
        private int _currentPierceCount;
        private HashSet<GameObject> _hitEnemies = new();

        public void Initialize(float damage, float speed, float duration, Vector2 direction, float size, int pierceCount)
        {
            _damage = damage;
            _speed = speed;
            _direction = direction.normalized;
            _pierceCount = pierceCount;
            _currentPierceCount = 0;

            transform.localScale = Vector3.one * size;
            
            Destroy(gameObject, duration);
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + _direction * (_speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && !_hitEnemies.Contains(other.gameObject))
            {
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.ApplyDamage((int)_damage);
                    _hitEnemies.Add(other.gameObject);
                    _currentPierceCount++;

                    if (_currentPierceCount > _pierceCount)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    // ========== DAMAGE AREA COMPONENT ==========

    // ========== ORBITAL OBJECT COMPONENT ==========

    // ========== BEAM COMPONENT (бонус - лазерный луч) ==========

    // ========== BOOMERANG COMPONENT (бонус) ==========
}
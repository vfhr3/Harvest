using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Бумеранг - летит в направлении врага и возвращается
    /// </summary>
    public class Boomerang : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        
        private Transform _player;
        private float _damage;
        private float _speed;
        private float _maxDistance;
        private Vector2 _direction;
        private bool _returning;
        private HashSet<GameObject> _hitEnemies = new();

        public void Initialize(Transform player, float damage, float speed, float maxDistance)
        {
            _player = player;
            _damage = damage;
            _speed = speed;
            _maxDistance = maxDistance;
            
            var target = TargetSelector.FindNearestEnemy(player.position);
            _direction = target != null 
                ? ((Vector2)(target.position - player.position)).normalized
                : Random.insideUnitCircle.normalized;
        }

        private void FixedUpdate()
        {
            if (!_returning)
            {
                rb.MovePosition(rb.position + _direction * (_speed * Time.fixedDeltaTime));
                
                if (Vector2.Distance(_player.position, transform.position) >= _maxDistance)
                {
                    _returning = true;
                    _hitEnemies.Clear(); // Можем снова бить тех же врагов при возвращении
                }
            }
            else
            {
                Vector2 directionToPlayer = ((Vector2)_player.position - rb.position).normalized;
                rb.MovePosition(rb.position + directionToPlayer * (_speed * Time.fixedDeltaTime));
                
                if (Vector2.Distance(_player.position, transform.position) < 0.5f)
                {
                    Destroy(gameObject);
                }
            }

            // Вращаем для визуала
            transform.Rotate(0f, 0f, 720f * Time.fixedDeltaTime);
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
                }
            }
        }
    }
}
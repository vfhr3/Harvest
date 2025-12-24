using Common;
using UnityEngine;

namespace Weapons
{
    /// <summary>
    /// Лазерный луч - постоянный луч урона в направлении ближайшего врага
    /// </summary>
    public class BeamWeapon : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private float maxRange = 10f;

        private float _damage;
        private float _tickRate = 0.1f;
        private float _tickTimer;
        private Transform _player;

        public void Initialize(Transform player, float damage, float width)
        {
            _player = player;
            _damage = damage;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }

        private void Update()
        {
            var target = TargetSelector.FindNearestEnemy(_player.position, maxRange);
            
            if (target != null)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, _player.position);
                lineRenderer.SetPosition(1, target.position);

                _tickTimer += Time.deltaTime;
                if (_tickTimer >= _tickRate)
                {
                    _tickTimer = 0f;
                    var damageable = target.GetComponent<IDamageable>();
                    damageable?.ApplyDamage((int)_damage);
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }
}
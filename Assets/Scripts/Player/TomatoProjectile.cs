using UnityEngine;

namespace Player
{
    public class TomatoProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float duration;
        
        [SerializeField] private Rigidbody2D rb;

        private Vector2 _direction;
        
        public void Initialize(float moveSpeed, float lifeDuration, Vector2 direction)
        {
            speed = moveSpeed;
            duration = lifeDuration;
            _direction = direction;
        }
        private void Start()
        {
            Destroy(gameObject, duration);
        }

        private void Update()
        {
            var newPosition = transform.position + (Vector3)_direction * (Time.fixedDeltaTime * speed);
            rb.MovePosition(newPosition);
        }

    }
}

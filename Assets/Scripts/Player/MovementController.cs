using Common;
using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private StatComponent stats;
        [SerializeField] public Vector2 Direction { get; private set; }
        [SerializeField] public Vector2 LastDirection { get; private set; }
        private void Awake()
        {
            rigidbody ??= GetComponent<Rigidbody2D>();
            stats ??= GetComponent<StatComponent>();
        }

        public void UpdateDirection(Vector2 direction)
        {
            Direction = direction;
            if (direction != Vector2.zero) 
                LastDirection = direction;
            Debug.Log(LastDirection);
        }
        
        private void Move()
        {
            float speed = stats.Get(StatType.Speed);
            
            var newPosition = rigidbody.position + Direction * (Time.fixedDeltaTime * speed); 
            rigidbody.MovePosition(newPosition);
        }

        private void FixedUpdate()
        {
            if (Direction != Vector2.zero)
            {
                Move();
            }
        }
    }
}

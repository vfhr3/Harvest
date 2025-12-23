using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInput : IInputSource
    {
        private readonly InputActionReference _playerInput;

        public PlayerInput(InputActionReference playerInput)
        {
            _playerInput = playerInput;
        }

        public Vector2 GetDirection()
        {
            var direction = new Vector2(); 
            return direction;
        }
    }
}
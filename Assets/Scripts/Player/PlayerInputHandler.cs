using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHandler: MonoBehaviour
    {
        private PlayerControls _input;
        private MovementController _movementController;
        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _input = new PlayerControls();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Gameplay.Movement.performed += OnMovePerformed;
            _input.Gameplay.Movement.canceled += OnMoveCanceled;
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            _movementController.UpdateDirection(Vector2.zero);
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            _movementController.UpdateDirection(direction.normalized);
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Gameplay.Movement.performed -= OnMovePerformed;
            _input.Gameplay.Movement.canceled -= OnMoveCanceled;
        }
    }
}
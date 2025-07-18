using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZombieWar.Character
{
    public class CharacterJoystickInput : MonoBehaviour
    {
        public Action<Vector2> OnMoveInputChanged;

        private CharacterInput characterInput;

        private void Awake() {
            characterInput = new CharacterInput();
        }

        private void OnEnable()
        {
            characterInput.Enable();
            characterInput.Player.Move.performed += OnMove;
            characterInput.Player.Move.canceled += OnMove;
        }

        private void OnDisable()
        {
            characterInput.Player.Move.performed -= OnMove;
            characterInput.Player.Move.canceled -= OnMove;
            characterInput.Disable();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            OnMoveInputChanged?.Invoke(context.ReadValue<Vector2>());
        }
    }
}

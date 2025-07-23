using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class CharacterJoystickInput : MonoBehaviour
    {
        public Action<Vector2> OnMoveInputChanged;
        public Action<Vector2> OnShootByLookInputChanged;

        private CharacterInput characterInput;

        private void Awake() {
            characterInput = new CharacterInput();
        }

        private void OnEnable()
        {
            characterInput.Enable();

            characterInput.Player.Move.performed += OnMove;
            characterInput.Player.Move.canceled += OnMove;

            characterInput.Player.Look.performed += OnLook;
            characterInput.Player.Look.canceled += OnLook;
        }

        private void OnDisable()
        {
            characterInput.Player.Move.performed -= OnMove;
            characterInput.Player.Move.canceled -= OnMove;

            characterInput.Player.Look.performed -= OnLook;
            characterInput.Player.Look.canceled -= OnLook;

            characterInput.Disable();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            OnShootByLookInputChanged?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            OnMoveInputChanged?.Invoke(context.ReadValue<Vector2>());
        }
    }
}

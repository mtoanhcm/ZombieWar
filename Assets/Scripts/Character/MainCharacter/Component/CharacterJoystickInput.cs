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
        public Action<CharacterWeaponEquipSlot> OnPickWeaponSlot;
        public Action OnUseAbility;

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

            characterInput.Player.PickWeaponSlot2.performed += OnPickWeaponSlot2;
            characterInput.Player.PickWeaponSlot4.performed += OnPickWeaponSlot4;
            characterInput.Player.UseAbility.performed += UseAbility;
        }

        private void OnDisable()
        {
            characterInput.Player.Move.performed -= OnMove;
            characterInput.Player.Move.canceled -= OnMove;

            characterInput.Player.Look.performed -= OnLook;
            characterInput.Player.Look.canceled -= OnLook;

            characterInput.Player.PickWeaponSlot2.performed -= OnPickWeaponSlot2;
            characterInput.Player.PickWeaponSlot4.performed -= OnPickWeaponSlot4;

            characterInput.Player.UseAbility.performed -= UseAbility;

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

        private void OnPickWeaponSlot2(InputAction.CallbackContext context)
        {
            OnPickWeaponSlot?.Invoke(CharacterWeaponEquipSlot.Slot2);
        }

        private void OnPickWeaponSlot4(InputAction.CallbackContext context)
        {
            OnPickWeaponSlot?.Invoke(CharacterWeaponEquipSlot.Slot4);
        }

        private void UseAbility(InputAction.CallbackContext context)
        {
            OnUseAbility?.Invoke();
        }
    }
}

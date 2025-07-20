using System;
using UnityEngine;
using UnityEngine.UIElements;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacter : CharacterBase<MainCharacterData>
    {
        private CharacterHealth characterHealth;
        private CharacterJoystickInput characterJoystickInput;
        private CharacterMoveByDirection characterMoveByDirection;
        private MainCharacterAnimation mainCharAnimation;
        private CharacterModelView characterModelView;
        private CharacterInventory weaponInventory;

        public override void Spawn(MainCharacterData characterData)
        {
            base.Spawn(characterData);

            InitCharacterHealth();
            InitCharacterJoystickInput();
            InitCharacterMovement();
            InitCharacterModelView();
            InitCharacterAnimation();
            InitCharacterInventory();
        }

        private void InitCharacterInventory()
        {
            //if(weaponInventory == null && !TryGetComponent(out weaponInventory))
            //{
            //    weaponInventory = gameObject.AddComponent<CharacterInventory>();
            //}

            //weaponInventory.Init(characterData.InitialWeapons);
        }

        private void InitCharacterModelView()
        {
            if (characterModelView == null && !TryGetComponent(out characterModelView))
            {
                characterModelView = gameObject.AddComponent<CharacterModelView>();
                characterModelView.AutoInitCharacterPart();
            }
        }

        private void InitCharacterAnimation()
        {
            if(mainCharAnimation == null && !TryGetComponent(out mainCharAnimation))
            {
                mainCharAnimation = gameObject.AddComponent<MainCharacterAnimation>();
            }

            characterJoystickInput.OnMoveInputChanged -= mainCharAnimation.OnPlayMovementAnimByInput;
            characterJoystickInput.OnMoveInputChanged += mainCharAnimation.OnPlayMovementAnimByInput;
        }

        private void InitCharacterJoystickInput()
        {
            if(characterJoystickInput == null && !TryGetComponent(out characterJoystickInput))
            {
                characterJoystickInput = gameObject.AddComponent<CharacterJoystickInput>();
            }
        }

        private void InitCharacterHealth()
        {
            if (characterHealth == null && !TryGetComponent(out characterHealth))
            {
                characterHealth = gameObject.AddComponent<CharacterHealth>();
            }

            characterHealth.Init(characterData.MaxHealth);
        }

        private void InitCharacterMovement()
        {
            if (characterMoveByDirection == null && !TryGetComponent(out characterMoveByDirection))
            {
                characterMoveByDirection = gameObject.AddComponent<CharacterMoveByDirection>();
            }

            characterJoystickInput.OnMoveInputChanged -= characterMoveByDirection.UpdateMoveDirection;
            characterJoystickInput.OnMoveInputChanged += characterMoveByDirection.UpdateMoveDirection;

            characterMoveByDirection.Init(characterData.MovementSpeed, characterData.RotateSpeed);
        }
    
    }
}

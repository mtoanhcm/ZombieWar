using System;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacter : CharacterBase<MainCharacterData>
    {
        private CharacterHealth characterHealth;
        private CharacterJoystickInput characterJoystickInput;
        private CharacterMoveByDirection characterMoveByDirection;
        private MainCharacterAnimation mainCharAnimation;

        public override void Spawn(MainCharacterData characterData)
        {
            base.Spawn(characterData);

            InitCharacterHealth();
            InitCharacterJoystickInput();
            InitCharacterMovement();
            InitCharacterAnimation();
        }

        private void InitCharacterAnimation()
        {
            if(mainCharAnimation == null)
            {
                mainCharAnimation = gameObject.AddComponent<MainCharacterAnimation>();
                characterJoystickInput.OnMoveInputChanged += mainCharAnimation.OnPlayMovementAnimByInput;
            }
        }

        private void InitCharacterJoystickInput()
        {
            if(characterJoystickInput == null)
            {
                characterJoystickInput = gameObject.AddComponent<CharacterJoystickInput>();
            }
        }

        private void InitCharacterHealth()
        {
            if (characterHealth == null)
            {
                characterHealth = gameObject.AddComponent<CharacterHealth>();
            }

            characterHealth.Init(characterData.MaxHealth);
        }

        private void InitCharacterMovement()
        {
            if (characterMoveByDirection == null)
            {
                characterMoveByDirection = gameObject.AddComponent<CharacterMoveByDirection>();
                characterJoystickInput.OnMoveInputChanged += characterMoveByDirection.UpdateMoveDirection;
            }

            characterMoveByDirection.Init(characterData.MovementSpeed, characterData.RotateSpeed);
        }
    
    }
}

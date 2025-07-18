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

        public override void Spawn(MainCharacterData characterData)
        {
            base.Spawn(characterData);

            InitCharacterHealth();
            InitCharacterJoystickInput();
            InitCharacterMovement();
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
            }

            characterJoystickInput.OnMoveInputChanged = characterMoveByDirection.UpdateMoveDirection;
            characterMoveByDirection.Init(characterData.MovementSpeed);
        }
    
    }
}

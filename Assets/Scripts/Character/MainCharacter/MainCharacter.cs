using System;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacter : CharacterBase
    {
        [SerializeField]
        private WeaponBaseConfig[] startWeapons;

        private MainCharacterData mainCharacterData;

        private CharacterHealth characterHealth;
        private CharacterJoystickInput characterJoystickInput;
        private CharacterMoveByDirection characterMoveByDirection;
        private MainCharacterAnimation mainCharAnimation;
        private CharacterModelView characterModelView;
        private CharacterWeaponHolder characterWeaponHolder;
        private CharacterCombat mainCharacterCombat;
        private CharacterAbilityHolder characterAbilityHolder;

        public override void Spawn<T>(T characterData)
        {
            base.Spawn(characterData);

            mainCharacterData = characterData as MainCharacterData;

            InitMainCharacterComponent();

            ChangeWeapon(CharacterWeaponEquipSlot.Slot2);
        }

        private void ChangeWeapon(CharacterWeaponEquipSlot slot) {
            if(characterWeaponHolder.GetWeapon(slot, out var weapon))
            {
                if (mainCharacterCombat.CurrentWeapon != null) {
                    characterWeaponHolder.PutBackWeapon(mainCharacterCombat.CurrentWeapon);
                }

                mainCharacterCombat.SetWeapon(weapon);
                weapon.SnapToHandGrabPoint(characterModelView.WeaponGrabSocket);
            }
        }

        #region Initialization Components

        private void InitMainCharacterComponent() {
            InitCharacterHealth();
            InitCharacterJoystickInput();
            InitCharacterModelView();

            InitCharacterWeaponHolder();
            InitCharacterAbilityHolder();
            InitCharacterCombat();

            InitCharacterMovement();
            InitCharacterAnimation();
        }

        private void InitCharacterAbilityHolder() {
            //Hardcode
            characterAbilityHolder = gameObject.GetComponentInChildren<CharacterAbilityHolder>();
        }

        private void InitCharacterCombat() { 
            if(mainCharacterCombat == null && !TryGetComponent(out mainCharacterCombat))
            {
                mainCharacterCombat = gameObject.AddComponent<CharacterCombat>();
            }

            characterJoystickInput.OnShootByLookInputChanged -= mainCharacterCombat.AttackByAuto;
            characterJoystickInput.OnShootByLookInputChanged += mainCharacterCombat.AttackByAuto;
        }

        private void InitCharacterWeaponHolder()
        {
            if (characterWeaponHolder == null && !TryGetComponent(out characterWeaponHolder)) { 
                characterWeaponHolder = gameObject.AddComponent<CharacterWeaponHolder>();
                characterWeaponHolder.Init();
            }

            foreach (var config in startWeapons) { 
                WeaponSpawnerManager.Instance.SpawnHitScanWeapon(config, out IWeapon weapon);
                characterWeaponHolder.AddWeapon(weapon);
            }
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

            mainCharacterCombat.OnFocusDirectionChanged -= mainCharAnimation.OnCombatChanged;
            mainCharacterCombat.OnFocusDirectionChanged += mainCharAnimation.OnCombatChanged;

            mainCharacterCombat.OnCurrentWeaponChanged -= mainCharAnimation.OnChangeAnimByWeapon;
            mainCharacterCombat.OnCurrentWeaponChanged += mainCharAnimation.OnChangeAnimByWeapon;
        }

        private void InitCharacterJoystickInput()
        {
            if(characterJoystickInput == null && !TryGetComponent(out characterJoystickInput))
            {
                characterJoystickInput = gameObject.AddComponent<CharacterJoystickInput>();
            }

            characterJoystickInput.OnPickWeaponSlot -= ChangeWeapon;
            characterJoystickInput.OnPickWeaponSlot += ChangeWeapon;

            Action useAbility = () => characterAbilityHolder.ActiveAbility(transform.position);
            characterJoystickInput.OnUseAbility -= useAbility;
            characterJoystickInput.OnUseAbility += useAbility;
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

            characterJoystickInput.OnMoveInputChanged -= characterMoveByDirection.Move;
            characterJoystickInput.OnMoveInputChanged += characterMoveByDirection.Move;

            mainCharacterCombat.OnFocusDirectionChanged -= characterMoveByDirection.SetTargetNeedToFocus;
            mainCharacterCombat.OnFocusDirectionChanged += characterMoveByDirection.SetTargetNeedToFocus;

            characterMoveByDirection.Init(characterData.MovementSpeed, characterData.RotateSpeed);
        }
        #endregion

    }
}

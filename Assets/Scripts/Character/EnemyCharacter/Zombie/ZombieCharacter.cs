using Codice.CM.Client.Differences.Graphic;
using System;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class ZombieCharacter : CharacterBase
    {
        private SimpleEnemyData enemyData;

        private CharacterMoveByDirection moveByDirection;
        private CharacterHealth characterHealth;
        private CharacterCombat characterCombat;
        private CharacterSimpleAnimation characterSimpleAnimation;
        private CharacterWeaponHolder weaponHolder;

        public override void Spawn<T>(T characterData)
        {
            base.Spawn(characterData);

            enemyData = characterData as SimpleEnemyData;

            InitCharacterComponent();
            SetWeapon();
        }

        private void SetWeapon() {
            if (!weaponHolder.GetWeapon(CharacterWeaponEquipSlot.Slot1, out var weapon))
            {
                DebugCustom.LogWarning("No weapon found in weapon holder for ZombieCharacter. Please ensure a weapon is assigned.");
                return;
            }

            characterCombat.SetWeapon(weapon);
        }

        private void InitCharacterComponent() {
            InitMovementComponent();
            InitHealthComponent();
            InitWeaponHolderComponent();
            InitCharacterCombat();
            InitCharacterAnimation();
        }

        private void InitWeaponHolderComponent()
        {
            if(weaponHolder == null || TryGetComponent(out weaponHolder) == false) {
                weaponHolder = gameObject.AddComponent<CharacterWeaponHolder>();
                weaponHolder.Init();
            }

            WeaponSpawnerManager.Instance.SpawnMeleeWeapon(enemyData.WeaponConfig, out IWeapon weapon, null);
            weaponHolder.AddWeapon(weapon);
        }

        private void InitCharacterAnimation()
        {
            if(characterSimpleAnimation == null || TryGetComponent(out characterSimpleAnimation) == false) {
                characterSimpleAnimation = gameObject.AddComponent<CharacterSimpleAnimation>();
            }

            characterCombat.OnAttackStateChanged -= characterSimpleAnimation.OnAttackStateChange;
            characterCombat.OnAttackStateChanged += characterSimpleAnimation.OnAttackStateChange;

            moveByDirection.OnSpeedInputChanged -= characterSimpleAnimation.OnSpeedChange;
            moveByDirection.OnSpeedInputChanged += characterSimpleAnimation.OnSpeedChange;
        }

        private void InitCharacterCombat()
        {
            if(characterCombat == null || TryGetComponent(out characterCombat) == false) {
                characterCombat = gameObject.AddComponent<CharacterCombat>();
            }
        }

        private void InitHealthComponent()
        {
            if(characterHealth == null || TryGetComponent(out characterHealth) == false) {
                characterHealth = gameObject.AddComponent<CharacterHealth>();
            }

            characterHealth.Init(characterData.MaxHealth);
        }

        private void InitMovementComponent() {
            if(moveByDirection == null || TryGetComponent(out moveByDirection) == false) {
                moveByDirection = gameObject.AddComponent<CharacterMoveByDirection>();
            }

            moveByDirection.Init(characterData.MovementSpeed, characterData.RotateSpeed);
        }
    }
}

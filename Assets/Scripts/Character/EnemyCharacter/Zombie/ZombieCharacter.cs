using System;
using UnityEngine;
using ZombieWar.Core;
using ZombieWar.AI;

namespace ZombieWar.Character
{
    public class ZombieCharacter : CharacterBase
    {
        [SerializeField]
        private Transform weaponGrabPoint;

        private AIBrainBase aiBrain;

        private SimpleEnemyData enemyData;
        private CharacterMoveByDirection moveByDirection;
        private CharacterHealth characterHealth;
        private CharacterCombat characterCombat;
        private CharacterSimpleAnimation characterSimpleAnimation;
        private CharacterWeaponHolder weaponHolder;

        public override void Spawn<T>(T characterData)
        {
            base.Spawn(characterData);

            SetLayer(LayerMask.NameToLayer(ObjectLayer.EnemyLayerName));
            enemyData = characterData as SimpleEnemyData;

            InitCharacterComponent();
            SetWeapon();

            if (aiBrain == null || !TryGetComponent(out aiBrain)) {
                aiBrain = gameObject.AddComponent<AINoobBrain>();
            }

            aiBrain.Init(moveByDirection, characterCombat, characterHealth, null);
            aiBrain.SetTarget(PlayerManager.Instance.MainCharacter);
        }

        private void SetLayer(int layerIndex) {
            gameObject.layer = layerIndex;
        }

        private void SetWeapon() {
            if (!weaponHolder.GetWeapon(CharacterWeaponEquipSlot.Slot5, out var weapon))
            {
                DebugCustom.LogWarning("No weapon found in weapon holder for ZombieCharacter. Please ensure a weapon is assigned.");
                return;
            }

            characterCombat.SetWeapon(weapon);
            weapon.SnapToHandGrabPoint(weaponGrabPoint);
        }

        private void InitCharacterComponent() {
            InitHealthComponent();
            InitMovementComponent();

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

            //Check Create weapon default for enemy
            if (weaponHolder.HasNoWeapon()) {
                WeaponSpawnerManager.Instance.SpawnMeleeWeapon(enemyData.WeaponConfig, out IWeapon weapon);
                weaponHolder.AddWeapon(weapon);
            }
        }

        private void InitCharacterAnimation()
        {
            if(characterSimpleAnimation == null || TryGetComponent(out characterSimpleAnimation) == false) {
                characterSimpleAnimation = gameObject.AddComponent<CharacterSimpleAnimation>();
            }

            characterSimpleAnimation.Init();

            characterCombat.OnAttackStateChanged -= characterSimpleAnimation.OnAttackStateChange;
            characterCombat.OnAttackStateChanged += characterSimpleAnimation.OnAttackStateChange;

            moveByDirection.OnSpeedInputChanged -= characterSimpleAnimation.OnSpeedChange;
            moveByDirection.OnSpeedInputChanged += characterSimpleAnimation.OnSpeedChange;

            characterHealth.OnDeath -= characterSimpleAnimation.OnDeathStateEnter;
            characterHealth.OnDeath += characterSimpleAnimation.OnDeathStateEnter;

            characterSimpleAnimation.OnHitByAttackAnimation -= characterCombat.AttackByCommand;
            characterSimpleAnimation.OnHitByAttackAnimation += characterCombat.AttackByCommand;
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

            characterHealth.OnDeath -= OnCharacterDeath;
            characterHealth.OnDeath += OnCharacterDeath;
        }

        private void InitMovementComponent() {
            if(moveByDirection == null || TryGetComponent(out moveByDirection) == false) {
                moveByDirection = gameObject.AddComponent<CharacterMoveByDirection>();
            }

            moveByDirection.Init(characterData.MovementSpeed, characterData.RotateSpeed);
        }

        private void OnCharacterDeath() {
            SetLayer(LayerMask.NameToLayer(ObjectLayer.EnemyDeathLayerName));
            OnDeath?.Invoke(this);
        }
    }
}

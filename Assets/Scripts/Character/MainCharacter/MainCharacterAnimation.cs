using System;
using UnityEngine;
using ZombieWar.Config;
using Sirenix.OdinInspector;
using ZombieWar.Core;
using System.Collections.Generic;

namespace ZombieWar.Character
{
    public class MainCharacterAnimation : MonoBehaviour
    {
        [SerializeField]
        private MainCharacterAnimationConfig config;

        private Animator animator;
        private int velocityXHash;
        private int velocityZHash;
        private int isInCombatHash;
        private bool isInCombat;

        private RuntimeAnimatorController defaultAnimatorController;

        private Dictionary<WeaponType, RuntimeAnimatorController> weaponAnimationControllersDic;
        

        private void Awake()
        {
            animator = transform.GetComponentInChildren<Animator>();
            velocityXHash = Animator.StringToHash("VelocityX");
            velocityZHash = Animator.StringToHash("VelocityZ");
            isInCombatHash = Animator.StringToHash("IsInCombat");

            defaultAnimatorController = animator.runtimeAnimatorController;

            weaponAnimationControllersDic = new Dictionary<WeaponType, RuntimeAnimatorController>
            {
                { WeaponType.Melee, defaultAnimatorController },
                { WeaponType.Rifle, config.RifleAnimationController },
            };
        }

        public void OnPlayMovementAnimByInput(Vector2 movementDirect) {

            var velocityX = movementDirect.x;
            var velocityZ = movementDirect.y;

            if (!isInCombat)
            {
                velocityZ = Mathf.Abs(movementDirect.magnitude);
            }

            animator.SetFloat(velocityXHash, velocityX);
            animator.SetFloat(velocityZHash, velocityZ);
        }

        public void OnChangeAnimByWeapon(IWeapon weaponChanged) {
            if (weaponAnimationControllersDic.TryGetValue(weaponChanged.BaseData.Type, out var controller))
            {
                animator.runtimeAnimatorController = controller;
                return;
            }

            animator.runtimeAnimatorController = config.RifleAnimationController;
        }

        public void OnCombatChanged(Vector3 combatDirection) { 
            isInCombat = combatDirection != Vector3.zero;
            animator.SetBool(isInCombatHash, isInCombat);
        }
    }
}

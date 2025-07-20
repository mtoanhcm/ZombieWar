using System;
using UnityEngine;
using ZombieWar.Config;
using Sirenix.OdinInspector;

namespace ZombieWar.Character
{
    public class MainCharacterAnimation : MonoBehaviour
    {
        public Func<bool> CheckCombatState;

        [SerializeField]
        private MainCharacterAnimationConfig config;

        private Animator animator;
        private int velocityXHash;
        private int velocityZHash;
        private int isInCombatHash;

        private RuntimeAnimatorController defaultAnimatorController;

        private void Awake()
        {
            animator = transform.GetComponentInChildren<Animator>();
            velocityXHash = Animator.StringToHash("VelocityX");
            velocityZHash = Animator.StringToHash("VelocityZ");
            isInCombatHash = Animator.StringToHash("IsInCombat");

            defaultAnimatorController = animator.runtimeAnimatorController;
        }

        public void OnPlayMovementAnimByInput(Vector2 movementDirect) {

            var velocityX = movementDirect.x;
            var velocityZ = movementDirect.y;
            bool isInCombat = CheckCombatState != null && CheckCombatState.Invoke();

            if (!isInCombat)
            {
                velocityZ = Mathf.Abs(movementDirect.magnitude);
            }

            animator.SetFloat(velocityXHash, velocityX);
            animator.SetFloat(velocityZHash, velocityZ);
            animator.SetBool(isInCombatHash, isInCombat);
        }

        [Button]
        public void OnEquipRifle() {
            animator.runtimeAnimatorController = config.RifleAnimationController;
        }
    }
}

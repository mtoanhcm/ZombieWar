using System;
using UnityEngine;

namespace ZombieWar.Character
{
    public class MainCharacterAnimation : MonoBehaviour
    {
        public Func<bool> CheckCombatState;

        private Animator animator;

        private int velocityXHash;
        private int velocityZHash;
        private int isInCombatHash;

        private void Awake()
        {
            animator = transform.GetComponentInChildren<Animator>();
            velocityXHash = Animator.StringToHash("VelocityX");
            velocityZHash = Animator.StringToHash("VelocityZ");
            isInCombatHash = Animator.StringToHash("IsInCombat");
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
    }
}

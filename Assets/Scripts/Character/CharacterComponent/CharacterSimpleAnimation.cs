using UnityEngine;

namespace ZombieWar.Character
{
    public class CharacterSimpleAnimation : MonoBehaviour
    {

        private Animator animator;
        private float currentMoveSpeed;
        private bool isAttack;

        private int moveSpeedHash;
        private int isAttackHash;

        private void Awake()
        {
            animator = transform.GetComponentInChildren<Animator>();
            moveSpeedHash = Animator.StringToHash("Move");
            isAttackHash = Animator.StringToHash("IsAttack");

        }

        private void Update()
        {
            if (animator == null)
            {
                return;
            }

            animator.SetFloat(moveSpeedHash, currentMoveSpeed);
            animator.SetBool(isAttackHash, isAttack);
        }

        public void OnSpeedChange(float speed) { 
            currentMoveSpeed = speed;
        }

        public void OnAttackStateChange(bool isAttack) {
            this.isAttack = isAttack;
        }
    }
}

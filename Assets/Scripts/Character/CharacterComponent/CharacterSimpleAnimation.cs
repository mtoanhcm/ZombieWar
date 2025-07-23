using System;
using UnityEngine;

namespace ZombieWar.Character
{
    public class CharacterSimpleAnimation : MonoBehaviour
    {
        public Action OnHitByAttackAnimation;

        private AttackAnimationEvent attackAnimationEvent;
        private Animator animator;
        private float currentMoveSpeed;
        private bool isAttack;
        private bool isDeath;

        private int moveSpeedHash;
        private int isAttackHash;
        private int deathHash;

        private void Awake()
        {
            animator = transform.GetComponentInChildren<Animator>();
            moveSpeedHash = Animator.StringToHash("Move");
            isAttackHash = Animator.StringToHash("IsAttack");
            deathHash = Animator.StringToHash("Death");

            attackAnimationEvent = transform.GetComponentInChildren<AttackAnimationEvent>();
            Debug.Log($"Found {attackAnimationEvent}");
            if( attackAnimationEvent != null)
            {
                attackAnimationEvent.OnHit = OnHitByAttackAnimation;
            }
        }

        private void Update()
        {
            if (animator == null)
            {
                return;
            }

            if (isDeath) {
                currentMoveSpeed = 0;
                isAttack = false;
            }

            animator.SetFloat(moveSpeedHash, currentMoveSpeed);
            animator.SetBool(isAttackHash, isAttack);
        }

        public void Init() {
            isDeath = false;
            isAttack = false;
            currentMoveSpeed = 0;
        }

        public void OnSpeedChange(float speed) { 
            currentMoveSpeed = speed;
        }

        public void OnAttackStateChange(bool isAttack) {
            this.isAttack = isAttack;
        }

        public void OnDeathStateEnter() {
            isDeath = true;
            animator.SetTrigger(deathHash);
        }
    }
}

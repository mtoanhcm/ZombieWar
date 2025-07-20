using System;
using UnityEngine;

namespace ZombieWar.Character
{
    public class CharacterMoveByDirection : MonoBehaviour
    {
        public Func<bool> CheckCombatState;

        private float moveSpeed;
        private float rotationSpeed;
        private Rigidbody rigbody;
        private Vector2 moveDirection;

        private void Awake()
        {
            rigbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(moveDirection == Vector2.zero)
            {
                return;
            }

            Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.fixedDeltaTime;
            rigbody.MovePosition(rigbody.position + moveVector);

            bool isInCombat = CheckCombatState != null && CheckCombatState.Invoke();
            if (!isInCombat) {
                RotateToMovementDirection(moveVector);
            }
            
        }

        public void Init(float moveSpeed, float rotationSpeed) { 
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public void UpdateMoveDirection(Vector2 direction)
        {
            Debug.Log($"UpdateMoveDirection: {direction}");
            moveDirection = direction;
        }

        private void RotateToMovementDirection(Vector3 moveVector) {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            rigbody.MoveRotation(Quaternion.Slerp(rigbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}

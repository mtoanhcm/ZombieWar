using System;
using UnityEngine;

namespace ZombieWar.Character
{
    public class CharacterMoveByDirection : MonoBehaviour
    {
        public Action<float> OnSpeedInputChanged;

        private float moveSpeed;
        private float rotationSpeed;
        private Rigidbody rigbody;
        private Vector2 moveDirection;

        private Vector3 directionNeedToFocus;

        private void Awake()
        {
            rigbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 moveVector = new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed * Time.fixedDeltaTime;
            Vector3 lookDirection = directionNeedToFocus != Vector3.zero ? rotationSpeed * Time.deltaTime * directionNeedToFocus : moveVector;

            if (moveVector != Vector3.zero)
            {
                rigbody.MovePosition(rigbody.position + moveVector);
            }

            if(lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                rigbody.MoveRotation(Quaternion.Slerp(rigbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }

        public void Init(float moveSpeed, float rotationSpeed) { 
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;
        }

        public void UpdateMoveDirection(Vector2 direction)
        {
            moveDirection = direction;
            OnSpeedInputChanged?.Invoke(moveDirection.magnitude);
        }

        public void SetTargetNeedToFocus(Vector3 focusDirection)
        {
            directionNeedToFocus = focusDirection;
        }
    }
}

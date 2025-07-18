using UnityEngine;

namespace ZombieWar.Character
{
    public class CharacterMoveByDirection : MonoBehaviour
    {
        private float moveSpeed;
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
        }

        public void Init(float moveSpeed) { 
            this.moveSpeed = moveSpeed;
        }

        public void UpdateMoveDirection(Vector2 direction)
        {
            moveDirection = direction;
        }
    }
}

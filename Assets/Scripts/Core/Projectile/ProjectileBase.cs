using System;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public Action OnDestroy;

        protected float moveTime;
        protected HitData hitData;

        protected bool canMove;

        private void Update()
        {
            if (!canMove) {
                return;
            }

            MoveToTarget();
        }

        private void OnEnable()
        {
            canMove = false;
        }

        public virtual void Spawn(float moveTime ,HitData hitData) { 
            this.moveTime = moveTime;
            this.hitData = hitData;
        }

        protected abstract void MoveToTarget();
    }
}

using System;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class AIBrainBase : MonoBehaviour
    {
        protected Transform target;
        protected Func<bool> onStopBrain;
        protected IMovement movementComp;
        protected ICombat combatComp;
        protected IHealth healthComp;
        protected bool isInit;
        private void Update()
        {
            if (!isInit) {
                return;
            }

            if (!healthComp.IsAlive || (onStopBrain != null && onStopBrain.Invoke()))
            {
                StopBrain();
                return;
            }

            ActiveBrain();
        }

        public virtual void Init(IMovement movementComp, ICombat combatComp, IHealth healthComp, Func<bool> onStopBrain) { 
            this.onStopBrain = onStopBrain;
            this.movementComp = movementComp;
            this.combatComp = combatComp;
            this.healthComp = healthComp;
        }
        public abstract void ActiveBrain();
        public abstract void StopBrain();
        public abstract void SetTarget(Transform target);
    }
}

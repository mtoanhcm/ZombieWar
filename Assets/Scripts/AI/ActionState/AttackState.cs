using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Pathfinding;

namespace ZombieWar.AI
{
    public class AttackState : IState
    {
        private readonly Transform target;
        private readonly ICombat combatHandle;

        public AttackState(Transform target, ICombat combatHandle)
        {
            this.target = target;
            this.combatHandle = combatHandle;
        }

        public void OnEnter()
        {
            // maybe play animation or stop moving
        }

        public void Tick()
        {
            combatHandle.AttackByCommand();
        }

        public void OnExit()
        {
            // maybe stop attack animation
        }
    }
}

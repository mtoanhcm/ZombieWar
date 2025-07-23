using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Pathfinding;


namespace ZombieWar.AI
{
    public class AINoobBrain : AIBrainBase
    {
        private StateMachine stateMachine;
        private SteeringBehaviour steeringAgent;

        private IState idleState;
        private IState chaseState;
        private IState attackState;

        public override void Init(IMovement movementComp, ICombat combatComp, IHealth healthComp, System.Func<bool> onStopBrain)
        {
            base.Init(movementComp, combatComp, healthComp, onStopBrain);

            steeringAgent = new SteeringBehaviour(transform, movementComp.MoveSpeed);

            isInit = true;
        }
        public override void ActiveBrain()
        {
            stateMachine.Tick();
        }

        public override void StopBrain()
        {
            stateMachine.Stop();
        }

        public override void SetTarget(Transform target) {
            this.target = target;
            InitStateMachine();
        }

        private void InitStateMachine() {
            stateMachine = new StateMachine();
            chaseState = new ChaseState(target, steeringAgent, movementComp.Move);
            attackState = new AttackState(target, combatComp);
            idleState = new IdleState();

            stateMachine.SetInitialState(idleState);

            stateMachine.AddTransition(chaseState, attackState, () => IsTargetInRange());
            stateMachine.AddTransition(attackState, chaseState, () => !IsTargetInRange());

            stateMachine.AddTransition(chaseState, idleState, () => target == null);
            stateMachine.AddTransition(attackState, idleState, () => target == null);

            stateMachine.AddTransition(idleState, chaseState, () => target != null);
        }

        private bool IsTargetInRange() {

            if (target == null) {
                return false;
            }

            var currentWeapon = combatComp.CurrentWeapon;
            if (currentWeapon == null)
            {
                return false;
            }

            var distanceToTarget = (target.transform.position - transform.position).sqrMagnitude;
            var attackRange = currentWeapon.GetAttackRange();

            return distanceToTarget < attackRange * attackRange;
        }
    }
}

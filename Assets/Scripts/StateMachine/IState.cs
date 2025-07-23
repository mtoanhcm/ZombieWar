using UnityEngine;

namespace ZombieWar.Pathfinding
{
    public interface IState
    {
        void OnEnter();
        void Tick();
        void OnExit();
    }
}

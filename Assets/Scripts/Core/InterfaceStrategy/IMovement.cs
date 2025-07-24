using UnityEngine;

namespace ZombieWar.Core
{
    public interface IMovement
    {
        float MoveSpeed { get; }
        bool CanMove { get; }
        void Move(Vector2 direction);
    }
}

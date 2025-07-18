using UnityEngine;

namespace ZombieWar.Core
{
    public interface ICombat
    {
        GameObject Target { get; }
        void Attack(Vector3 direction);
    }
}

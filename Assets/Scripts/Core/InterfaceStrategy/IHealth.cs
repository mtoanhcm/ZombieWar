using System;
using UnityEngine;

namespace ZombieWar.Core
{
    public interface IHealth
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }
        bool IsAlive { get; }

        void TakeDamage(float damage);
        void RestoreHealth(float amount);
    }
}

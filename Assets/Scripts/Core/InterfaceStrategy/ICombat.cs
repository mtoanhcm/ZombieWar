using UnityEngine;

namespace ZombieWar.Core
{
    public interface ICombat
    {
        bool CanAttack { get; }
        IWeapon CurrentWeapon { get; }
        GameObject Self { get; }
        /// <summary>
        /// Call once and attack auto repeat to the giving direction
        /// </summary>
        /// <param name="direction">Attack direction</param>
        void AttackByAuto(Vector2 direction);

        /// <summary>
        /// Call once and attack once
        /// </summary>
        /// <param name="isAttack">Do attack if true</param>
        void AttackByCommand();
    }
}

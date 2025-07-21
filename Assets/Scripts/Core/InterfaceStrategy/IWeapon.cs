using UnityEngine;

namespace ZombieWar.Core
{
    public interface IWeapon
    {
        WeaponBaseData BaseData { get; }
        void Attack();
        bool AddAttachment();
    }
}

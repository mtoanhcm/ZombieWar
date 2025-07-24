using UnityEngine;

namespace ZombieWar.Core
{
    public interface IWeapon
    {
        GameObject Self { get; }
        WeaponBaseData BaseData { get; }
        float GetAttackRange();
        void Attack(bool isAttack);
        void SetOwner(ICombat owner);
        bool AddAttachment();
        void SnapToHandGrabPoint(Transform grabPoint);
    }
}

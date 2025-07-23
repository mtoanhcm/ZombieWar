using System.Collections.Generic;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        [Header("Weapon base attribute")]
        public Transform GripPos;
        public WeaponBaseData BaseData => weaponBaseData;

        public GameObject Self => gameObject;

        protected ICombat owner;
        protected WeaponBaseData weaponBaseData;

        public virtual void Spawn<T>(T weaponData) where T : WeaponBaseData
        {
            weaponBaseData = weaponData;
        }

        public abstract void Attack(bool isAttack);
        public abstract bool AddAttachment();

        public abstract void SetOwner(ICombat owner);

        public abstract float GetAttackRange();

        public abstract void SnapToHandGrabPoint(Transform grabPoint);
    }
}

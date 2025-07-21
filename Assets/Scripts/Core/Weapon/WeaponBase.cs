using System.Collections.Generic;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        [Header("Weapon base attribute")]
        public Transform GripPos;
        public WeaponBaseData BaseData => weaponBaseData;

        protected ICombat owner;
        protected WeaponBaseData weaponBaseData;

        public virtual void Spawn<T>(T weaponData) where T : WeaponBaseData
        {
            weaponBaseData = weaponData;
        }

        public abstract void Attack();
        public abstract bool AddAttachment();

        public abstract void SetOwner(ICombat owner);
    }
}

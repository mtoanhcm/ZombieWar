using System.Collections.Generic;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBase : MonoBehaviour, IWeapon
    {
        public Transform GripPos;
        protected WeaponBaseData weaponBaseData;

        public WeaponBaseData BaseData => weaponBaseData;

        public virtual void Spawn<T>(T weaponData) where T : WeaponBaseData
        {
            weaponBaseData = weaponData;
        }

        public abstract void Attack();
        public abstract bool AddAttachment();
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBase<T> : MonoBehaviour where T : WeaponBaseData
    {
        public Transform GripPos;
        protected T weaponData;

        public virtual void Spawn(T weaponData)
        {
            this.weaponData = weaponData;
        }

        public abstract void Attack();
        public abstract void AddAttachment();
    }
}

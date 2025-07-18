using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponBase<T> : MonoBehaviour where T : WeaponBaseData
    {
        protected T weaponData;

        public virtual void Initialize(T weaponData)
        {
            this.weaponData = weaponData;
        }

        public abstract void Fire();
    }
}

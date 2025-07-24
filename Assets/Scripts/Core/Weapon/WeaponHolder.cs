using System.Collections.Generic;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponHolder : MonoBehaviour
    {
        protected WeaponHolderConfig weaponHolderConfig;
        protected Dictionary<CharacterWeaponEquipSlot, IWeapon> weaponSlots;

        public virtual void Init() {
            weaponSlots = new Dictionary<CharacterWeaponEquipSlot, IWeapon>();
        }

        public abstract bool GetWeapon(CharacterWeaponEquipSlot slot, out IWeapon weapon);
        public abstract bool AddWeapon(IWeapon weaponData);
        public abstract void PutBackWeapon(IWeapon weapon);
        public abstract bool RemoveWeapon(IWeapon weaponData);
        public abstract bool HasNoWeapon();
    }
}

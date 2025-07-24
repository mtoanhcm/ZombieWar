using System;
using UnityEngine;

namespace ZombieWar.Core
{
    [CreateAssetMenu(fileName = "WeaponHolderConfig", menuName = "Config/Weapon/WeaponHolderConfig")]
    public class WeaponHolderConfig : ScriptableObject
    {
        [Flags]
        public enum WeaponSlotFlag { 
            None = 0,
            Melee = 1 << 0,
            Pistol = 1 << 1,
            Rifle = 1 << 2,
            Shotgun = 1 << 3,
            Sniper = 1 << 4,
            SMG = 1 << 5,
            LMG = 1 << 6,
        }

        [Serializable]
        public class WeaponHolderSlot { 
            public CharacterWeaponEquipSlot Slot;
            public WeaponSlotFlag WeaponTypeCanHold;
        }

        [SerializeField]
        private WeaponHolderSlot[] weaponHolderSlots;

        public bool GetHolderSlot(WeaponType type, out CharacterWeaponEquipSlot holderSlot) { 
            var flag = ConvertToFlag(type);

            foreach(var slot in weaponHolderSlots)
            {
                if ((slot.WeaponTypeCanHold & flag) != 0)
                {
                    holderSlot = slot.Slot;
                    return true;
                }
            }

            holderSlot = CharacterWeaponEquipSlot.Slot1; // Default value if not found
            return false;
        }

        private WeaponSlotFlag ConvertToFlag(WeaponType type)
        {
            return type switch
            {
                WeaponType.Melee => WeaponSlotFlag.Melee,
                WeaponType.Pistol => WeaponSlotFlag.Pistol,
                WeaponType.Shotgun => WeaponSlotFlag.Shotgun,
                WeaponType.SMG => WeaponSlotFlag.SMG,
                WeaponType.Rifle => WeaponSlotFlag.Rifle,
                WeaponType.LMG => WeaponSlotFlag.LMG,
                WeaponType.Sniper => WeaponSlotFlag.Sniper,
                _ => WeaponSlotFlag.None
            };
        }
    }
}

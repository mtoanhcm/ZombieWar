using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacterWeaponHolder : WeaponHolder
    {
        public override void Init()
        {
            base.Init();

            foreach(CharacterWeaponEquipSlot slot in System.Enum.GetValues(typeof(CharacterWeaponEquipSlot)))
            {
                weaponSlots[slot] = null;
            }

            weaponHolderConfig = Resources.Load<WeaponHolderConfig>("MainCharacterWeaponHolderConfig");
        }

        public override bool AddWeapon(IWeapon weapon)
        {
            if (!weaponHolderConfig.GetHolderSlot(weapon.BaseData.Type, out var slot)) { 
                DebugCustom.LogError($"Weapon type {weapon.BaseData.Type} is not supported by this weapon holder.");
                return false;
            }

            if (weaponSlots[slot] != null) {
                RemoveWeapon(weaponSlots[slot]);
            }

            weaponSlots[slot] = weapon;

            DebugCustom.Log($"Weapon {weapon.BaseData.ID} added to slot {slot} in MainCharacterWeaponHolder.", Color.green);

            return true;
        }

        public override bool RemoveWeapon(IWeapon weapon)
        {
            foreach (var slot in weaponSlots)
            {
                if (slot.Value == weapon)
                {
                    weaponSlots[slot.Key] = null;
                    return true;
                }
            }

            return false;
        }
    }
}

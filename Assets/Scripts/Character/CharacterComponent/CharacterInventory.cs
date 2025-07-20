using System.Collections.Generic;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class CharacterInventory : MonoBehaviour
    {
        List<IWeapon> weapons;

        public void Init(IWeapon[] initWeapons) {
            if (initWeapons == null) { 
                initWeapons = new IWeapon[0];
            }

            weapons = new List<IWeapon>(initWeapons);
        }

        public bool TryGetWeapon<T>(out T result) where T : IWeapon {
            foreach (var weapon in weapons) {
                if (weapon is T targetWeapon) {
                    result = targetWeapon;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public void AddWeapon(IWeapon weapon) {
            if (weapon == null) {
                Debug.LogWarning("Attempted to add a null weapon to the inventory.");
                return;
            }

            if (!weapons.Contains(weapon)) {
                weapons.Add(weapon);
            } else {
                Debug.LogWarning("Weapon already exists in the inventory.");
            }
        }

        public void RemoveWeapon(IWeapon weapon) {
            if (weapon == null) {
                Debug.LogWarning("Attempted to remove a null weapon from the inventory.");
                return;
            }
            if (weapons.Contains(weapon)) {
                weapons.Remove(weapon);
            } else {
                Debug.LogWarning("Weapon not found in the inventory.");
            }
        }
    }
}

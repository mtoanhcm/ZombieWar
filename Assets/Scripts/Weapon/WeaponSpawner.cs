using Sirenix.OdinInspector;
using UnityEngine;

namespace ZombieWar.Weapon
{
    public class WeaponSpawner : MonoBehaviour
    {
        [SerializeField]
        private HitScanWeapon weaponPrefab;
        [SerializeField]
        private HitScanWeaponConfig weaponConfig;

        [Button]
        public void SpawnAK12(Transform weaponGragHandler) {
            var weapon = Instantiate(weaponPrefab, weaponGragHandler);
            weapon.Spawn(new HitScanWeaponData(weaponConfig));
            weapon.SnapToHandGrabPoint(weaponGragHandler);
        }
    }
}

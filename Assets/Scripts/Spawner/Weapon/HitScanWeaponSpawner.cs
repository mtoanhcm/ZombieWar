using System.Collections.Generic;
using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Weapon;

namespace ZombieWar.Spawner
{
    public class HitScanWeaponSpawner : WeaponSpawnerBase
    {
        private Dictionary<WeaponID, WeaponBase> prefabDic = new Dictionary<WeaponID, WeaponBase>();

        public override bool SpawnWeapon<T>(T config, out IWeapon result, Transform grabHand = null) {

            if(config is not HitScanWeaponConfig) {
                DebugCustom.LogError("HitScanWeaponSpawner can only spawn HitScanWeaponConfig.");
                result = null;
                return false;
            }

            HitScanWeapon weaponPrefab;
            if (prefabDic.ContainsKey(config.ID) && prefabDic[config.ID] != null)
            {
                weaponPrefab = prefabDic[config.ID] as HitScanWeapon;
            }
            else {
                weaponPrefab = Resources.Load<HitScanWeapon>($"Weapon/HitScanWeapon/{config.ID}");
                if (weaponPrefab != null) {
                    prefabDic.Add(config.ID, weaponPrefab);
                }
            }

            if(weaponPrefab == null) {
                Debug.LogError($"Weapon prefab for ID {config.ID} not found in Resources/Weapon.");
                result = null;
                return false;
            }

            var data = new HitScanWeaponData(config as HitScanWeaponConfig);
            var weapon = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity);
            weapon.Spawn(data);

            if (grabHand != null) {
                weapon.SnapToHandGrabPoint(grabHand);
            }

            result = weapon;
            return true;
        }
    }
}

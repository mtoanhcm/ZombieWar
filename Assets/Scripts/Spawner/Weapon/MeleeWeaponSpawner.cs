using System.Collections.Generic;
using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Weapon;

namespace ZombieWar.Spawner
{
    public class MeleeWeaponSpawner : WeaponSpawnerBase
    {
        private Dictionary<WeaponID, WeaponBase> prefabDic = new Dictionary<WeaponID, WeaponBase>();

        public override bool SpawnWeapon<T>(T config, out IWeapon result)
        {
            if (config is not MeleeWeaponConfig meleeWeaponConfig)
            {
                DebugCustom.LogError("MeleeWeaponSpawner can only spawn MeleeWeaponConfig.");
                result = null;
                return false;
            }

            MeleeWeapon weaponPrefab;
            if (prefabDic.ContainsKey(config.ID) && prefabDic[config.ID] != null)
            {
                weaponPrefab = prefabDic[config.ID] as MeleeWeapon;
            }
            else
            {
                weaponPrefab = Resources.Load<MeleeWeapon>($"Weapon/MeleeWeapon/{config.ID}");
                if(weaponPrefab != null)
                {
                    prefabDic.Add(config.ID, weaponPrefab);
                }
            }

            if (weaponPrefab == null)
            {
                Debug.LogError($"Weapon prefab for ID {config.ID} not found in Resources/Weapon.");
                result = null;
                return false;
            }

            var data = new MeleeWeaponData(meleeWeaponConfig);
            var weapon = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity);
            weapon.Spawn(data);

            result = weapon;
            return true;
        }

        
    }
}

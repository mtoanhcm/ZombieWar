using System.Collections.Generic;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class EnemySpawner : CharacterSpawnerBase
    {
        private Dictionary<CharacterID ,CharacterBase> prefabDic = new Dictionary<CharacterID, CharacterBase>();

        public override bool SpawnCharacter<T>(T config, out ICharacter result)
        {
            if (config is not CharacterBaseConfig)
            {
                DebugCustom.LogError("HitScanWeaponSpawner can only spawn HitScanWeaponConfig.");
                result = null;
                return false;
            }

            ZombieCharacter characterPrefab;
            if (prefabDic.ContainsKey(config.ID) && prefabDic[config.ID] != null)
            {
                characterPrefab = prefabDic[config.ID] as ZombieCharacter;
            }
            else
            {
                characterPrefab = Resources.Load<ZombieCharacter>($"Character/Enemy/{config.ID}");
                prefabDic.Add(config.ID, characterPrefab);
            }

            if (characterPrefab == null)
            {
                Debug.LogError($"Weapon prefab for ID {config.ID} not found in Resources/Weapon.");
                result = null;
                return false;
            }

            var data = new SimpleEnemyData(config as SimpleEnemyConfig);
            var character = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
            character.Spawn(data);

            result = character;
            return true;
        }
    }
}

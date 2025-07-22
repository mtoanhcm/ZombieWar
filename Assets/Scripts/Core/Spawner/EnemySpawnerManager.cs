using Sirenix.OdinInspector;
using System.Net;
using UnityEngine;

namespace ZombieWar.Core
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField]
        private bool enableAutoSpawn;
        [SerializeField, ShowIf(nameof(enableAutoSpawn))]
        private float spawnCooldown;
        [SerializeField]
        private CharacterSpawnerBase characterSpawners;
        [SerializeField]
        private CharacterBaseConfig[] characterConfigs;

        private float tempSpawnDelay;

        private void Update()
        {
            if (!enableAutoSpawn)
            {
                return;
            }

            if (tempSpawnDelay > Time.time) {
                return;
            }

            SpawnEnemy();

            tempSpawnDelay = spawnCooldown + Time.time;
        }

        [Button]
        public void SpawnEnemy() { 

            if(characterConfigs == null || characterConfigs.Length == 0)
            {
                DebugCustom.LogError("No character configs available for spawning.");
                return;
            }

            var config = characterConfigs[Random.Range(0, characterConfigs.Length)];
            characterSpawners.SpawnCharacter(config, out var character);
            character.Self.transform.position = characterSpawners.transform.position;
        }
    }
}

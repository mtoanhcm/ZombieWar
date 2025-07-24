using Sirenix.OdinInspector;
using ZombieWar.Core;
using UnityEngine;

namespace ZombieWar.Spawner
{
    public class EnemySpawnerManager : MonoBehaviour
    {
        [SerializeField]
        private int maxSpawnCount = 40;
        [SerializeField]
        private bool enableAutoSpawn;
        [SerializeField, ShowIf(nameof(enableAutoSpawn))]
        private float spawnCooldown = 1f;
        [SerializeField]
        private CharacterBaseConfig[] characterConfigs;

        private EnemySpawner[] enemySpawners;

        private float tempSpawnDelay;

        private void Start()
        {
            InitSpawner();
        }

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

            var randomSpawner = enemySpawners[Random.Range(0, enemySpawners.Length)];
            if (randomSpawner.SpawnCharacter(out var character))
            {
                character.Self.transform.position = randomSpawner.transform.position;
            }
        }

        private void InitSpawner() {
            enemySpawners = new EnemySpawner[characterConfigs.Length];
            for (var i = 0; i < characterConfigs.Length; i++)
            {
                var config = characterConfigs[i];

                var spawnerObj = new GameObject("EnemySpawner");
                spawnerObj.transform.SetParent(transform, false);
                spawnerObj.transform.localPosition = Vector3.zero;

                var spawner = spawnerObj.AddComponent<EnemySpawner>();
                spawner.Init(config, 50);

                enemySpawners[i] = spawner;
            }
        }
    }
}

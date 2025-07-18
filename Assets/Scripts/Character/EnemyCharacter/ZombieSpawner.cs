using System;
using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class ZombieSpawner : MonoBehaviour
    {
        public Action<ZombieCharacter> OnZombieSpawned;

        [SerializeField]
        private float spawnInterval = 2f;
        [SerializeField]
        private ZombieCharacter zombiePrefab;

        private bool isInit;
        private ObjectPool<ZombieCharacter> zombiePool;
        private float tempSpawnInterval;

        private void Start()
        {
            zombiePool = new ObjectPool<ZombieCharacter>(zombiePrefab, 30, transform);

            isInit = true;
        }

        private void Update()
        {
            if (!isInit)
            {
                return;
            }

            if(tempSpawnInterval > Time.time)
            {
                return;
            }

            tempSpawnInterval = spawnInterval + Time.deltaTime;
            
            var zombie = zombiePool.GetObject();



            OnZombieSpawned?.Invoke(zombie);
        }
    }
}

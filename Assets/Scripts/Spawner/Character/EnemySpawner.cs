using UnityEngine;
using ZombieWar.Core;
using ZombieWar.Character;
using System.Threading.Tasks;
using System.Collections;

namespace ZombieWar.Spawner
{
    public class EnemySpawner : CharacterSpawnerBase
    {
        public override void Init(CharacterBaseConfig config, int poolIncrease) {

            this.config = config;

            var prefab = Resources.Load<ZombieCharacter>($"Character/Enemy/{config.ID}");
            if (prefab == null) {
                DebugCustom.LogError($"Cannot create prefab for character {config.ID}");
                return;
            }

            characterPool = new ObjectPool<CharacterBase>(prefab, poolIncrease, transform);
        }

        public override bool SpawnCharacter(out ICharacter result)
        {
            if (config is not CharacterBaseConfig)
            {
                result = null;
                return false;
            }

            //Hardcode
            var data = new SimpleEnemyData(config as SimpleEnemyConfig);
            var character = characterPool.GetObject() as ZombieCharacter;

            character.OnDeath -= ReturnCharacter;
            character.OnDeath += ReturnCharacter;

            character.Spawn(data);

            result = character;
            return true;
        }

        private async void ReturnCharacter(CharacterBase character) {
            await Task.Delay(3000);

            characterPool.ReturnObject(character);
        }
    }
}

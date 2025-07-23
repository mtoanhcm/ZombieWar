using System.Collections.Generic;
using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterSpawnerBase : MonoBehaviour
    {
        protected CharacterBaseConfig config;

        protected ObjectPool<CharacterBase> characterPool;
        public abstract void Init(CharacterBaseConfig config, int poolIncrease);
        public abstract bool SpawnCharacter(out ICharacter result);
    }
}

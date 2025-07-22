using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class CharacterSpawnerBase : MonoBehaviour
    {
        public abstract bool SpawnCharacter<T>(T config, out ICharacter result) where T : CharacterBaseConfig;
    }
}

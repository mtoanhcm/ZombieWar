using UnityEngine;

namespace ZombieWar.Core
{
    public abstract class WeaponSpawnerBase : MonoBehaviour
    {
        public abstract bool SpawnWeapon<T>(T config, out IWeapon result) where T : WeaponBaseConfig;
    }
}

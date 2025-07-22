using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class SimpleEnemyData : CharacterBaseData
    {
        public WeaponBaseConfig WeaponConfig { get; private set; }

        public SimpleEnemyData(SimpleEnemyConfig config) : base(config)
        {
            WeaponConfig = config.WeaponConfig;
        }
    }
}

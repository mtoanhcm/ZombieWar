using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    [CreateAssetMenu(fileName = "SimpleEnemyConfig", menuName = "Config/CharacterConfig/SimpleEnemyConfig")]
    public class SimpleEnemyConfig : CharacterBaseConfig
    {
        [Header("Simple enemy attribute")]
        public WeaponBaseConfig WeaponConfig;
    }
}

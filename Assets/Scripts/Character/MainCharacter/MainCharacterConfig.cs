using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    [CreateAssetMenu(fileName = "MainCharacterConfig", menuName = "Config/CharacterConfig/MainCharacterConfig", order = 1)]
    public class MainCharacterConfig : CharacterBaseConfig
    {
        [Header("Start up weapon")]
        public WeaponBaseConfig[] StartUpWeapons;
    }
}

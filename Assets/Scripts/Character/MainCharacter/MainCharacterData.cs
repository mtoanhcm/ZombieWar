using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacterData : CharacterBaseData
    {
        public WeaponBaseConfig[] StartUpWeaponConfigs { get; private set; }

        public MainCharacterData(MainCharacterConfig config) : base(config)
        {
            StartUpWeaponConfigs = config.StartUpWeapons;
        }
    }
}

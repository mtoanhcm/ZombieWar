using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class MainCharacterData : CharacterBaseData
    {
        public float RotateSpeed { get; private set; }

        public MainCharacterData(MainCharacterConfig config) : base(config)
        {
            RotateSpeed = config.RotateSpeed;
        }
    }
}

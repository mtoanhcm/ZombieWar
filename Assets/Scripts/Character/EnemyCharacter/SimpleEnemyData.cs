using UnityEngine;
using ZombieWar.Core;

namespace ZombieWar.Character
{
    public class SimpleEnemyData : CharacterBaseData
    {
        public float Damage { get; private set; }
        public float AttackRange { get; private set; }
        public float AttackSpeed { get; private set; }

        public SimpleEnemyData(SimpleEnemyConfig config) : base(config)
        {
            Damage = config.Damage;
            AttackRange = config.AttackRange;
            AttackSpeed = config.AttackSpeed;
        }
    }
}
